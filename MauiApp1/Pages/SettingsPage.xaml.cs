using MauiApp1.Extension;

namespace MauiApp1.Pages;

public partial class SettingsPage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

     /*readonly string text = "Very long text! )";
     bool IsFlag = true;
     readonly List<string> ipAddress = ["192.168.1.1", "192.168.55.99", "192.168.55.102"];
     readonly List<string> ipResult = [];
     readonly string ipServer = "192.168.1.68";
     readonly int portServer = 8888;
     string path = "";
     string command = "IMAGE";
     string currentLanguage = "English";
     readonly string RUSSIAN = "Русский";
     readonly string ENGLISH = "English";*/

    public SettingsPage()
    {
        InitializeComponent();

        /*Ping ping = new();
        foreach (string item in ipAddress)
        {
            PingReply pingReply = ping.Send(item);
            ipResult.Add(pingReply.Status.ToString());
        }

        foreach (string item in ipResult)
        {
            await DisplayAlert(Title, item, "OK");
        }*/
    }
}