namespace MauiApp1.Pages;

public class User
{
    public User()
    {

    }
    public User(string name, int age)
    {
        Name = name;
        Id = age;
    }

    public string Name { get; set; } = "";
    public int Id { get; set; }
}
