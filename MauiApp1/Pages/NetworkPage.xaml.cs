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
        TitleLb.FontSize = getValue + 5;
        IpLb.FontSize = getValue;
        IpEntr.FontSize = getValue;
        PortLb.FontSize = getValue;
        PortEntr.FontSize = getValue;
        PasswordLb.FontSize = getValue;
        PasswordEntr.FontSize = getValue;
    }
    

    private async void PingServer(object sender, EventArgs e)
    {
        if (!IsFlag) return;

        IsFlag = false;
        CheckIpAPort.IsInProgress = true;

        string ipAdress = IpEntr.Text;
        _ = int.TryParse(PortEntr.Text, out int port);
        if ((string.IsNullOrEmpty(ipAdress) || port == 0) || !IsValidIpAddress(ipAdress) || !IsValidPort(port))
        {
            IsFlag = true;
            CheckIpAPort.IsInProgress = false;
            await DisplayAlert(LocalizationResourceManager["AppName"].ToString(), LocalizationResourceManager["ErrorWithIPOrPort"].ToString(), "OK");
            return;
        }
        Task<bool> task = PingServerAsync(ipAdress, port);
        string? result = (await task) ? LocalizationResourceManager["Sucess"].ToString() : LocalizationResourceManager["DestHostUn"].ToString();
        await DisplayAlert(LocalizationResourceManager["AppName"].ToString(), result, "OK");

        IsFlag = true;
        CheckIpAPort.IsInProgress = false;
    }

    private bool IsValidIpAddress(string ipAdress)
    {
        Regex validateIPv4Regex = new Regex("^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");  // prints True
        return validateIPv4Regex.IsMatch(ipAdress);
    }

    private bool IsValidPort(int port) => (port >= 49152 && port <= 65535) ? true : false;

    private async Task<bool> PingServerAsync(string ipAddress, int port)
    {
        try
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync(ipAddress, port);
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
}