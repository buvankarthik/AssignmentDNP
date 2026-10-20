using APIContracts;

namespace BlazorApp1.Services;

public interface IUserService {
    public Task<UserDto> AddUserAsync(CreateUserDto request);
    public Task UpdateUserAsync(int id, UpdateUserDto request);
    
    public Task<UserDto> GetSingleAsync(int id);
    public Task<IEnumerable<UserDto>> GetManyAsync(string? nameContains = null);
    public Task DeleteUserAsync(int id);
}