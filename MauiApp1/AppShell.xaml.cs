﻿using MauiApp1.Extension;
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

        Application.Current.UserAppTheme = Preferences.Get("ThemeApp", "Default") switch
        {
            "Light" => AppTheme.Light,
            "Dark" => AppTheme.Dark,
            _ => AppTheme.Unspecified,
        };
    }
}
