using APIContracts;

namespace BlazorApp1.Services;

public interface IUserService {
    public Task<UserDto> AddUserAsync(CreateUserDto request);
    public Task UpdateUserSync(int id, UpdateUserDto request);
    
    public Task<UserDto> GetSingleAsync(int id, UserDto userDto);
    
    public Task<UserDto> GetMany(int id, UserDto userDto);
}