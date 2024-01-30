namespace MauiApp1.Pages;

public class User
{
    public User()
    {

    }
    public User(string name, int age, string description)
    {
        Name = name;
        Id = age;
        Description = description;
    }

    public string Name { get; set; } = "";
    public int Id { get; set; }
    public string Description { get; set; } = "";
}
