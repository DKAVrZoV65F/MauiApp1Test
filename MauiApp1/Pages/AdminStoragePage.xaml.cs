using MauiApp1.Extension;

namespace MauiApp1.Pages;

public partial class AdminStoragePage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;
    public AdminStoragePage() => InitializeComponent();
}