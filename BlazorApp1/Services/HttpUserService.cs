using APIContracts;

namespace BlazorApp1.Services;

public class HttpUserService : IUserService
{
    
    private readonly HttpClient httpClient;

    public HttpUserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public Task<UserDto> AddUserAsync(CreateUserDto request)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserSync(int id, UpdateUserDto request)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetSingleAsync(int id, UserDto userDto)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetMany(int id, UserDto userDto)
    {
        throw new NotImplementedException();
    }
}