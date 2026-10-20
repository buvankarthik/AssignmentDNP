using APIContracts.PostsDtos;

namespace BlazorApp1.Services;

public interface IPostService {
    public Task<PostDto> AddPostAsync(CreatePostDto request);
    public Task UpdatePostAsync(int id, UpdatePostDto request);
    
    public Task<PostDto> GetSingleAsync(int id, PostDto postDto);
    
    public Task<PostDto> GetMany(int id, PostDto postDto);
}