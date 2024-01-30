using MauiApp1.Extension;
using System.Collections.ObjectModel;

namespace MauiApp1.Pages;

public partial class AdminStoragePage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    //public List<User> Users { get; set; }
    public ObservableCollection<User> Users { get; set; }

    public AdminStoragePage()
    {
        InitializeComponent();

        Users = new ObservableCollection<User>
        {
            new User {Name="Tom", Age=38 },
            new User {Name = "Bob", Age= 42},
            new User {Name="Sam", Age = 28},
            new User {Name = "Alice", Age = 33}
        };
        BindingContext = this; // привязка к текущему объекту
        User user = new("ADS", 123);
        Users.Add(user);
        usersListView.ItemsSource = Users;
    }

    private void UsersListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var tappedUser = e.Item as User;
        if (tappedUser != null)
        {
            //tappedItemHeader.Text = $"Pressed: {tappedUser.Name}";
            tappedItemHeader.Text = $"Count: {Users.Count}";
            User user = new(tappedUser.Name, tappedUser.Age);
            Users.Add(user);
            usersListView.ItemsSource = Users;
        }
    }
}