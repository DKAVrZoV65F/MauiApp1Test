using MauiApp1.Extension;

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
        if (string.IsNullOrEmpty(text) && !IsFlag) return;

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
        IsFlag = true;
        text = "";
    }

    private void TextEntryTest_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (btnPicture == null && btnText == null) return;

        btnPicture.IsVisible = string.IsNullOrEmpty(TextEntryTest.Text);
        btnText.IsVisible = !string.IsNullOrEmpty(TextEntryTest.Text);
    }

    private void btnPicture_Tapped(object sender, EventArgs e)
    {
        if (!IsFlag) return;

        IsFlag = false;
        btnText.IsInProgress = true;
        btnPicture.IsInProgress = true;

        btnText.IsInProgress = false;
        btnPicture.IsInProgress = false;
        IsFlag = true;
    }
}
