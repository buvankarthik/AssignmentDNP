namespace Entities;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public List<Post> Posts { get; set; } = new();

    public List<Comment> Comments { get; set; } = new();

    
    public User(){}  // for the EFC 
    
    public User(int userId, string name, string password)
    {
        UserId = userId;
        Name = name;
        Password = password;
    }
}