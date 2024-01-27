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

        int getValue = Preferences.Get("FontSize", 20);
        TitleLb.FontSize = getValue + 5;
        RussianRb.FontSize = getValue;
        EnglishRb.FontSize = getValue;

        string currentLanguage = Preferences.Get("LanguageApp", "ru-RU");
        switch (currentLanguage)
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
        string? checkBoxValue = (selectedRadioButton.Value != null) ? selectedRadioButton.Value.ToString() : "";
        if (string.IsNullOrEmpty(checkBoxValue)) return;

        CultureInfo cultureInfo = new CultureInfo(checkBoxValue);
        LocalizationResourceManager.Instance.SetCulture(cultureInfo);
        Preferences.Set("LanguageApp", checkBoxValue);
    }
}