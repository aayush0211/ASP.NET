using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.CodeAnalysis.Options;

namespace MVCWeb2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            //Add Service to Create Session Variable
              builder.Services.AddSession(Options =>
            {
                Options.IdleTimeout = TimeSpan.FromSeconds(50);
                Options.Cookie.HttpOnly = true; // Cookie Only uses for server side
                Options.Cookie.IsEssential = true; // Mandatory
            });

            var app = builder.Build();
            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}