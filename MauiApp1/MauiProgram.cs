using MauiApp1.Pages;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace MauiApp1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Carlito-Bold.ttf", "BoldFont");
                fonts.AddFont("Carlito-Regular.ttf", "RegularFont");
            })

            .Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.ConfigureLifecycleEvents(events =>
        {
#if ANDROID
            events.AddAndroid(android => android.OnCreate((activity, bundle) => activity.Window?.SetStatusBarColor(Android.Graphics.Color.Black)));
#endif
        });
        return builder.Build();
    }
}