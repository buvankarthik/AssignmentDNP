using System.Text.Json;
using APIContracts;

namespace BlazorApp1.Services;

public class HttpUserService : IUserService
{
    
    private readonly HttpClient httpClient;

    public HttpUserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public async Task<UserDto> AddUserAsync(CreateUserDto request)
    {
        HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDto>( response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
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