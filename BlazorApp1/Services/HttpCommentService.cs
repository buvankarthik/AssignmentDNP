using System.Text.Json;
using APIContracts.CommentsDtos;

namespace BlazorApp1.Services;

public class HttpCommentService : ICommentService
{
    
    private readonly HttpClient httpClient;
    private static readonly JsonSerializerOptions jsonOptions = 
        new() { PropertyNameCaseInsensitive = true };

    public HttpCommentService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public async Task<CommentDto> AddCommentAsync(CreateCommentDto request)
    {
        HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync("comments", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto>( response, jsonOptions);
    }

    public async Task UpdateCommentAsync(int id, UpdateCommentDto request)
    {
        HttpResponseMessage httpResponse = await httpClient.PutAsJsonAsync($"comments/{id}", request);
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(await httpResponse.Content.ReadAsStringAsync());
        }
    }

    public async Task<CommentDto> GetSingleAsync(int id)
    {
        HttpResponseMessage httpResponse = await httpClient.GetAsync($"comments/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto>( response, jsonOptions )!;
    }

    public async Task<IEnumerable<CommentDto>> GetManyAsync(int? postId = null, int? userId = null, string? userName = null)
    {
        List<string> query = new();
        if (postId is not null) query.Add($"postId={postId}");
        if (userId is not null) query.Add($"userId={userId}");
        if (!string.IsNullOrWhiteSpace(userName)) query.Add($"userName={Uri.EscapeDataString(userName)}");
        string url = query.Count == 0 ? "comments" : "comments?" + string.Join("&", query);

        HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<List<CommentDto>>(response, jsonOptions)!;
    }
    
    public async Task DeleteCommentAsync(int id) 
    {
        HttpResponseMessage httpResponse = await httpClient.DeleteAsync($"comments/{id}");
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(await httpResponse.Content.ReadAsStringAsync());
        }
    }
}