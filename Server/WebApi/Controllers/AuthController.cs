using APIContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase {
    
    private readonly IUserRepository userRepository;

    public AuthController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost("login")]
    public ActionResult<UserDto> Login([FromBody] LoginRequest request)
    {
        User? user = userRepository.GetMany()
            .SingleOrDefault(u => u.Name == request.UserName);

        if (user is null || user.Password != request.Password)
        {
            return Unauthorized("Invalid username or password");
        }

        UserDto dto = new()
        {
            Id = user.UserId,
            UserName = user.Name
        };
        return Ok(dto);
    }
    
}