using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Auth.Model;

public class UserApplication: IdentityUser<Guid>
{
    public Guid UserId { get; private set; }

    public UserApplication(Guid userId)
    {
        UserId = userId;
    }

    public UserApplication()
    {
        
    }
}