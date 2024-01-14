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
        TextLabelTest.Text = text;
    }
}