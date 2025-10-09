

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Auth.Context.Seed
{
    public class DataSeeder
    {
        public static async Task SeedAsync(UserManager<IdentityUser> um, RoleManager<IdentityRole> rm)
        {
            foreach (var role in new[] { "admin", "user" })
                if (!await rm.RoleExistsAsync(role)) await rm.CreateAsync(new IdentityRole(role));

            var adminEmail = "admin@local";
            var admin = await um.FindByEmailAsync(adminEmail);
            if (admin is null)
            {
                admin = new IdentityUser { UserName = "admin", Email = adminEmail, EmailConfirmed = true };
                await um.CreateAsync(admin, "Admin@123");
                await um.AddToRolesAsync(admin, new[] { "admin" });
            }
        }
    }
}
