using APIContracts.CommentsDtos;
using APIContracts.PostsDtos;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;

    public CommentsController(ICommentRepository commentRepository, IUserRepository userRepository,  IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> AddComment([FromBody] CreateCommentDto request)
    {
        Comment comment = new (0, request.Body, request.UserId, request.PostId);
        Comment created = await commentRepository.AddAsync(comment);
        CommentDto dto = new()
        {
            Id = created.CommentId,
            Body = created.Body,
            UserId = created.UserId,
            PostId = created.PostId
        };
    return Created($"/comments/{dto.Id}", dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateComment([FromRoute] int id ,[FromBody] UpdateCommentDto request)
    {
        Comment existing = await commentRepository.GetSingleAsync(id);
        Comment comment = new(id, request.Body, existing.UserId, existing.PostId);
        await commentRepository.UpdateAsync(comment);
        return NoContent();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetSingle([FromRoute] int id)
    {
        Comment comment = await commentRepository.GetSingleAsync(id);
        CommentDto dto = new()
        {
            Id = comment.CommentId,
            Body = comment.Body,
            UserId = comment.UserId,
            PostId = comment.PostId
        };
        return Ok(dto);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<CommentDto>> GetMany(
        [FromQuery] int? postId,
        [FromQuery] int? userId,
        [FromQuery] string? userName)
    {
        IQueryable<Comment> comments = commentRepository.GetMany();
        
        if (!string.IsNullOrWhiteSpace(userName))
        {
            List<int> userIds = userRepository.GetMany()
                .Where(u => u.Name == userName)
                .Select(u => u.UserId)
                .ToList();
            comments = comments.Where(p => userIds.Contains(p.UserId));
        }
        if (postId is not null)
        {
            comments = comments.Where(c => c.PostId == postId);
        }

        if (userId is not null)
        {
            comments = comments.Where(c => c.UserId == userId);
        }
        List<CommentDto> dtos = comments
            .Select(c => new CommentDto
            {
                Id = c.CommentId,
                Body = c.Body,
                UserId = c.UserId,
                PostId = c.PostId
            })
            .ToList();
        return Ok(dtos);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComment([FromRoute] int id)
    {
        await commentRepository.DeleteAsync(id);
            return NoContent();
    }
    
}
