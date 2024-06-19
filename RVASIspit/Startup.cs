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

                // Kreiraj role ako ne postoje
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

                // Dodaj admina ako ne postoji i dodeli mu rolu Admin
                var adminUser = userManager.FindByEmail("admin@raf.rs");
                if (adminUser != null && !userManager.IsInRole(adminUser.Id, "Admin"))
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }

                // Dodaj korisnika ako ne postoji i dodeli mu rolu Korisnik
                var korisnikUser = userManager.FindByEmail("korisnik@raf.rs");
                if (korisnikUser != null && !userManager.IsInRole(korisnikUser.Id, "Korisnik"))
                {
                    userManager.AddToRole(korisnikUser.Id, "Korisnik");
                }
            }
        }
    }
}
