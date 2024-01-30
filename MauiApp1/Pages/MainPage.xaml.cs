using MauiApp1.Extension;
using System.Net.Sockets;
using System.Text;

namespace MauiApp1.Pages;

public partial class MainPage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    string text = "";
    string textFromServer = "";
    bool IsFlag = true;

    private List<string> IpAddress = ["192.168.55.101", "192.168.55.102", "192.168.55.103", "192.168.55.104", "192.168.55.105", "192.168.48.250", "192.168.48.251", "192.168.48.252"];
    private string ipServer;
    private int portServer = 55555;


    readonly Random rnd = new();
    readonly int minValue = 10;
    readonly int maxValue = 50;

    public MainPage()
    {
        bool IsPolicyRead = Preferences.Get("IsPolicyRead", true);
        if (IsPolicyRead) GoToPolicy();

        InitializeComponent();

        int getValue = Preferences.Get("FontSize", 20);
        InfoLabel.FontSize = getValue;
        ResultEditor.FontSize = getValue;
        QueryEditor.FontSize = getValue;
    }

    private async void GoToPolicy() => await Navigation.PushModalAsync(new PolicyPage());

    private async void SendText_Tapped(object sender, EventArgs e)
    {
        text = QueryEditor.Text;
        if (string.IsNullOrEmpty(text) || !IsFlag) return;

        IsFlag = false;
        SendTextButton.IsInProgress = true;
        SendPictureButton.IsInProgress = true;
        QueryEditor.Text = "";

        ResultEditor.Text += "You: ";
        foreach (var item in text)
        {
            ResultEditor.Text += item;
            await Task.Delay(rnd.Next(minValue, maxValue));
        }
        ResultEditor.Text += '\n';

        await SendText(text);
        ResultEditor.Text += "Server: ";
        foreach (var item in textFromServer)
        {
            ResultEditor.Text += item;
            await Task.Delay(rnd.Next(minValue, maxValue));
        }

        SendTextButton.IsInProgress = false;
        SendPictureButton.IsInProgress = false;
        text = "";
        IsFlag = true;
    }

    private void QueryEditor_Changed(object sender, TextChangedEventArgs e)
    {
        if (SendPictureButton == null || SendTextButton == null) return;

        SendPictureButton.IsVisible = string.IsNullOrEmpty(QueryEditor.Text);
        SendTextButton.IsVisible = !string.IsNullOrEmpty(QueryEditor.Text);
    }

    private async void SendPicture_Tapped(object sender, EventArgs e)
    {
        if (!IsFlag) return;

        IsFlag = false;
        SendTextButton.IsInProgress = true;
        SendPictureButton.IsInProgress = true;


        string path = await GetPicturePath();
        if (string.IsNullOrEmpty(path))
        {
            SendTextButton.IsInProgress = false;
            SendPictureButton.IsInProgress = false;
            IsFlag = true;
            return;
        }

        ResultEditor.Text += "You pick a photo \"";
        foreach (var item in path)
        {
            ResultEditor.Text += item;
            await Task.Delay(rnd.Next(minValue, maxValue));
        }
        ResultEditor.Text += "\"\n";
        await SendPicture(path);

        ResultEditor.Text += "Server: ";
        foreach (var item in textFromServer)
        {
            ResultEditor.Text += item;
            await Task.Delay(rnd.Next(minValue, maxValue));
        }

        SendTextButton.IsInProgress = false;
        SendPictureButton.IsInProgress = false;
        IsFlag = true;
    }

    private async Task<string> GetPicturePath()
    {
        FileResult myPhoto = await MediaPicker.Default.PickPhotoAsync();
        if (myPhoto == null) return "";
        string localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
        using Stream sourceStream = await myPhoto.OpenReadAsync();
        using FileStream localFileStream = File.OpenWrite(localFilePath);
        await sourceStream.CopyToAsync(localFileStream);
        localFileStream.Close();
        return localFilePath;
    }

    private async Task SendPicture(string path)
    {
        using TcpClient tcpClient = new();
        ipServer = Preferences.Get("SavedIpServer", "");
        if (string.IsNullOrEmpty(ipServer))
        {
            textFromServer = "Error with server";
            return;
        }
        textFromServer = "";

        await tcpClient.ConnectAsync(ipServer, portServer);
        var stream = tcpClient.GetStream();

        // буфер для входящих данных
        var response = new List<byte>();
        NetworkStream networkStream = tcpClient.GetStream();
        FileStream fileStream = null;

        int bytesRead = 10; // для считывания байтов из потока
        await stream.WriteAsync(Encoding.UTF8.GetBytes("IMAGE" + "\0"));

        string fileName = path;
        FileInfo fileInfo = new FileInfo(fileName);
        long fileSize = fileInfo.Length;
        await stream.WriteAsync(Encoding.UTF8.GetBytes(fileSize + "\0"));

        byte[] buffer = new byte[1024];
        int bytesReadImg;
        string filePath = path;
        fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        while ((bytesReadImg = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            tcpClient.GetStream().Write(buffer, 0, bytesReadImg);
        }
        fileStream.Close();

        while ((bytesRead = stream.ReadByte()) != '\0')
        {
            // добавляем в буфер
            response.Add((byte)bytesRead);
        }
        var translation = Encoding.UTF8.GetString(response.ToArray());
        textFromServer = translation + '\n';
        response.Clear();
        networkStream.Close();
    }

    private async Task SendText(string text)
    {
        using TcpClient tcpClient = new();
        ipServer = Preferences.Get("SavedIpServer", "");

        if (string.IsNullOrEmpty(ipServer))
        {
            textFromServer = "Error with server";
            return;
        }
        textFromServer = "";
        await tcpClient.ConnectAsync(ipServer, portServer);
        var stream = tcpClient.GetStream();

        // буфер для входящих данных
        var response = new List<byte>();
        NetworkStream networkStream = tcpClient.GetStream();
        FileStream fileStream = null;

        int bytesRead = 10; // для считывания байтов из потока
        await stream.WriteAsync(Encoding.UTF8.GetBytes("TEXT" + "\0"));

        await stream.WriteAsync(Encoding.UTF8.GetBytes(text + '\0'));
        try
        {
            while ((bytesRead = stream.ReadByte()) != '\0')
            {
                // добавляем в буфер
                response.Add((byte)bytesRead);
            }
        }
        finally
        {
            textFromServer = "Error on server 2\n";
        }

        var translation = Encoding.UTF8.GetString(response.ToArray());
        //logEditor.Text += $"\nWord: {translation}";
        textFromServer = translation + '\n';
        response.Clear();
        networkStream.Close();
    }
}
