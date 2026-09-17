using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{

    private readonly string filePath = "comments.json";

    public CommentFileRepository(){
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<Comment> AddAsync(Comment comment){
        List<Comment> comments = await ReadCommentsAsync();
        comment.CommentId = comments.Any() ? comments.Max(c => c.CommentId) + 1 : 1;
        comments.Add(comment);
        await WriteCommentsAsync(comments);
        return comment;
    }

    public async Task UpdateAsync(Comment comment){
        List<Comment> comments = await ReadCommentsAsync();
        Comment? existing = comments.SingleOrDefault(c => c.CommentId == comment.CommentId);
        if (existing is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.CommentId}' not found");
        }
        comments.Remove(existing);
        comments.Add(comment);
        await WriteCommentsAsync(comments);
    }

    public async Task DeleteAsync(int id){
        List<Comment> comments = await ReadCommentsAsync();
        Comment? commentToRemove = comments.SingleOrDefault(c => c.CommentId == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException("Comment with ID '{id}' not found");
        }
        comments.Remove(commentToRemove);
        await WriteCommentsAsync(comments);
    }

    public async Task<Comment> GetSingleAsync(int id){
        List<Comment> comments = await ReadCommentsAsync();
        Comment? comment = comments.SingleOrDefault(c => c.CommentId == id);
        if (comment is null)
        {
            throw new InvalidOperationException("Comment with ID '{id}' not found");
        }
        return comment;
    }

    public IQueryable<Comment> GetMany(){
        string commentsAsJson = File.ReadAllText(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }

    private async Task<List<Comment>> ReadCommentsAsync(){
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
    }

    private async Task WriteCommentsAsync(List<Comment> comments){
        string commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
}