using MauiApp1.Extension;
using System.Collections.ObjectModel;

namespace MauiApp1.Pages;

public partial class AdminStoragePage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    public ObservableCollection<User> Users { get; set; }

    public AdminStoragePage()
    {
        InitializeComponent();
        
        BindingContext = this;

        int getValue = Preferences.Get("FontSize", 20);
        SearchEntry.FontSize = getValue;

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
        User user = new(tappedUser.Name, tappedUser.Id, tappedUser.Description);
        await Navigation.PushAsync(new UpdatingStoragePage(user));
    }

    private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        ObservableCollection<User> Search = new(Users.Where(x => x.Name.Contains(SearchEntry.Text) || x.Id.ToString().Contains(SearchEntry.Text)));
        fruitsListView.ItemsSource = (Search.Count > 0) ? Search : Users;
    }
}