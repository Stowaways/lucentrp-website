using lucentrp.Features.Authentication;
using lucentrp.Features.Users;
using lucentrp.Shared.Models.Authentication;
using MySql.Data.MySqlClient;

namespace lucentrp
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
            // Create a web application builder.
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Register console logging with the builder.
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Register serivces with the builder.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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

            // Build the application.
            WebApplication app = builder.Build();
            app.Services.GetRequiredService<MySqlConnection>().Open();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Add middleware.
            app.UseHttpsRedirection();
            app.Use(app.Services.GetRequiredService<AuthenticationMiddleware>().Authenticate);

            // Register API endpoints.
            app.MapControllers();

            // Start the application.
            app.Run();
        }
    }
}