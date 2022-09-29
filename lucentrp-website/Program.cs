namespace lucentrp_website
{
    /// <summary>
    /// The main class for the lucentrp backend.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>
        /// <param name="args">Arguments passed in through the command line.</param>
        public static void Main(string[] args)
        {
            // Create a web application builder.
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Register serivces with the builder.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Build the application.
            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Add middleware.
            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Register API endpoints.
            app.MapControllers();

            // Start the application.
            app.Run();
        }
    }
}