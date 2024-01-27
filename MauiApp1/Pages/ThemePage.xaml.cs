using MauiApp1.Extension;

namespace MauiApp1.Pages;

public partial class ThemePage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;
    private int oldValue = 20;

    public ThemePage()
    {
        InitializeComponent();

        string getTheme = Preferences.Get("ThemeApp", "Default");
        switch (getTheme)
        {
            case "Light":
                LightRb.IsChecked = true;
                break;
            case "Dark":
                DarkRb.IsChecked = true;
                break;
            default:
                DefaultRb.IsChecked = true;
                break;
        }

        int getValue = Preferences.Get("FontSize", oldValue);
        fontSizeSl.Value = getValue;
        oldValue = getValue;
        ExampleLabel.FontSize = getValue;


        TitleLb.FontSize = getValue + 5;
        btnAccept.FontSize = getValue;
        LightRb.FontSize = getValue;
        DarkRb.FontSize = getValue;
        DefaultRb.FontSize = getValue;
    }

    private void OnThemeCheckedChanged(object sender, CheckedChangedEventArgs e)
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
        Preferences.Set("ThemeApp", checkBoxValue);
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        int value = (int)e.NewValue;
        ExampleLabel.FontSize = value;
        header.Text = value.ToString();
        if (value != oldValue) oldValue = value;
    }

    private async void SettingsClicked(object sender, EventArgs e)
    {
        Preferences.Set("FontSize", oldValue);
        await DisplayAlert(LocalizationResourceManager["AppName"].ToString(), LocalizationResourceManager["ReloadApp"].ToString(), "OK");
    }
}