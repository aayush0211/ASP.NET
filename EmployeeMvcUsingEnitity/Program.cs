using EmployeeMvcUsingEnitity.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMvcUsingEnitity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<EmployeeContext>(options =>
                                   options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeContext")));

            var app = builder.Build();

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
                pattern: "{controller=EmployeesMvcs}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
