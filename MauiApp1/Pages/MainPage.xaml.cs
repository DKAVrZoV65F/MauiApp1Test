using MauiApp1.Extension;

namespace MauiApp1.Pages;

public partial class MainPage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    readonly string ipServer = "192.168.1.68";
    readonly int portServer = 8888;
    string path = "";
    string command = "IMAGE";
    string currentLanguage = "English";
    readonly string RUSSIAN = "Русский";
    readonly string ENGLISH = "English";

    static readonly Dictionary<DevicePlatform, IEnumerable<string>> FileType = new()
    {
        { DevicePlatform.Android, new[] { "text/*" } } ,
        { DevicePlatform.iOS, new[] { "public.json", "public.plain-text" } },
        { DevicePlatform.MacCatalyst, new[] { "public.png", "public.plain-text" } },
        { DevicePlatform.WinUI, new[] { ".png", ".jpg" } }
    };


    private List<string> _allImages = new();
    private Random _random = new Random();

    public List<string> ImageList1 { get; set; }
    public List<string> ImageList2 { get; set; }
    public List<string> ImageList3 { get; set; }
    public List<string> ImageList4 { get; set; }

    public MainPage()
    {
        InitializeComponent();

        //  if (true) ImagePathClicked(null, null);

        GenerateData();

        ImageList1 = Randomize(source: _allImages);
        ImageList2 = Randomize(source: _allImages);
        ImageList3 = Randomize(source: _allImages);
        ImageList4 = Randomize(source: _allImages);

        BindingContext = this;
    }

    private void GenerateData()
    {
        //All Images
        _allImages = new();

        for (var i = 1; i <= 18; i++)
        {
            _allImages.Add($"img{i.ToString("00")}.jpg");
        }
    }

    public List<T> Randomize<T>(List<T> source) =>
        source.OrderBy<T, int>((item) => _random.Next()).ToList();

    public async void ImagePathClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new HelpPage());

        /*await DisplayAlert(Title, LocalizationResourceManager["ErrorWithAlgorithm"].ToString(), "ОK");
        string result = await DisplayActionSheet(LocalizationResourceManager["AppInfo"].ToString(), LocalizationResourceManager["Thanks"].ToString(), "GitHub", LocalizationResourceManager["Version"].ToString() + $" {AppInfo.Current.VersionString}", LocalizationResourceManager["Language"].ToString() + $"  {currentLanguage}", LocalizationResourceManager["Author"].ToString());

        if (result == null) return;
        else if (result == "GitHub") await Clipboard.SetTextAsync("https://github.com/DKAVrZoV65F/Digital-Terrain-Models");
        else if (result.Contains(ENGLISH) || result.Contains(RUSSIAN))
        {
            var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName.
                Equals("en", StringComparison.InvariantCultureIgnoreCase) ?
                new CultureInfo("ru-RU") : new CultureInfo("en-US");

            LocalizationResourceManager.Instance.SetCulture(switchToCulture);

            currentLanguage = (currentLanguage.Equals(ENGLISH)) ? RUSSIAN : ENGLISH;
        }

        var response = await FilePicker.PickAsync();
        if (response == null) return;
        await DisplayAlert(Title, response.FullPath, "OK");*/

        /*FileResult myPhoto = await MediaPicker.Default.PickPhotoAsync();
        if (myPhoto == null) return;
        string localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
        using Stream sourceStream = await myPhoto.OpenReadAsync();
        using FileStream localFileStream = File.OpenWrite(localFilePath);
        await sourceStream.CopyToAsync(localFileStream);
        //await DisplayAlert(Title, localFilePath, "OK");
        localFileStream.Close();



        // if (!File.Exists(path)) return;

        using TcpClient tcpClient = new();
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
    }
}
