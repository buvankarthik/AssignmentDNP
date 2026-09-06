namespace Entities;

public class Comment
{
    public int commentId { get; set; }
    public string body { get; set; }
    public int userId { get; set; }
    public int postId { get; set; }
}