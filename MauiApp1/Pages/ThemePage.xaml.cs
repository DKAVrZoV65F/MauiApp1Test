using MauiApp1.Extension;

namespace MauiApp1.Pages;

public partial class ThemePage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;
    public ThemePage()
    {
        InitializeComponent();

        DefaultRb.IsChecked = true;
        /*switch (currentCulture.Name)
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
        }*/
    }

    void OnThemeCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton selectedRadioButton = ((RadioButton)sender);
        string? checkBoxValue = (selectedRadioButton.Value != null) ? selectedRadioButton.Value.ToString() : "";
        if (string.IsNullOrEmpty(checkBoxValue)) return;

        switch (checkBoxValue)
        {
            case "Light":
                Application.Current.UserAppTheme = AppTheme.Light;
                break;
            case "Dark":
                Application.Current.UserAppTheme = AppTheme.Dark;
                break;
            default:
                Application.Current.UserAppTheme = AppTheme.Unspecified;
                break;
        }
    }

    void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        btnAccept.IsEnabled = true;
        int value = (int)e.NewValue;
        ExampleLabel.FontSize = value;
        //header.FontSize = value;
        header.Text = value.ToString();
    }

    private async void SettingsClicked(object sender, EventArgs e)
    {

        await DisplayAlert(LocalizationResourceManager["AppName"].ToString(), LocalizationResourceManager["ReloadApp"].ToString(), "OK");
    }
}