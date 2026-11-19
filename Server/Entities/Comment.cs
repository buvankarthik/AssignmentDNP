namespace Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    
    public User User { get; set; } = null!;
    public Post Post { get; set; } = null!;
    
    public Comment(){} // for the EFC
    
    public Comment(int commentId, string body, int userId, int postId)
    {
        CommentId = commentId;
        Body = body;
        UserId = userId;
        PostId = postId;
    }
}