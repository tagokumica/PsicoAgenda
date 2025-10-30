using Infrastructure.Auth.Model;

namespace Infrastructure.Auth.Service
{
    public interface IJwtTokenService
    {
        Task<(string accessToken, RefreshToken refresh)> IssueTokensAsync(UserApplication user, CancellationToken ct = default);
        Task<(string accessToken, RefreshToken refresh)> RotateRefreshAsync(string refreshToken, CancellationToken ct = default);
    }
}
