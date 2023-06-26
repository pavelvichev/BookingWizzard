using Microsoft.AspNetCore.Identity;

namespace BookingWizard.IdentityServer.Configuration
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminName = "Pavel1";
            string password = "paswword1";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("guest") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("guest"));
            }
            if (await userManager.FindByNameAsync(adminName) == null)
            {
                IdentityUser admin = new IdentityUser { UserName = adminName };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }

            }
        }
    }
}
