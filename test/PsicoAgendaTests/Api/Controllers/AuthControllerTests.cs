
using Infrastructure.Auth.Model;
using Infrastructure.Auth.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PsicoAgendaAPI.Controllers;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace PsicoAgendaTests.Api.Controllers
{
    public class AuthControllerTests
    {
        private static Mock<UserManager<IdentityUser>> CreateUserManagerMock()
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            var options = new Mock<IOptions<IdentityOptions>>();
            options.Setup(o => o.Value).Returns(new IdentityOptions());

            var pwdHasher = new Mock<IPasswordHasher<IdentityUser>>();
            var userValidators = new List<IUserValidator<IdentityUser>>();
            var pwdValidators = new List<IPasswordValidator<IdentityUser>>();
            var normalizer = new Mock<ILookupNormalizer>();
            var services = new Mock<IServiceProvider>();
            var logger = new Mock<ILogger<UserManager<IdentityUser>>>();

            return new Mock<UserManager<IdentityUser>>(
                store.Object, options.Object, pwdHasher.Object, userValidators,
                pwdValidators, normalizer.Object, new IdentityErrorDescriber(),
                services.Object, logger.Object);
        }

        private static Mock<SignInManager<IdentityUser>> CreateSignInManagerMock(UserManager<IdentityUser> userManager)
        {
            var contextAccessor = new Mock<IHttpContextAccessor>();
            contextAccessor.Setup(a => a.HttpContext).Returns(new DefaultHttpContext());

            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            var options = new Mock<IOptions<IdentityOptions>>();
            options.Setup(o => o.Value).Returns(new IdentityOptions());

            var logger = new Mock<ILogger<SignInManager<IdentityUser>>>();
            var schemes = new Mock<Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider>();
            var confirmation = new Mock<IUserConfirmation<IdentityUser>>();

            return new Mock<SignInManager<IdentityUser>>(
                userManager, contextAccessor.Object, claimsFactory.Object,
                options.Object, logger.Object, schemes.Object, confirmation.Object);
        }

        private static AuthController CreateController(
            Mock<UserManager<IdentityUser>> userMgr,
            Mock<SignInManager<IdentityUser>> signInMgr,
            Mock<IJwtTokenService> jwt)
            {
                return new AuthController(userMgr.Object, signInMgr.Object, jwt.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext()
                    }
                };
            }

        [Fact(DisplayName = "Register: sucesso retorna Ok(TokenResponse) e adiciona role 'user'")]
        public async Task Register_Sucesso()
        {
            // Arrange
            var userMgr = CreateUserManagerMock();
            var signInMgr = CreateSignInManagerMock(userMgr.Object);
            var jwt = new Mock<IJwtTokenService>();

            userMgr.Setup(m => m.CreateAsync(It.IsAny<IdentityUser>(), "P@ss123"))
                   .ReturnsAsync(IdentityResult.Success);
            userMgr.Setup(m => m.AddToRoleAsync(It.IsAny<IdentityUser>(), "user"))
                   .ReturnsAsync(IdentityResult.Success);

            jwt.Setup(t => t.IssueTokensAsync(It.IsAny<IdentityUser>(), default))
               .ReturnsAsync(("access-abc", new RefreshToken { Token = "refresh-xyz" }));

            var controller = CreateController(userMgr, signInMgr, jwt);

            var req = new AuthController.RegisterRequest("alice", "alice@mail.test", "P@ss123");

            // Act
            var result = await controller.Register(req);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var payload = Assert.IsType<AuthController.TokenResponse>(ok.Value);
            Assert.Equal("access-abc", payload.AccessToken);
            Assert.Equal("refresh-xyz", payload.RefreshToken);

            userMgr.Verify(m => m.AddToRoleAsync(It.IsAny<IdentityUser>(), "user"), Times.Once);
            jwt.Verify(t => t.IssueTokensAsync(It.IsAny<IdentityUser>(), default), Times.Once);
        }

        [Fact(DisplayName = "Register: falha de criação retorna BadRequest com erros")]
        public async Task Register_Falha()
        {
            var userMgr = CreateUserManagerMock();
            var signInMgr = CreateSignInManagerMock(userMgr.Object);
            var jwt = new Mock<IJwtTokenService>();

            userMgr.Setup(m => m.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                   .ReturnsAsync(IdentityResult.Failed(new IdentityError { Code = "X", Description = "Erro" }));

            var controller = CreateController(userMgr, signInMgr, jwt);
            var req = new AuthController.RegisterRequest("bob", "bob@mail.test", "bad");

            var result = await controller.Register(req);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            var errs = Assert.IsAssignableFrom<IEnumerable<IdentityError>>(bad.Value);
            Assert.Contains(errs, e => e.Description == "Erro");
            jwt.Verify(t => t.IssueTokensAsync(It.IsAny<IdentityUser>(), default), Times.Never);
        }

        // ---------- Tests: /login ----------
        [Fact(DisplayName = "Login por username: sucesso retorna Ok(TokenResponse)")]
        public async Task Login_Sucesso_Username()
        {
            var userMgr = CreateUserManagerMock();
            var signInMgr = CreateSignInManagerMock(userMgr.Object);
            var jwt = new Mock<IJwtTokenService>();

            var user = new IdentityUser { Id = Guid.NewGuid().ToString("D"), UserName = "alice", Email = "a@a" };

            userMgr.Setup(m => m.FindByNameAsync("alice")).ReturnsAsync(user);
            userMgr.Setup(m => m.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null!);
            signInMgr.Setup(s => s.CheckPasswordSignInAsync(user, "P@ss", true))
                     .ReturnsAsync(SignInResult.Success);

            jwt.Setup(t => t.IssueTokensAsync(user, default))
               .ReturnsAsync(("token-access", new RefreshToken { Token = "token-refresh" }));

            var controller = CreateController(userMgr, signInMgr, jwt);

            var res = await controller.Login(new AuthController.LoginRequest("alice", "P@ss"));

            var ok = Assert.IsType<OkObjectResult>(res);
            var payload = Assert.IsType<AuthController.TokenResponse>(ok.Value);
            Assert.Equal("token-access", payload.AccessToken);
            Assert.Equal("token-refresh", payload.RefreshToken);
        }

        [Fact(DisplayName = "Login por email: sucesso retorna Ok(TokenResponse)")]
        public async Task Login_Sucesso_Email()
        {
            var userMgr = CreateUserManagerMock();
            var signInMgr = CreateSignInManagerMock(userMgr.Object);
            var jwt = new Mock<IJwtTokenService>();

            var user = new IdentityUser { Id = Guid.NewGuid().ToString("D"), UserName = "bob", Email = "bob@mail.test" };

            userMgr.Setup(m => m.FindByNameAsync("bob@mail.test")).ReturnsAsync((IdentityUser)null!);
            userMgr.Setup(m => m.FindByEmailAsync("bob@mail.test")).ReturnsAsync(user);
            signInMgr.Setup(s => s.CheckPasswordSignInAsync(user, "P@ss", true))
                     .ReturnsAsync(SignInResult.Success);

            jwt.Setup(t => t.IssueTokensAsync(user, default))
               .ReturnsAsync(("a", new RefreshToken { Token = "b" }));

            var controller = CreateController(userMgr, signInMgr, jwt);

            var res = await controller.Login(new AuthController.LoginRequest("bob@mail.test", "P@ss"));

            var ok = Assert.IsType<OkObjectResult>(res);
            var payload = Assert.IsType<AuthController.TokenResponse>(ok.Value);
            Assert.Equal("a", payload.AccessToken);
            Assert.Equal("b", payload.RefreshToken);
        }

        [Fact(DisplayName = "Login: usuário não encontrado retorna Unauthorized")]
        public async Task Login_UsuarioNaoEncontrado()
        {
            var userMgr = CreateUserManagerMock();
            var signInMgr = CreateSignInManagerMock(userMgr.Object);
            var jwt = new Mock<IJwtTokenService>();

            userMgr.Setup(m => m.FindByNameAsync("who")).ReturnsAsync((IdentityUser)null!);
            userMgr.Setup(m => m.FindByEmailAsync("who")).ReturnsAsync((IdentityUser)null!);

            var controller = CreateController(userMgr, signInMgr, jwt);

            var res = await controller.Login(new AuthController.LoginRequest("who", "pass"));
            Assert.IsType<UnauthorizedResult>(res);
        }

        [Fact(DisplayName = "Login: senha inválida retorna Unauthorized")]
        public async Task Login_SenhaInvalida()
        {
            var userMgr = CreateUserManagerMock();
            var signInMgr = CreateSignInManagerMock(userMgr.Object);
            var jwt = new Mock<IJwtTokenService>();

            var user = new IdentityUser { Id = Guid.NewGuid().ToString("D"), UserName = "alice" };

            userMgr.Setup(m => m.FindByNameAsync("alice")).ReturnsAsync(user);
            signInMgr.Setup(s => s.CheckPasswordSignInAsync(user, "wrong", true))
                     .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var controller = CreateController(userMgr, signInMgr, jwt);

            var res = await controller.Login(new AuthController.LoginRequest("alice", "wrong"));
            Assert.IsType<UnauthorizedResult>(res);
        }

        
        [Fact(DisplayName = "Refresh: sucesso retorna Ok(TokenResponse)")]
        public async Task Refresh_Sucesso()
        {
            var userMgr = CreateUserManagerMock();
            var signInMgr = CreateSignInManagerMock(userMgr.Object);
            var jwt = new Mock<IJwtTokenService>();

            jwt.Setup(t => t.RotateRefreshAsync("r1", default))
               .ReturnsAsync(("a1", new RefreshToken { Token = "r2" }));

            var controller = CreateController(userMgr, signInMgr, jwt);

            var res = await controller.Refresh(new AuthController.RefreshRequest("r1"));

            var ok = Assert.IsType<OkObjectResult>(res);
            var payload = Assert.IsType<AuthController.TokenResponse>(ok.Value);
            Assert.Equal("a1", payload.AccessToken);
            Assert.Equal("r2", payload.RefreshToken);
        }

        
        [Fact(DisplayName = "Me: retorna nome e claims do usuário autenticado")]
        public void Me_Sucesso()
        {
            var userMgr = CreateUserManagerMock();
            var signInMgr = CreateSignInManagerMock(userMgr.Object);
            var jwt = new Mock<IJwtTokenService>();

            var controller = CreateController(userMgr, signInMgr, jwt);

            // simula usuário autenticado
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, "alice"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim("custom", "123")
            },
            authenticationType: "TestAuth");

            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(identity);

            var res = controller.Me();

            var ok = Assert.IsType<OkObjectResult>(res);
            Assert.NotNull(ok.Value);

            // inspeciona via reflection (objeto anônimo)
            var valType = ok.Value!.GetType();
            var userProp = valType.GetProperty("User");
            var claimsProp = valType.GetProperty("Claims");

            Assert.NotNull(userProp);
            Assert.NotNull(claimsProp);

            var userName = (string)userProp!.GetValue(ok.Value)!;
            var claimsEnum = (IEnumerable<object>)claimsProp!.GetValue(ok.Value)!;

            Assert.Equal("alice", userName);
            Assert.True(claimsEnum.Cast<object>().Any());
        }
    }
}
