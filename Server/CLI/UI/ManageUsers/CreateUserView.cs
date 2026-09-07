using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository _userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Create User ===");

        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Password: ");
        string password = Console.ReadLine() ?? "";

        User user = new User(0, name, password);
        User createdUser = await _userRepository.AddAsync(user);

        Console.WriteLine($"User '{createdUser.Name}' created with ID {createdUser.UserId}.");
    }
}