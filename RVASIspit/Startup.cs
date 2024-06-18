using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using RVASIspit.Models;

[assembly: OwinStartupAttribute(typeof(RVASIspit.Startup))]
namespace RVASIspit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new IdentityRole { Name = "Admin" };
                    roleManager.Create(role);
                }

                if (!roleManager.RoleExists("Korisnik"))
                {
                    var role = new IdentityRole { Name = "Korisnik" };
                    roleManager.Create(role);
                }

                // Dodelite rolu Admin prvom korisniku
                var user = userManager.FindByEmail("admin@raf.rs");
                if (user != null)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }

                var user2 = userManager.FindByEmail("korisnik@raf.rs");
                if(user2 != null)
                {
                    userManager.AddToRole(user.Id, "Korisnik");
                }
            }
        }
    }
}
