using APIContracts.CommentsDtos;

namespace BlazorApp1.Services;

public interface ICommentService {
    public Task<CommentDto> AddCommentAsync(CreateCommentDto request);
    public Task UpdateCommentAsync(int id, UpdateCommentDto request);
    
    public Task<CommentDto> GetSingleAsync(int id);
    
    public Task<IEnumerable<CommentDto>> GetManyAsync(int? postId = null, int? userId = null, string? userName = null);
    public Task DeleteCommentAsync(int id);
}