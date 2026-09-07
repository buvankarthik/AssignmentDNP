using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository _userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task ShowAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Users ===");

        List<User> users = _userRepository.GetMany().ToList();
        foreach (User user in users)
        {
            Console.WriteLine($"{user.UserId}: {user.Name}");
        }

        return Task.CompletedTask;
    }
}