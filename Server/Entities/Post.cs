namespace Entities;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; } 
    public Post(int postId, string title, string body, int userId)
    {
        PostId = postId;
        Title = title;
        Body = body;
        UserId = userId;
    }
}