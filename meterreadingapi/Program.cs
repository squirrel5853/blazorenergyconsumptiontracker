using energyconsumptiontracker.Application.DataImport;
using energyconsumptiontracker.Domain;
using energyconsumptiontracker.Persistence;
using meterreadingapi.Controllers;
using Microsoft.EntityFrameworkCore;

namespace meterreadingapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddControllers();

            // blazor frontend integration
            builder.Services.AddScoped<MeterReadingController>();

            // Application layer
            builder.Services.AddScoped<ICsvFileProcessor, CsvFileProcessor>();

            //persistence layer
            builder.Services.AddScoped<IMeterReadingPersistence, MeterReadingPersistence>();
            builder.Services.AddScoped<ICustomerAccountPersistence, CustomerAccountPersistence>();

            builder.Services.AddScoped<DatabaseSeeder>();

            builder.Services.AddDbContext<MeterReadingDbContext>(options =>
                options.UseSqlite("Data Source=meterreadings.db"));

            SQLitePCL.Batteries.Init();

            var app = builder.Build();

            // Request logging middleware — early in pipeline
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
                await next();
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            // Register controller endpoints *before* fallback routes
            app.MapControllers();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            //intialise data
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MeterReadingDbContext>();
                db.Database.EnsureCreated();

                var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();

                var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "test_accounts.csv");
                seeder.SeedAsync(csvPath).Wait();
            }

            app.Run();
        }
    }
}
