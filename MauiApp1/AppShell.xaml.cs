using MauiApp1.Extension;
using System.Globalization;

namespace MauiApp1;

public partial class AppShell : Shell
{
    public LocalizationResourceManager LocalizationResourceManager
        => LocalizationResourceManager.Instance;
    public AppShell()
    {
        InitializeComponent();

        BindingContext = this;
        AdminPanel.IsVisible = Preferences.Get("IsAdminPanel", false);

        CultureInfo cultureInfo = new(Preferences.Get("LanguageApp", "ru-RU"));
        LocalizationResourceManager.Instance.SetCulture(cultureInfo);

        switch (Preferences.Get("ThemeApp", "Default"))
        {
            case "Light":
                Application.Current.UserAppTheme = AppTheme.Light;
                break;
            case "Dark":
                Application.Current.UserAppTheme = AppTheme.Dark;
                break;
            default:
                Application.Current.UserAppTheme = AppTheme.Unspecified;
                break;
        }
    }
}
