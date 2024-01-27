using MauiApp1.Extension;
using System.Globalization;

namespace MauiApp1.Pages;

public partial class SettingsPage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    private int counter = 0;
    private bool IsFlag = false;

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

        int getValue = Preferences.Get("FontSize", 20);
        TitleSetLb.FontSize = getValue + 5;
        chatSettLb.FontSize = getValue;
        networkLb.FontSize = getValue;
        languageLb.FontSize = getValue;
        currentLangLb.FontSize = getValue;
        TitleHelpLb.FontSize = getValue + 5;
        ghLb.FontSize = getValue;
        mailLB.FontSize = getValue;
        policyLB.FontSize = getValue;
        AppVersionLB.FontSize = getValue - 5;



        IsFlag = Preferences.Get("IsAdminPanel", false);
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
        if (IsFlag) return;
        if (counter > 5)
        {
            Preferences.Set("IsAdminPanel", true);
            IsFlag = true;
            await DisplayAlert(LocalizationResourceManager["AppName"].ToString(), LocalizationResourceManager["ReloadApp"].ToString(), "OK");
        }
        else if (counter <= 5) counter++;
    }

    public async void UpdateTime()
    {
        while(true)
        {
            string getLanguage = Preferences.Get("LanguageApp", "ru-RU");
            CultureInfo currentCulture = new(getLanguage);
            string currentLanguage = currentCulture.DisplayName;
            currentLangLb.Text = $"{currentLanguage[0].ToString().ToUpper()}{currentLanguage.Substring(1)}";
            await Task.Delay(1000);
        }
    }
}