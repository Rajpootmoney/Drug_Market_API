
namespace UberDinner.Application.Services.Authentication;

public interface IAuthenticationSerive
{
    AuthenticationResult Register(string firstname,
                               string lastname,
                               string email,
                               string password);
    AuthenticationResult Login(string email, string password);
}
