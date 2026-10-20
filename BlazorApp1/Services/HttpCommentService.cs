using APIContracts.CommentsDtos;

namespace BlazorApp1.Services;

public class HttpCommentService : ICommentService
{
    
    private readonly HttpClient httpClient;

    public HttpCommentService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public Task<CommentDto> AddCommentAsync(CreateCommentDto request)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCommentAsync(int id, UpdateCommentDto request)
    {
        throw new NotImplementedException();
    }

    public Task<CommentDto> GetSingleAsync(int id, CommentDto commentDto)
    {
        throw new NotImplementedException();
    }

    public Task<CommentDto> GetMany(int id, CommentDto commnentDto)
    {
        throw new NotImplementedException();
    }
}