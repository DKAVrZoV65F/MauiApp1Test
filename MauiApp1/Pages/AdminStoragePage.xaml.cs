using MauiApp1.Extension;
using System.Collections.ObjectModel;

namespace MauiApp1.Pages;

public partial class AdminStoragePage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    public List<User> Users2 { get; set; }
    public ObservableCollection<User> Users { get; set; }
    private ObservableCollection<User> Search { get; set; }

    public AdminStoragePage()
    {
        InitializeComponent();
        BindingContext = this; // привязка к текущему объекту

        Users = new ObservableCollection<User>
        {
            new User {Name="Tom", Id=1, Description = "Q" },
            new User {Name = "Bob", Id = 2, Description = "W"},
            new User {Name="Sam", Id = 3, Description = "E"},
            new User {Name = "Alice", Id = 4, Description = "R" }
        };
        
        fruitsListView.ItemsSource = Users;
    }

    private async void FruitsListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var tappedUser = e.Item as User;
        // go to editing
        User user = new(tappedUser.Name, tappedUser.Id, tappedUser.Description);
        await Navigation.PushAsync(new UpdatingStoragePage(user));
    }

    private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        //search
        Search = new(Users.Where(x => x.Name == SearchEntry.Text || x.Id.ToString() == SearchEntry.Text));

        fruitsListView.ItemsSource = (Search.Count > 0) ? Search : Users;
    }
}