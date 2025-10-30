
using Infrastructure.Auth.Configuration.Option;
using Infrastructure.Auth.Context;
using Infrastructure.Auth.Model;
using Infrastructure.Auth.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PsicoAgendaTests.Infrastructure.Auth
{
    public class JwtTokenServiceTests
    {
        private static AuthContext CreateDb(string name)
        {
            var opts = new DbContextOptionsBuilder<AuthContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new AuthContext(opts);
        }

        private static IOptions<JwtOptions> CreateJwtOptions() =>
            Options.Create(new JwtOptions
            {
                Key = "A_VERY_LONG_TEST_KEY_1234567890_ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                Issuer = "issuer-tests",
                Audience = "audience-tests",
                AccessTokenMinutes = 3,
                RefreshTokenDays = 10
            });

        private static Mock<UserManager<UserApplication>> CreateUserManagerMock()
        {
            var store = new Mock<IUserStore<UserApplication>>();
            var options = new Mock<IOptions<IdentityOptions>>();
            options.Setup(o => o.Value).Returns(new IdentityOptions());

            var pwdHasher = new Mock<IPasswordHasher<UserApplication>>();
            var userValidators = new List<IUserValidator<UserApplication>>();
            var pwdValidators = new List<IPasswordValidator<UserApplication>>();
            var normalizer = new Mock<ILookupNormalizer>();
            var services = new Mock<IServiceProvider>();
            var logger = new Mock<ILogger<UserManager<UserApplication>>>();

            return new Mock<UserManager<UserApplication>>(
                store.Object, options.Object, pwdHasher.Object, userValidators,
                pwdValidators, normalizer.Object, new IdentityErrorDescriber(),
                services.Object, logger.Object);
        }

        private static ClaimsPrincipal ValidateJwt(string token, JwtOptions opt)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidIssuer = opt.Issuer,
                ValidAudience = opt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(opt.Key)),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            return handler.ValidateToken(token, parameters, out _);
        }


        [Fact(DisplayName = "IssueTokensAsync: emite JWT assinado e persiste RefreshToken")]
        public async Task IssueTokens_EmitsJwt_And_PersistsRefresh()
        {
            // Arrange
            using var db = CreateDb(nameof(IssueTokens_EmitsJwt_And_PersistsRefresh));
            var jwtOpts = CreateJwtOptions();
            var userMgr = CreateUserManagerMock();

            var userId = Guid.NewGuid();
            var user = new UserApplication { Id = userId, UserName = "alice" };

            userMgr.Setup(m => m.GetRolesAsync(user))
                   .ReturnsAsync(new List<string> { "Admin", "User" });

            var sut = new JwtTokenService(userMgr.Object, db, jwtOpts);

            // Act
            var (access, refresh) = await sut.IssueTokensAsync(user);

            // Assert - access token
            var principal = ValidateJwt(access, jwtOpts.Value);

            Assert.Equal("alice", principal.FindFirst(ClaimTypes.Name)!.Value);

        }

        [Fact(DisplayName = "RotateRefreshAsync: com refresh válido revoga antigo e retorna novos tokens")]
        public async Task RotateRefresh_ValidToken_RevokesOld_And_ReturnsNew()
        {
            // Arrange
            using var db = CreateDb(nameof(RotateRefresh_ValidToken_RevokesOld_And_ReturnsNew));
            var jwtOpts = CreateJwtOptions();
            var userMgr = CreateUserManagerMock();

            var userId = Guid.NewGuid();
            var user = new UserApplication { Id = userId, UserName = "bob" };
            userMgr.Setup(m => m.FindByIdAsync(userId.ToString("D"))).ReturnsAsync(user);
            userMgr.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(new List<string> { "User" });

            // seed refresh válido
            var seed = new RefreshToken
            {
                UserId = userId,
                Token = "seed-refresh",
                ExpiresAtUtc = DateTime.UtcNow.AddDays(5),
                Revoked = false
            };
            db.RefreshTokens.Add(seed);
            await db.SaveChangesAsync();

            var sut = new JwtTokenService(userMgr.Object, db, jwtOpts);

            // Act
            var (access, newRefresh) = await sut.RotateRefreshAsync("seed-refresh");

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(access));
            Assert.NotNull(newRefresh);
            Assert.NotEqual("seed-refresh", newRefresh.Token);

            // antigo foi revogado
            var old = await db.RefreshTokens.SingleAsync(x => x.Token == "seed-refresh");
            Assert.True(old.Revoked);

            // novo foi persistido
            var all = await db.RefreshTokens.ToListAsync();
            Assert.Contains(all, r => r.Token == newRefresh.Token && r.UserId == userId && !r.Revoked);
        }

        [Fact(DisplayName = "RotateRefreshAsync: token não encontrado => SecurityTokenException")]
        public async Task RotateRefresh_TokenNotFound_Throws()
        {
            using var db = CreateDb(nameof(RotateRefresh_TokenNotFound_Throws));
            var jwtOpts = CreateJwtOptions();
            var userMgr = CreateUserManagerMock();
            var sut = new JwtTokenService(userMgr.Object, db, jwtOpts);

            await Assert.ThrowsAsync<SecurityTokenException>(() =>
                sut.RotateRefreshAsync("unknown"));
        }

        [Fact(DisplayName = "RotateRefreshAsync: token expirado => SecurityTokenException")]
        public async Task RotateRefresh_ExpiredToken_Throws()
        {
            using var db = CreateDb(nameof(RotateRefresh_ExpiredToken_Throws));
            var jwtOpts = CreateJwtOptions();
            var userMgr = CreateUserManagerMock();

            db.RefreshTokens.Add(new RefreshToken
            {
                UserId = Guid.NewGuid(),
                Token = "expired",
                ExpiresAtUtc = DateTime.UtcNow.AddMinutes(-1),
                Revoked = false
            });
            await db.SaveChangesAsync();

            var sut = new JwtTokenService(userMgr.Object, db, jwtOpts);

            await Assert.ThrowsAsync<SecurityTokenException>(() =>
                sut.RotateRefreshAsync("expired"));
        }

        [Fact(DisplayName = "RotateRefreshAsync: token revogado => SecurityTokenException")]
        public async Task RotateRefresh_RevokedToken_Throws()
        {
            using var db = CreateDb(nameof(RotateRefresh_RevokedToken_Throws));
            var jwtOpts = CreateJwtOptions();
            var userMgr = CreateUserManagerMock();

            db.RefreshTokens.Add(new RefreshToken
            {
                UserId = Guid.NewGuid(),
                Token = "revoked",
                ExpiresAtUtc = DateTime.UtcNow.AddDays(1),
                Revoked = true
            });
            await db.SaveChangesAsync();

            var sut = new JwtTokenService(userMgr.Object, db, jwtOpts);

            await Assert.ThrowsAsync<SecurityTokenException>(() =>
                sut.RotateRefreshAsync("revoked"));
        }

        [Fact(DisplayName = "RotateRefreshAsync: usuário não encontrado => SecurityTokenException")]
        public async Task RotateRefresh_UserNotFound_Throws()
        {
            using var db = CreateDb(nameof(RotateRefresh_UserNotFound_Throws));
            var jwtOpts = CreateJwtOptions();
            var userMgr = CreateUserManagerMock();

            var userId = Guid.NewGuid();

            db.RefreshTokens.Add(new RefreshToken
            {
                UserId = userId,
                Token = "valid-but-user-missing",
                ExpiresAtUtc = DateTime.UtcNow.AddDays(1),
                Revoked = false
            });
            await db.SaveChangesAsync();

            userMgr.Setup(m => m.FindByIdAsync(userId.ToString("D"))).ReturnsAsync((UserApplication)null!);

            var sut = new JwtTokenService(userMgr.Object, db, jwtOpts);

            await Assert.ThrowsAsync<SecurityTokenException>(() =>
                sut.RotateRefreshAsync("valid-but-user-missing"));
        }
    }
}
