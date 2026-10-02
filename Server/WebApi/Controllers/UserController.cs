using APIContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public UserController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
    {
        await VerifyUserNameIsAvailableAsync(request.UserName);

        User user = new(0, request.UserName, request.Password);
        User created = await userRepository.AddAsync(user);
        UserDto dto = new()
        {
            Id = created.UserId,
            UserName = created.Name
        };
    return Created($"/User/{dto.Id}", created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser([FromRoute] int id ,[FromBody] UpdateUserDto request)
    {
        User user = new(id, request.UserName, request.Password);
        await userRepository.UpdateAsync(user);
        return NoContent();
    }
    
    
       
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetSingle([FromRoute] int id)
    {
        User user = await userRepository.GetSingleAsync(id);
        UserDto dto = new()
        {
            Id = user.UserId,
            UserName = user.Name
        };
        return Ok(dto);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> GetMany([FromQuery] string? nameContains)
    {
        IQueryable<User> users = userRepository.GetMany();

        if (!string.IsNullOrWhiteSpace(nameContains))
        {
            users = users.Where(u =>
                u.Name.Contains(nameContains, StringComparison.OrdinalIgnoreCase));
        }

        List<UserDto> dtos = users
            .Select(u => new UserDto { Id = u.UserId, UserName = u.Name })
            .ToList();

        return Ok(dtos);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        await userRepository.DeleteAsync(id);
            return NoContent();
    }
    
    private Task VerifyUserNameIsAvailableAsync(string userName, int? ignoreUserId = null )
    {
        bool taken = userRepository.GetMany().Any(u => u.Name == userName && u.UserId != ignoreUserId);
        if (taken)
        {
            throw new InvalidOperationException($"Username '{userName}' is already taken.");
        }
        return Task.CompletedTask;
    }
    
}