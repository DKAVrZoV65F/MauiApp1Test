using MauiApp1.Extension;

namespace MauiApp1.Pages;

public partial class MainPage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    string text = "";
    bool IsFlag = true;
    readonly Random rnd = new();
    readonly int minValue = 100;
    readonly int maxValue = 500;

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


        ResultEditor.Text += "You pick a photo \" ";
        foreach (var item in path)
        {
            ResultEditor.Text += item;
            await Task.Delay(rnd.Next(minValue, maxValue));
        }
        ResultEditor.Text += "\"\n";



        // if (!File.Exists(path)) return;

        /*using TcpClient tcpClient = new();
        await tcpClient.ConnectAsync(ipServer, portServer);
        var stream = tcpClient.GetStream();

        // буфер для входящих данных
        var response = new List<byte>();
        NetworkStream networkStream = tcpClient.GetStream();
        FileStream fileStream = null;

        int bytesRead = 10; // для считывания байтов из потока
        await stream.WriteAsync(Encoding.UTF8.GetBytes(command + "\0"));



        string fileName = localFilePath;
        FileInfo fileInfo = new FileInfo(fileName);
        long fileSize = fileInfo.Length;
        //await DisplayAlert(Title, fileSize.ToString(), "OK");
        await stream.WriteAsync(Encoding.UTF8.GetBytes(fileSize + "\0"));
        if (command == "IMAGE")
        {
            byte[] buffer = new byte[1024];
            int bytesReadImg;
            string filePath = localFilePath;
            fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            while ((bytesReadImg = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                tcpClient.GetStream().Write(buffer, 0, bytesReadImg);
            }
            fileStream.Close();
        }

        //logEditor.Text = "Send image. Close image. Waiting for message...";

        while ((bytesRead = stream.ReadByte()) != '\0')
        {
            // добавляем в буфер
            response.Add((byte)bytesRead);
        }
        var translation = Encoding.UTF8.GetString(response.ToArray());
        //logEditor.Text += $"\nWord: {translation}";
        await DisplayAlert(Title, translation, "OK");
        //logEditor.Text = "";
        response.Clear();
        networkStream.Close();*/


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
}
