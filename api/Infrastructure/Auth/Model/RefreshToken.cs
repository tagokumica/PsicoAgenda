
namespace Infrastructure.Auth.Model
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } = default!;
        public string Token { get; set; } = default!;
        public DateTime ExpiresAtUtc { get; set; }
        public bool Revoked { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public RefreshToken(Guid id, Guid userId, string token, DateTime expiresAtUtc, bool revoked, DateTime createdAtUtc)
        {
            Id = id;
            UserId = userId;
            Token = token;
            ExpiresAtUtc = expiresAtUtc;
            Revoked = revoked;
            CreatedAtUtc = createdAtUtc;
        }
        public RefreshToken() { }
    }
}
