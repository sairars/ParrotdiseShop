using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.Utilities;
using ParrotdiseShop.Persistence;
using ParrotdiseShop.Persistence.Data;
using ParrotdiseShop.Persistence.DbInitializer;
using Stripe;

namespace ParrotdiseShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

			builder.Services.AddAuthentication().AddFacebook(options =>
			{
				options.AppId = builder.Configuration["Facebook:AppId"];
				options.AppSecret = builder.Configuration["Facebook:AppSecret"];
			});

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
                                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
           
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IEmailSender, EmailSender>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();

            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

			builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.LogoutPath = "/Identity/Account/Logout";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

			StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:StripeSecretKey").Get<String>();

			app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(name: "home",
                pattern: "Customer/Home/Index/{categoryId?}",
                defaults: new { area="Customer", controller = "Home", action = "Index" });
             
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            SeedDatabase(app);

            app.Run();
        }

        private static void SeedDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            dbInitializer.Initialize();
        }
    }
}