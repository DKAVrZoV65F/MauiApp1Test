namespace MauiApp1.Pages;

public partial class UpdatingStoragePage : ContentPage
{
	private bool IsFlag = true;
	public UpdatingStoragePage(User xyz)
	{
		InitializeComponent();

        IDLabel.Text = $"ID: {xyz.Id}";
        NameLabel.Text = $"Name: {xyz.Name}";
        DescriptionEntry.Text = $"{xyz.Description}";
    }

	private async void SavingData(object sender, EventArgs e)
	{
        if (!IsFlag) return;

        IsFlag = false;
        SavingButton.IsInProgress = true;

		await Task.Delay(1000);

        IsFlag = true;
        SavingButton.IsInProgress = false;
    }
}