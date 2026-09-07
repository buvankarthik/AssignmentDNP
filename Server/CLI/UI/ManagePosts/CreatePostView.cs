using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository _postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Create Post ===");

        Console.Write("Title: ");
        string title = Console.ReadLine() ?? "";
        Console.Write("Body: ");
        string body = Console.ReadLine() ?? "";
        Console.Write("User ID: ");
        string? userIdInput = Console.ReadLine();
        int userId;
        if (!int.TryParse(userIdInput, out userId))
        {
            Console.WriteLine("Invalid user ID.");
            return;
        }

        Post post = new Post(0, title, body, userId);
        Post createdPost = await _postRepository.AddAsync(post);

        Console.WriteLine($"Post '{createdPost.Title}' created with ID {createdPost.PostId}.");
    }
}