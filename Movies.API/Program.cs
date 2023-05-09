using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.API.Data;
using Movies.API.Data;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<MoviesContext>(options =>
            options.UseInMemoryDatabase("Movies" ?? throw new InvalidOperationException("Connection string 'MoviesAPIContext' not found.")));

        // Add services to the container.
         
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        seedDatabase(app);
        app.Run();


        void seedDatabase(WebApplication host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var moviesContext = services.GetRequiredService<MoviesContext>();
                MoviesContextSeed.SeedAsync(moviesContext);
            }
        }
    }
}