using MauiApp1.Extension;

namespace MauiApp1;

public partial class AppShell : Shell
{
    public LocalizationResourceManager LocalizationResourceManager
        => LocalizationResourceManager.Instance;
    public AppShell()
    {
        InitializeComponent();

        BindingContext = this;
    }
}
