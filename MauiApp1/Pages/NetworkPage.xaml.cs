using MauiApp1.Extension;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace MauiApp1.Pages;

public partial class NetworkPage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    private bool IsFlag = true;
    public NetworkPage()
    {
        InitializeComponent();

        int getValue = Preferences.Get("FontSize", 20);
        TitleLabel.FontSize = getValue + 5;
        IPLabel.FontSize = getValue;
        IPEntry.FontSize = getValue;
        PortLabel.FontSize = getValue;
        PortEntry.FontSize = getValue;
        PasswordLabel.FontSize = getValue;
        PasswordEntry.FontSize = getValue;
    }
    

    private async void PingServer(object sender, EventArgs e)
    {
        if (!IsFlag) return;

        IsFlag = false;
        CheckIpPortButton.IsInProgress = true;

        string ipAdress = IPEntry.Text;
        _ = int.TryParse(PortEntry.Text, out int port);
        if ((string.IsNullOrEmpty(ipAdress) || port == 0) || !IsValidIpAddress(ipAdress) || !IsValidPort(port))
        {
            IsFlag = true;
            CheckIpPortButton.IsInProgress = false;
            await DisplayAlert(LocalizationResourceManager["AppName"].ToString(), LocalizationResourceManager["ErrorWithIPOrPort"].ToString(), "OK");
            return;
        }
        Task<bool> task = PingServerAsync(ipAdress, port);
        string? result = (await task) ? LocalizationResourceManager["Sucess"].ToString() : LocalizationResourceManager["DestHostUn"].ToString();
        await DisplayAlert(LocalizationResourceManager["AppName"].ToString(), result, "OK");

        IsFlag = true;
        CheckIpPortButton.IsInProgress = false;
    }

    private bool IsValidIpAddress(string ipAdress)
    {
        Regex validateIPv4Regex = new("^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");  // prints True
        return validateIPv4Regex.IsMatch(ipAdress);
    }

    private bool IsValidPort(int port) => port >= 49152 && port <= 65535;

    private async Task<bool> PingServerAsync(string ipAddress, int port)
    {
        try
        {
            using TcpClient client = new();
            await client.ConnectAsync(ipAddress, port);
            return true;
        }
        catch
        {
            return false;
        }
    }
}