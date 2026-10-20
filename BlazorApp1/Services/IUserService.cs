namespace BlazorApp1.Services;

public interface IUserService {
    public Task<UserDto> AddUserAsync(CreateUserDto request);
    public Task UpdateUsersync(int id, UpdateUserDto request);
}