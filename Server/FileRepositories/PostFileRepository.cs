using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";

    public PostFileRepository(){
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Post> AddAsync(Post post){
        List<Post> posts = await ReadPostsAsync();
        post.PostId = posts.Any() ? posts.Max(p => p.PostId) + 1 : 1;
        posts.Add(post);
        await WritePostsAsync(posts);
        return post;
    }

    public async Task UpdateAsync(Post post){
        List<Post> posts = await ReadPostsAsync();
        Post? existing = posts.SingleOrDefault(p => p.PostId == post.PostId);
        if (existing is null)
        {
            throw new InvalidOperationException($"Post with ID '{post.PostId}' not found");
        }

        posts.Remove(existing);
        posts.Add(post);
        await WritePostsAsync(posts);
    }

    public async Task DeleteAsync(int id){
        List<Post> posts = await ReadPostsAsync();
        Post? postToRemove = posts.SingleOrDefault(p => p.PostId == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }
        posts.Remove(postToRemove);
        await WritePostsAsync(posts);
    }

    public async Task<Post> GetSingleAsync(int id){
        List<Post> posts = await ReadPostsAsync();
        Post? post = posts.SingleOrDefault(p => p.PostId == id);
        if (post is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }
        return post;
    }

    public IQueryable<Post> GetMany(){
        string postsAsJson = File.ReadAllText(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        return posts.AsQueryable();
    }

    private async Task<List<Post>> ReadPostsAsync(){
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
    }

    private async Task WritePostsAsync(List<Post> posts){
        string postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }
}