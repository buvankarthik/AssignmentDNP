using APIContracts.CommentsDtos;

namespace BlazorApp1.Services;

public interface ICommentService {
    public Task<CommentDto> AddCommentAsync(CreateCommentDto request);
    public Task UpdateCommentAsync(int id, UpdateCommentDto request);
    
    public Task<CommentDto> GetSingleAsync(int id, CommentDto commentDto);
    
    public Task<CommentDto> GetMany(int id, CommentDto commnentDto);
}