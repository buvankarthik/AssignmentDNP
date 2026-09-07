using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository _postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public Task ShowAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Posts Overview ===");

        List<Post> posts = _postRepository.GetMany().ToList();
        foreach (Post post in posts)
        {
            Console.WriteLine($"[{post.Title}, {post.PostId}]");
        }

        return Task.CompletedTask;
    }
}