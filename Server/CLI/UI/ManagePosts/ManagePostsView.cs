using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;

    public ManagePostsView(IPostRepository postRepository, IUserRepository userRepository,
        ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine();
            Console.WriteLine("=== Manage Posts ===");
            Console.WriteLine("1. Create new post");
            Console.WriteLine("2. Update existing post");
            Console.WriteLine("3. Delete post");
            Console.WriteLine("4. See overview of posts");
            Console.WriteLine("5. View single post");
            Console.WriteLine("6. Add comment to a post");
            Console.WriteLine("7. Back");
            Console.Write("What do you want to do? ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreatePostView createPostView = new CreatePostView(_postRepository);
                    await createPostView.ShowAsync();
                    break;
                case "2":
                    await UpdatePostAsync();
                    break;
                case "3":
                    await DeletePostAsync();
                    break;
                case "4":
                    ListPostsView listPostsView = new ListPostsView(_postRepository);
                    await listPostsView.ShowAsync();
                    break;
                case "5":
                    SinglePostView singlePostView = new SinglePostView(_postRepository, _userRepository, _commentRepository);
                    await singlePostView.ShowAsync();
                    break;
                case "6":
                    await AddCommentAsync();
                    break;
                case "7":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    private async Task UpdatePostAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Update Post ===");

        Console.Write("Post ID to update: ");
        string? idInput = Console.ReadLine();
        int postId;
        if (!int.TryParse(idInput, out postId))
        {
            Console.WriteLine("Invalid post ID.");
            return;
        }

        Post existingPost;
        try
        {
            existingPost = await _postRepository.GetSingleAsync(postId);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"Post with ID '{postId}' not found.");
            return;
        }

        Console.WriteLine($"Current title: {existingPost.Title}");
        Console.Write("New title (leave empty to keep current): ");
        string? title = Console.ReadLine();

        Console.WriteLine($"Current body: {existingPost.Body}");
        Console.Write("New body (leave empty to keep current): ");
        string? body = Console.ReadLine();

        Post updatedPost = new Post(
            existingPost.PostId,
            string.IsNullOrWhiteSpace(title) ? existingPost.Title : title,
            string.IsNullOrWhiteSpace(body) ? existingPost.Body : body,
            existingPost.UserId);

        await _postRepository.UpdateAsync(updatedPost);

        Console.WriteLine($"Post '{updatedPost.Title}' updated.");
    }

    private async Task DeletePostAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Delete Post ===");

        Console.Write("Post ID to delete: ");
        string? idInput = Console.ReadLine();
        int postId;
        if (!int.TryParse(idInput, out postId))
        {
            Console.WriteLine("Invalid post ID.");
            return;
        }

        try
        {
            await _postRepository.DeleteAsync(postId);
            Console.WriteLine($"Post with ID '{postId}' deleted.");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"Post with ID '{postId}' not found.");
        }
    }

    private async Task AddCommentAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Add Comment ===");

        Console.Write("Comment body: ");
        string body = Console.ReadLine() ?? "";

        Console.Write("User ID: ");
        string? userIdInput = Console.ReadLine();
        int userId;
        if (!int.TryParse(userIdInput, out userId))
        {
            Console.WriteLine("Invalid user ID.");
            return;
        }

        Console.Write("Post ID: ");
        string? postIdInput = Console.ReadLine();
        int postId;
        if (!int.TryParse(postIdInput, out postId))
        {
            Console.WriteLine("Invalid post ID.");
            return;
        }

        try
        {
            await _postRepository.GetSingleAsync(postId);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"Post with ID '{postId}' not found.");
            return;
        }

        Comment comment = new Comment(0, body, userId, postId);
        Comment createdComment = await _commentRepository.AddAsync(comment);

        Console.WriteLine($"Comment added with ID {createdComment.CommentId}.");
    }
}