namespace SolarCalculator
{
public class Program 
{
    public static void Main(string[] args) {
        var builder = CreateHostBulder(args);

        
        var app = builder.Build();
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



 

