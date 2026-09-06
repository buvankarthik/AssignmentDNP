namespace Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public Comment(int commentId, string body, int userId, int postId)
    {
        CommentId = commentId;
        Body = body;
        UserId = userId;
        PostId = postId;
    }
}