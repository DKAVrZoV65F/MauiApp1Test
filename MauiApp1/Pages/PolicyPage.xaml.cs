using MauiApp1.Extension;

namespace MauiApp1.Pages;

public partial class PolicyPage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    private List<string> _allImages = [];
    private Random _random = new();

    public List<string> ImageList1 { get; set; }
    public List<string> ImageList2 { get; set; }
    public List<string> ImageList3 { get; set; }
    public List<string> ImageList4 { get; set; }

    public PolicyPage()
    {
        InitializeComponent();

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
            _allImages.Add($"img{i:00}.jpg");
        }
    }

    public List<T> Randomize<T>(List<T> source) => [.. source.OrderBy((item) => _random.Next())];

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e) => btnAccept.IsEnabled = e.Value;

    public async void ImagePathClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();

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
    }
}