using MauiApp1.Extension;

namespace MauiApp1.Pages;

public partial class SettingsPage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    int counter = 0;

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
    readonly string ENGLISH = "English";

    FormattedString formattedString = new FormattedString();
        formattedString.Spans.Add(new Span
        {
            Text = "Сегодня ",
            FontSize = 22
        });
        formattedString.Spans.Add(new Span
        {
            Text = "хорошая",
            TextColor = Colors.DarkRed,
            BackgroundColor = Colors.LightPink,
        });
        formattedString.Spans.Add(new Span
        {
            Text = " погода!",
            FontAttributes = FontAttributes.Bold
        });
    testLabel.FormattedText = formattedString;

    Ping ping = new();
    foreach (string item in ipAddress)
    {
        PingReply pingReply = ping.Send(item);
        ipResult.Add(pingReply.Status.ToString());
    }

    foreach (string item in ipResult)
    {
        await DisplayAlert(Title, item, "OK");
    }*/

    public SettingsPage() => InitializeComponent();

    private async void networkLb_Tapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new NetworkPage());

    private async void languageLb_Tapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new LanguagePage());

    private async void themeLb_Tapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new ThemePage());

    private async void ghLb_Tapped(object sender, TappedEventArgs e) => await Launcher.OpenAsync("https://github.com/DKAVrZoV65F/MLFoodAnalyzer");

    private async void mailLb_Tapped(object sender, TappedEventArgs e)
    {
        await Clipboard.SetTextAsync("gw9ckwfsp@mozmail.com");
        await DisplayAlert(Title, "Mail copied in clipboard", "OK");
    }

    private async void secret_Tapped(object sender, TappedEventArgs e) {
        if (counter > 5)
        {
            await DisplayAlert(Title, "Please reload app", "OK");
            //Preferences.Set("IsAdminPanel", true);
        }
        else if (counter <= 5) counter++;
    }
}