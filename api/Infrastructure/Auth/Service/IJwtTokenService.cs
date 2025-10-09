using Infrastructure.Auth.Model;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Auth.Service
{
    public interface IJwtTokenService
    {
        Task<(string accessToken, RefreshToken refresh)> IssueTokensAsync(IdentityUser user, CancellationToken ct = default);
        Task<(string accessToken, RefreshToken refresh)> RotateRefreshAsync(string refreshToken, CancellationToken ct = default);
    }
}
