using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Auth.Model;

public class RoleApplication : IdentityRole<Guid>
{
    public Guid RoleId { get; private set; }

    public RoleApplication(Guid roleId)
    {
        RoleId = roleId;
    }
}