using LucentRP.Features.Authentication;
using LucentRP.Features.Users;
using LucentRP.Shared.Models.Authentication;
using MySql.Data.MySqlClient;

namespace LucentRP
{
    /// <summary>
    /// The main class for the lucentrp backend.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The appsettings.json configuration file.
        /// </summary>
        public static IConfigurationRoot AppSettings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        /// <summary>
        /// The entry point of the program.
        /// </summary>
        /// <param name="args">Arguments passed in through the command line.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<ILogger, Logger<Program>>();
            builder.Services.AddSingleton(_ => AppSettings);
            builder.Services.AddSingleton(_ => new MySqlConnection(AppSettings["ConnectionStrings:Default"]));
            builder.Services.AddSingleton(serviceProvider => new AuthenticationMiddleware(serviceProvider));
            builder.Services.AddSingleton(serviceProvider => new TokenManager(serviceProvider.GetRequiredService<RSAKeyPair>()));
            builder.Services.AddSingleton(_ => new RSAKeyPair(
                                                    AuthUtilities.LoadKey(
                                                        Path.Combine(Directory.GetCurrentDirectory(), AppSettings["Authentication:PublicKey"])
                                                    ),
                                                    AuthUtilities.LoadKey(
                                                        Path.Combine(Directory.GetCurrentDirectory(), AppSettings["Authentication:PrivateKey"])
                                                    )
                                                )
            );

            UserService.Register(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
            }

            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "api/v1/{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}