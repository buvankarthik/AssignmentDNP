using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;

    public SinglePostView(IPostRepository postRepository, IUserRepository userRepository,
        ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== View Single Post ===");
        Console.Write("Post ID: ");
        string? idInput = Console.ReadLine();
        int postId;
        if (!int.TryParse(idInput, out postId))
        {
            Console.WriteLine("Invalid post ID.");
            return;
        }

        Post post;
        try
        {
            post = await _postRepository.GetSingleAsync(postId);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"Post with ID '{postId}' not found.");
            return;
        }

        User author = await _userRepository.GetSingleAsync(post.UserId);

        Console.WriteLine();
        Console.WriteLine($"=== {post.Title} ===");
        Console.WriteLine($"By: {author.Name}");
        Console.WriteLine(post.Body);

        List<Comment> comments = _commentRepository.GetMany().Where(c => c.PostId == post.PostId).ToList();
        Console.WriteLine();
        Console.WriteLine(comments.Any() ? "Comments:" : "No comments yet.");
        foreach (Comment comment in comments)
        {
            User commenter = await _userRepository.GetSingleAsync(comment.UserId);
            Console.WriteLine($"- {commenter.Name}: {comment.Body}");
        }
    }
}