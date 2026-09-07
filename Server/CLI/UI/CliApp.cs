using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;

    public CliApp(IUserRepository userRepository,
        ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }

    public async Task StartAsync()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1. Manage Users");
            Console.WriteLine("2. Manage Posts");
            Console.WriteLine("3. Exit");
            Console.Write("What do you want to do? ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ManageUsersView manageUsersView = new ManageUsersView(_userRepository);
                    await manageUsersView.ShowAsync();
                    break;
                case "2":
                    ManagePostsView managePostsView = new ManagePostsView(_postRepository, _userRepository, _commentRepository);
                    await managePostsView.ShowAsync();
                    break;
                case "3":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }
}