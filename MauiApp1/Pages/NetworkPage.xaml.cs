using MauiApp1.Extension;

namespace MauiApp1.Pages;

public partial class NetworkPage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;
    public NetworkPage() => InitializeComponent();
}