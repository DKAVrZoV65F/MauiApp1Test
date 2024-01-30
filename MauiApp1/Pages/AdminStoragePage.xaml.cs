using MauiApp1.Extension;
using System.Collections.ObjectModel;

namespace MauiApp1.Pages;

public partial class AdminStoragePage : ContentPage
{
    public LocalizationResourceManager LocalizationResourceManager
       => LocalizationResourceManager.Instance;

    public List<User> Users2 { get; set; }
    public ObservableCollection<User> Users { get; set; }

    public AdminStoragePage()
    {
        InitializeComponent();

        Users2 = new List<User>
        {
            new User {Name="Tom", Id=1 },
            new User {Name = "Bob", Id= 2},
            new User {Name="Sam", Id = 3},
            new User {Name = "Alice", Id = 4}
        };


        Users = new ObservableCollection<User>
        {
            new User {Name="Tom", Id=1 },
            new User {Name = "Bob", Id = 2},
            new User {Name="Sam", Id = 3},
            new User {Name = "Alice", Id = 4 }
        };
        BindingContext = this; // привязка к текущему объекту
        User user = new("ADS", 5);
        Users.Add(user);
        usersListView.ItemsSource = Users;
    }

    private void UsersListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var tappedUser = e.Item as User;
        if (tappedUser != null)
        {
            User user = new(tappedUser.Name, Users.Count + 1);
            Users.Add(user);
            tappedItemHeader.Text = $"Count: {Users.Count}";
        }

        if(Users.Count > 15) usersListView.ItemsSource = Users2;
    }
}