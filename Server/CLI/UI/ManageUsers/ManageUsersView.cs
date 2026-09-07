using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly IUserRepository _userRepository;

    public ManageUsersView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine();
            Console.WriteLine("=== Manage Users ===");
            Console.WriteLine("1. Create new user");
            Console.WriteLine("2. Update existing user");
            Console.WriteLine("3. Delete user");
            Console.WriteLine("4. See all users");
            Console.WriteLine("5. Back");
            Console.Write("What do you want to do? ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateUserView createUserView = new CreateUserView(_userRepository);
                    await createUserView.ShowAsync();
                    break;
                case "2":
                    await UpdateUserAsync();
                    break;
                case "3":
                    await DeleteUserAsync();
                    break;
                case "4":
                    ListUsersView listUsersView = new ListUsersView(_userRepository);
                    await listUsersView.ShowAsync();
                    break;
                case "5":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    private async Task UpdateUserAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Update User ===");

        Console.Write("User ID to update: ");
        string? idInput = Console.ReadLine();
        int userId;
        if (!int.TryParse(idInput, out userId))
        {
            Console.WriteLine("Invalid user ID.");
            return;
        }

        User existingUser;
        try
        {
            existingUser = await _userRepository.GetSingleAsync(userId);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"User with ID '{userId}' not found.");
            return;
        }

        Console.WriteLine($"Current name: {existingUser.Name}");
        Console.Write("New name (leave empty to keep current): ");
        string? name = Console.ReadLine();

        Console.Write("New password (leave empty to keep current): ");
        string? password = Console.ReadLine();

        User updatedUser = new User(
            existingUser.UserId,
            string.IsNullOrWhiteSpace(name) ? existingUser.Name : name,
            string.IsNullOrWhiteSpace(password) ? existingUser.Password : password);

        await _userRepository.UpdateAsync(updatedUser);

        Console.WriteLine($"User '{updatedUser.Name}' updated.");
    }

    private async Task DeleteUserAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Delete User ===");

        Console.Write("User ID to delete: ");
        string? idInput = Console.ReadLine();
        int userId;
        if (!int.TryParse(idInput, out userId))
        {
            Console.WriteLine("Invalid user ID.");
            return;
        }

        try
        {
            await _userRepository.DeleteAsync(userId);
            Console.WriteLine($"User with ID '{userId}' deleted.");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"User with ID '{userId}' not found.");
        }
    }
}