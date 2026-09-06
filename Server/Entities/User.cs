namespace Entities;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public User(int userId, string name, string password)
    {
        UserId = userId;
        Name = name;
        Password = password;
    }
}