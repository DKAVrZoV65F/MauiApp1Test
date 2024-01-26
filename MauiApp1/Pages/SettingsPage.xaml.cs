using MauiApp1.Extension;
using System.Globalization;

namespace MauiApp1.Pages;

public partial class SettingsPage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    private int counter = 0;
    private bool IsFlag = true;

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

    public SettingsPage()
    {
        InitializeComponent();
#if ANDROID
        AppVersionLB.Text = (LocalizationResourceManager["AppName"].ToString() + ' ' + LocalizationResourceManager["For"].ToString() + " Android v0.1");
#elif IOS
        AppVersionLB.Text = (LocalizationResourceManager["AppName"].ToString() + ' ' + LocalizationResourceManager["For"].ToString() + " IOS v0.1");
#elif MACCATALYST
        AppVersionLB.Text = (LocalizationResourceManager["AppName"].ToString() + ' ' + LocalizationResourceManager["For"].ToString() + " Maccatalyst v0.1");
#elif WINDOWS
        AppVersionLB.Text = (LocalizationResourceManager["AppName"].ToString() + ' ' + LocalizationResourceManager["For"].ToString() + " Windows v0.1");
#endif
        UpdateTime();
    }

    private async void networkLb_Tapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new NetworkPage());

    private async void languageLb_Tapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new LanguagePage());

    private async void themeLb_Tapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new ThemePage());

    private async void ghLb_Tapped(object sender, TappedEventArgs e) => await Launcher.OpenAsync("https://github.com/DKAVrZoV65F/MLFoodAnalyzer");

    private async void policyLb_Tapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new PolicyPage());

    private async void mailLb_Tapped(object sender, TappedEventArgs e)
    {
        await Clipboard.SetTextAsync("gw9ckwfsp@mozmail.com");
        await DisplayAlert(LocalizationResourceManager["AppName"].ToString(), LocalizationResourceManager["MailMessage"].ToString(), "OK");
    }

    private async void secret_Tapped(object sender, TappedEventArgs e)
    {
        if (counter > 5 && IsFlag)
        {
            IsFlag = false;
            await DisplayAlert(LocalizationResourceManager["AppName"].ToString(), LocalizationResourceManager["ReloadApp"].ToString(), "OK");
            //Preferences.Set("IsAdminPanel", true);
        }
        else if (counter <= 5) counter++;
    }

    public async void UpdateTime()
    {
        //int counter2 = 0;
        while(true)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            string currentLanguage = currentCulture.DisplayName;
            LanguageLb.Text = $"{currentLanguage[0].ToString().ToUpper()}{currentLanguage.Substring(1)}";
            /*counter2++;
            LanguageLb.Text = counter2.ToString();*/

            await Task.Delay(1000);
        }
    }
}