using MauiApp1.Extension;

namespace MauiApp1.Pages;

public partial class ThemePage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;
    public ThemePage() => InitializeComponent();
}