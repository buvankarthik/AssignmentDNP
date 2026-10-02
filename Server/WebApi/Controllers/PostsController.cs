using APIContracts;
using APIContracts.PostsDtos;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public PostsController(IPostRepository postRepository, IUserRepository userRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> AddPost([FromBody] CreatePostDto request)
    {

        Post post = new (0, request.Title, request.Body, request.UserId);
        Post created = await postRepository.AddAsync(post);
        PostDto dto = new()
        {
            Id = created.PostId,
            Title = created.Title,
            Body = created.Body,
            UserId = created.UserId,
        };
    return Created($"/posts/{dto.Id}", dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePost([FromRoute] int id ,[FromBody] UpdatePostDto request)
    {
        Post existing = await postRepository.GetSingleAsync(id);
        Post post = new(id, request.Title, request.Body, existing.UserId);
        await postRepository.UpdateAsync(post);
        return NoContent();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetSingle([FromRoute] int id)
    {
        Post post = await postRepository.GetSingleAsync(id);
        PostDto dto = new()
        {
            Id = post.PostId,
            Title = post.Title,
            Body = post.Body,
            UserId = post.UserId,
        };
        return Ok(dto);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<PostDto>> GetMany(
        [FromQuery] string? titleContains,
        [FromQuery] int? userId,
        [FromQuery] string? userName)
    {
        IQueryable<Post> posts = postRepository.GetMany();

        if (!string.IsNullOrWhiteSpace(titleContains))
        {
            posts = posts.Where(p =>
                p.Title.Contains(titleContains, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrWhiteSpace(userName))
        {
            List<int> userIds = userRepository.GetMany()
                .Where(u => u.Name == userName)
                .Select(u => u.UserId)
                .ToList();
            posts = posts.Where(p => userIds.Contains(p.UserId));
        }

        if (userId is not null)
        {
            posts = posts.Where(p => p.UserId == userId);
        }
        List<PostDto> dtos = posts
            .Select(p => new PostDto
            {
                Id = p.PostId,
                Title = p.Title,
                Body = p.Body,
                UserId = p.UserId
            })
            .ToList();
        return Ok(dtos);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost([FromRoute] int id)
    {
        await postRepository.DeleteAsync(id);
            return NoContent();
    }
    
}