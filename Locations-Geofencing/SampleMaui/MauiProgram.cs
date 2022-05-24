using Shiny;

namespace Sample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp
            .CreateBuilder()
            .UseMauiApp<App>()
            .UseShiny()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("fasolid900.ttf", "FAS");
                fonts.AddFont("faregular400.ttf", "FAR");
            });

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<LogsPage>();
        builder.Services.AddTransient<LogsViewModel>();
        builder.Services.AddTransient<CreatePage>();
        builder.Services.AddTransient<CreateViewModel>();
        builder.Services.AddTransient<ListPage>();
        builder.Services.AddTransient<ListViewModel>();

        builder.Services.AddSingleton<SampleSqliteConnection>();

        builder.Services.AddGeofencing<GeofenceDelegate>();

        //// if you need some very real-time geofencing, you want to use below - this will really hurt your user's battery
        ////services.UseGpsDirectGeofencing<GeofenceDelegate>();

        //// let's send some notifications from our geofence
        builder.Services.AddNotifications();

        //// we use this in the example, it isn't needed for geofencing in general
        builder.Services.AddGps();

        var app = builder.Build();
        app.RunShiny();

        return app;
    }
}