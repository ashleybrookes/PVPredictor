using PVPredictor.WebApi.Models;
using PVPredictor.WebApi.Services;

namespace SolarCalculator
{
public class Program 
{
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.Configure<PVPredictorDatabaseSettings>(
            builder.Configuration.GetSection("PVPredictorDatabase"));               
        
            builder.Services.AddSingleton<CitiesService>();

        var app = builder.Build();
            app.MapControllers();

            app.Run();
    }

    public static IHostBuilder CreateHostBulder(string[] args ) =>

        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => 
            { 
                webBuilder.UseStartup<Startup>();
            } );

    private static void SetThreadCultureGB()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    }

}

}



 

