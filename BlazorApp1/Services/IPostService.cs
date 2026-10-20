namespace BlazorApp1.Services;

public interface IPostService {
    public Task<PostDto> AddPostAsync(CreatePostDto request);
    public Task UpdatePostAsync(int id, UpdatePostDto request);
}