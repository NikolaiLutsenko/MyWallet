using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWallet.Data;
using MyWallet.Profiles;
using System;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace MyWallet.Extensions
{
    public static class DIExtensions
    {
        private const string MYWALLET_DB_USER = nameof(MYWALLET_DB_USER);
        private const string MYWALLET_DB_USER_PASSWORD = nameof(MYWALLET_DB_USER_PASSWORD);
        private const string MYWALLET_DB_PORT = nameof(MYWALLET_DB_PORT);

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dbUser = Environment.GetEnvironmentVariable(MYWALLET_DB_USER) ?? throw new ArgumentNullException($"There isn't {MYWALLET_DB_USER} environment variable");
            var dbUserPassword = Environment.GetEnvironmentVariable(MYWALLET_DB_USER_PASSWORD) ?? throw new ArgumentNullException($"There isn't {MYWALLET_DB_USER_PASSWORD} environment variable");
            if (!Int32.TryParse(Environment.GetEnvironmentVariable(MYWALLET_DB_PORT), out var dbPort))
                dbPort = 3306;

            var connection = configuration.GetConnectionString("DefaultConnection")
                .Replace("{user}", dbUser)
                .Replace("{password}", dbUserPassword)
                .Replace("{port}", dbPort.ToString());

            services.AddDbContext<MyWalletContext>(options =>
                options.UseMySql(
                    connection,
                     new MySqlServerVersion(new Version(8, 0, 23)),
                     mySqlOptions => mySqlOptions.MigrationsAssembly("MyWallet.Data")
                )
            );

            services
                .AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;

                    options.Password.RequiredLength = 5;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<MyWalletContext>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.AddAutoMapper(typeof(HistoryLineProfile).Assembly);

            return services;
        }
    }
}
