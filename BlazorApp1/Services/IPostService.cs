using APIContracts.PostsDtos;

namespace BlazorApp1.Services;

public interface IPostService {
    public Task<PostDto> AddPostAsync(CreatePostDto request);
    public Task UpdatePostAsync(int id, UpdatePostDto request);
    
    public Task<PostDto> GetSingleAsync(int id);
    public Task<IEnumerable<PostDto>> GetManyAsync(string? titleContains = null, int? userId = null, string? userName = null);
    public Task DeletePostAsync(int id);
}