
using Infrastructure.Auth.Configuration.Option;
using Infrastructure.Auth.Context;
using Infrastructure.Auth.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Auth.Service
{
    public sealed class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AuthContext _db;
        private readonly JwtOptions _opt;

        public JwtTokenService(UserManager<IdentityUser> userManager, AuthContext db, IOptions<JwtOptions> opt)
        {
            _userManager = userManager; 
            _db = db; 
            _opt = opt.Value;
        }

        public async Task<(string, RefreshToken)> IssueTokensAsync(IdentityUser user, CancellationToken ct = default)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_opt.AccessTokenMinutes);

            var token = new JwtSecurityToken(
                issuer: _opt.Issuer,
                audience: _opt.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            var access = new JwtSecurityTokenHandler().WriteToken(token);
            var refresh = await CreateRefreshTokenAsync(Guid.Parse(user.Id), ct);
            return (access, refresh);
        }

        public async Task<(string, RefreshToken)> RotateRefreshAsync(string refreshToken, CancellationToken ct = default)
        {
            var token = await _db.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken, ct);
            if (token is null || token.Revoked || token.ExpiresAtUtc < DateTime.UtcNow)
                throw new SecurityTokenException("Invalid refresh token");

            var user = await _userManager.FindByIdAsync(token.UserId.ToString("D")) ?? throw new SecurityTokenException("User not found");
            token.Revoked = true;
            await _db.SaveChangesAsync(ct);

            return await IssueTokensAsync(user, ct);
        }

        private async Task<RefreshToken> CreateRefreshTokenAsync(Guid userId, CancellationToken ct)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var refreshToken = new RefreshToken()
            {
                UserId = userId,
                Token = token,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(_opt.RefreshTokenDays)
            };
            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync(ct);
            return refreshToken;
        }
    }
}
