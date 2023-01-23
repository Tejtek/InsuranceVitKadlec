using InsuranceVitKadlec.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InsuranceVitKadlec
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            SetupAdmin(app);

            app.Run();
        }

        private static void SetupAdmin(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                // získání správce rolí
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                // získání správce uživatelù                
                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // vytvoøení role Admin když neexistuje
                if (!roleManager.RoleExistsAsync("Admin").Result) 
                {
                    var role = new IdentityRole();
                    role.Name = "Admin";
                    roleManager.CreateAsync(role).Wait(); //
                }
                
                string adminEmail = "admin@test.test";
                string adminPassword = "1_23456Ahoj";
                // Zkusí najít uživatele
                IdentityUser uzivatel = userManager.FindByEmailAsync(adminEmail).Result;
                // vytvoøí uživatele v pøípadì, že není
                if (uzivatel == null)
                {
                    userManager.CreateAsync(new IdentityUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail
                    }, adminPassword).Wait();
                    uzivatel = userManager.FindByEmailAsync(adminEmail).Result;
                }
                // pøiøazení uživatele do role Admin
                userManager.AddToRoleAsync(uzivatel, "Admin").Wait();
            }
            // Zdroj: https://www.itnetwork.cz/csharp/asp-net-core/zaklady/uzivatelske-role-v-aspnet-core-mvc-a-dokonceni-blogu
        }
    }
}