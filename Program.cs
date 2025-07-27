using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddLocalization(options => options.ResourcesPath = "Resource");

            // Add services to the container.
            builder.Services.AddControllersWithViews()
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();


            builder.Services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLconnections"));
            });


            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IShelfRepository, ShelfRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<LibraryDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/LogIn"; 
                });
            builder.Services.Configure<RequestLocalizationOptions>(option =>
            {
                var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("ar-JO") };
                option.DefaultRequestCulture = new RequestCulture(("en-US"), ("ar-JO"));
                option.SupportedCultures = supportedCultures;
                option.SupportedUICultures = supportedCultures;t
            });
            var app = builder.Build();


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var supportedCultures = new[] {new CultureInfo ("en-US"),new CultureInfo ("ar-JO") };

            var localizationOptions = new RequestLocalizationOptions
            {

                DefaultRequestCulture = new RequestCulture("ar-JO"),


                SupportedCultures = supportedCultures,

                SupportedUICultures = supportedCultures,


                RequestCultureProviders = new[]
                {
                    new CookieRequestCultureProvider()
                }
            };

            app.UseRequestLocalization(localizationOptions);

 
            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=LogIn}/{id?}");

            ShelfSeedData.EnsurePopulated(app);
            BookSeedData.EnsurePopulated(app);


            app.Run();
        }
    }
}
