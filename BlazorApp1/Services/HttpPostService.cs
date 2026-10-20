using APIContracts.PostsDtos;

namespace BlazorApp1.Services;

public class HttpPostService : IPostService
{
    
    private readonly HttpClient httpClient;

    public HttpPostService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public Task<PostDto> AddPostAsync(CreatePostDto request)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePostAsync(int id, UpdatePostDto request)
    {
        throw new NotImplementedException();
    }

    public Task<PostDto> GetSingleAsync(int id, PostDto postDto)
    {
        throw new NotImplementedException();
    }

    public Task<PostDto> GetMany(int id, PostDto postDto)
    {
        throw new NotImplementedException();
    }
}