using Microsoft.AspNetCore.Mvc;
using UberDinner.Application.Services.Authentication;
using UberDinner.Contracts.Authentication;

namespace UberDinner.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationSerive _authenticationSerive;

    public AuthenticationController(IAuthenticationSerive authenticationSerive)
    {
        _authenticationSerive = authenticationSerive;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationSerive.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationSerive.Login(request.Email, request.Password);

        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
        return Ok(response);
    }
}
