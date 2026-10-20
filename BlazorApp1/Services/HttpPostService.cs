using System.Text.Json;
using APIContracts.PostsDtos;

namespace BlazorApp1.Services;

public class HttpPostService : IPostService
{
    private readonly HttpClient httpClient;
    private static readonly JsonSerializerOptions jsonOptions = 
        new() { PropertyNameCaseInsensitive = true };

    public HttpPostService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public async Task<PostDto> AddPostAsync(CreatePostDto request)
    {
        HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync("posts", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<PostDto>( response, jsonOptions);
    }

    public async Task UpdatePostAsync(int id, UpdatePostDto request)
    {
        HttpResponseMessage httpResponse = await httpClient.PutAsJsonAsync($"posts/{id}", request);
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(await httpResponse.Content.ReadAsStringAsync());
        }
    }

    public async Task<PostDto> GetSingleAsync(int id)
    {
        HttpResponseMessage httpResponse = await httpClient.GetAsync($"posts/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<PostDto>( response, jsonOptions )!;
    }

    public async Task<IEnumerable<PostDto>> GetManyAsync(string? titleContains = null, int? userId = null, string? userName = null)
    {
        List<string> query = new();
        if (!string.IsNullOrWhiteSpace(titleContains)) query.Add($"titleContains={Uri.EscapeDataString(titleContains)}");
        if (userId is not null) query.Add($"userId={userId}");
        if (!string.IsNullOrWhiteSpace(userName)) query.Add($"userName={Uri.EscapeDataString(userName)}");
        string url = query.Count == 0 ? "posts" : "posts?" + string.Join("&", query);

        HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<List<PostDto>>(response, jsonOptions)!;
    }
    
    public async Task DeletePostAsync(int id) 
    {
        HttpResponseMessage httpResponse = await httpClient.DeleteAsync($"posts/{id}");
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(await httpResponse.Content.ReadAsStringAsync());
        }
    }
}