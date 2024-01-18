using MauiApp1.Extension;
using System.Globalization;

namespace MauiApp1.Pages;

public partial class LanguagePage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    public LanguagePage()
    {
        InitializeComponent();

        CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
        LocalizationResourceManager.Instance.SetCulture(currentCulture);
        switch (currentCulture.Name)
        {
            case "ru-RU":
                RussianRb.IsChecked = true;
                break;
            case "en-US":
                EnglishRb.IsChecked = true;
                break;
            default:
                RussianRb.IsChecked = true;
                break;
        }
    }

    void OnLanguageCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton selectedRadioButton = ((RadioButton)sender);

        if (string.IsNullOrEmpty(selectedRadioButton.Value.ToString())) return;

        CultureInfo cultureInfo = new CultureInfo(selectedRadioButton.Value.ToString());
        LocalizationResourceManager.Instance.SetCulture(cultureInfo);
    }
}