using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class AuthController(SupabaseAuthService auth) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var session = await auth.LoginAsync(dto.Email, dto.Password);
        return Ok(session);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginDto dto)
    {
        var user = await auth.RegisterAsync(dto.Email, dto.Password);
        return Ok(user);
    }

    [HttpGet("me")]
    public IActionResult Me()
    {
        var user = auth.GetCurrentUser();
        return Ok(user);
    }
}

public record LoginDto(string Email, string Password);
