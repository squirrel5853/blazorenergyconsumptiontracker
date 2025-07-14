using energyconsumptiontracker.Application.DataImport;
using meterreadingapi.Services;

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
            builder.Services.AddScoped<MeterReadingService>();

            // Application layer
            builder.Services.AddSingleton<ICsvFileProcessor, CsvFileProcessor>();

            //persistence layer
            builder.Services.AddSingleton<IMeterReadingPersistence, MeterReadingPersistence>();

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

            app.Run();
        }
    }
}
