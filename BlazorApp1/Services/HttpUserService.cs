using System.Text.Json;
using APIContracts;

namespace BlazorApp1.Services;

public class HttpUserService : IUserService
{
    
    private readonly HttpClient httpClient;
    private static readonly JsonSerializerOptions jsonOptions = 
        new() { PropertyNameCaseInsensitive = true };

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
        return JsonSerializer.Deserialize<UserDto>( response, jsonOptions);
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto request)
    {
        HttpResponseMessage httpResponse = await httpClient.PutAsJsonAsync($"users/{id}", request);
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(await httpResponse.Content.ReadAsStringAsync());
        }
    }

    public async Task<UserDto> GetSingleAsync(int id)
    {
        HttpResponseMessage httpResponse = await httpClient.GetAsync($"users/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDto>( response, jsonOptions )!;
    }

    public async Task<IEnumerable<UserDto>> GetManyAsync(string? nameContains = null)
    {
        string url = "users";
        if (!string.IsNullOrWhiteSpace(nameContains))
        {
            url += $"?nameContains={Uri.EscapeDataString(nameContains)}";
        }
        HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<List<UserDto>>( response, jsonOptions)!;
    }
    
    public async Task DeleteUserAsync(int id)
    {
        HttpResponseMessage httpResponse = await httpClient.DeleteAsync($"users/{id}");
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(await httpResponse.Content.ReadAsStringAsync());
        }
    }
    
}