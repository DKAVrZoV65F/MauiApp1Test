using MauiApp1.Extension;
using System.Net.Sockets;

namespace MauiApp1.Pages;

public partial class MainPage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    string text = "";
    bool IsFlag = true;
    Random rnd = new Random();
    int minValue = 100;
    int maxValue = 500;

    public MainPage()
    {
        InitializeComponent();

        if (true) GoToPolicy();
    }

    private async void GoToPolicy() => await Navigation.PushModalAsync(new PolicyPage());

    private async void btnText_Tapped(object sender, EventArgs e)
    {
        text = TextEntryTest.Text;
        if (string.IsNullOrEmpty(text) || !IsFlag) return;

        IsFlag = false;
        btnText.IsInProgress = true;
        btnPicture.IsInProgress = true;
        TextEntryTest.Text = "";

        TextLabelTest.Text += "You: ";
        foreach (var item in text)
        {
            TextLabelTest.Text += item;
            await Task.Delay(rnd.Next(minValue, maxValue));
        }
        TextLabelTest.Text += '\n';

        btnText.IsInProgress = false;
        btnPicture.IsInProgress = false;
        text = "";
        IsFlag = true;
    }

    private void TextEntryTest_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (btnPicture == null || btnText == null) return;

        btnPicture.IsVisible = string.IsNullOrEmpty(TextEntryTest.Text);
        btnText.IsVisible = !string.IsNullOrEmpty(TextEntryTest.Text);
    }

    private async void btnPicture_Tapped(object sender, EventArgs e)
    {
        if (!IsFlag) return;

        IsFlag = false;
        btnText.IsInProgress = true;
        btnPicture.IsInProgress = true;

        FileResult myPhoto = await MediaPicker.Default.PickPhotoAsync();
        if (myPhoto == null) return;
        string localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
        using Stream sourceStream = await myPhoto.OpenReadAsync();
        using FileStream localFileStream = File.OpenWrite(localFilePath);
        await sourceStream.CopyToAsync(localFileStream);
        localFileStream.Close();

        if (string.IsNullOrEmpty(localFilePath)) return;


        TextLabelTest.Text += "You pick a photo \" ";
        foreach (var item in localFilePath)
        {
            TextLabelTest.Text += item;
            await Task.Delay(rnd.Next(minValue, maxValue));
        }
        TextLabelTest.Text += "\"\n";



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


        btnText.IsInProgress = false;
        btnPicture.IsInProgress = false;
        IsFlag = true;
    }
}
