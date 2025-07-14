using energyconsumptiontracker.Application.DataImport;
using meterreadingapi.Data;

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

            // Application layer
            builder.Services.AddSingleton<ICsvFileProcessor, CsvFileProcessor>();

            //persistence layer
            builder.Services.AddSingleton<IMeterReadingPersistence, MeterReadingPersistence>();

            // api layer
            builder.Services.AddSingleton<MeterReadingController>();


            var app = builder.Build();

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

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
