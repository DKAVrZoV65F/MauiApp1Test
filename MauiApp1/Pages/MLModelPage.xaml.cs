namespace MauiApp1.Pages;

public partial class MLModelPage : ContentPage
{
    string text = "";
    public MLModelPage()
    {
        InitializeComponent();
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        text = TextEntryTest.Text;
        TextEntryTest.Text = "";

        if (string.IsNullOrEmpty(text)) return;
        TextLabelTest.Text += "User: " + text + '\n';
    }

    private void TextEntryTest_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (btnPicture == null && btnText == null) return;

        btnPicture.IsVisible = string.IsNullOrEmpty(TextEntryTest.Text);
        btnText.IsVisible = string.IsNullOrEmpty(TextEntryTest.Text);

    }
}