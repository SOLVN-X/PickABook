using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PickABookWeb.Data;
using PickABookWeb.Models;

namespace PickABookWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DBContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<DefaultUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DBContext>();
            builder.Services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = 104857600; // 100MB limit
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsProduction())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
                        app.UseAuthentication();;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
           


        }
    }
}