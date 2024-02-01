﻿using MauiApp1.Extension;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace MauiApp1.Pages;

public partial class NetworkPage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    private bool IsFlag = true;
    private bool task = false;
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

        IPEntry.Text = Preferences.Get("SavedIpServer", "");
        PortEntry.Text = Preferences.Get("SavedPortServer", 0).ToString();
        PasswordEntry.Text = Preferences.Get("SavedPasswordServer", "");
    }


    private async void PingServer(object sender, EventArgs e)
    {
        if (!IsFlag) return;

        IsFlag = false;
        CheckIpPortButton.IsInProgress = true;

        string ipAddress = IPEntry.Text;
        _ = int.TryParse(PortEntry.Text, out int port);
        if ((string.IsNullOrEmpty(ipAddress) || port == 0) || !IsValidIpAddress(ipAddress) || !IsValidPort(port))
        {
            IsFlag = true;
            CheckIpPortButton.IsInProgress = false;
            await DisplayAlert(LocalizationResourceManager["AppName"].ToString(), LocalizationResourceManager["ErrorWithIPOrPort"].ToString(), "OK");
            return;
        }
        string password = PasswordEntry.Text;

        await PingServerAsync(ipAddress, port, password);

        string? result = (task) ? LocalizationResourceManager["Sucess"].ToString() : LocalizationResourceManager["DestHostUn"].ToString();
        await DisplayAlert(LocalizationResourceManager["AppName"].ToString(), result, "OK");

        task = false;
        IsFlag = true;
        CheckIpPortButton.IsInProgress = false;
    }

    private bool IsValidIpAddress(string ipAdress)
    {
        Regex validateIPv4Regex = new("^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");  // prints True
        return validateIPv4Regex.IsMatch(ipAdress);
    }

    private bool IsValidPort(int port) => port >= 49152 && port <= 65535;

    private async Task PingServerAsync(string ipAddress, int port, string password)
    {
        if (string.IsNullOrEmpty(ipAddress)) return;


        using TcpClient tcpClient = new();
        try
        {
            await tcpClient.ConnectAsync(ipAddress, port);
            var stream = tcpClient.GetStream();

            // буфер для входящих данных
            var response = new List<byte>();
            NetworkStream networkStream = tcpClient.GetStream();

            int bytesRead = 10; // для считывания байтов из потока
            await stream.WriteAsync(Encoding.UTF8.GetBytes("PING\0"));
            while ((bytesRead = stream.ReadByte()) != '\0')
            {
                // добавляем в буфер
                response.Add((byte)bytesRead);
            }

            var translation = Encoding.UTF8.GetString(response.ToArray());
            if ("SUCCESS" == translation)
            {
                Preferences.Set("SavedIpServer", ipAddress);
                Preferences.Set("SavedPortServer", port);
                Preferences.Set("SavedPasswordServer", password); 
                task = true;
            }

            response.Clear();
            networkStream.Close();
        }
        catch
        {
        }
        finally
        {
            tcpClient.Close();
        }
    }
}