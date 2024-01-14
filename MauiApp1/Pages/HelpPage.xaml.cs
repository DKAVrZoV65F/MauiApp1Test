using System.Net.NetworkInformation;

namespace MauiApp1.Pages;

public partial class HelpPage : ContentPage
{
    readonly string text = "Very long text! )";
    bool IsFlag = true;
    readonly List<string> ipAddress = ["192.168.1.1", "192.168.55.99", "192.168.55.102"];
    readonly List<string> ipResult = [];

    public HelpPage()
    {
        InitializeComponent();
    }

    private async void TestBtn_Tapped(object sender, EventArgs e)
    {
        if (!IsFlag) return;
        IsFlag = false;
        TestBtn.IsInProgress = true;

        foreach (var item in text)
        {
            testEntry.Text += item;
            await Task.Delay(100);
        }

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


        TestBtn.IsInProgress = false;
        IsFlag = true;
    }
}