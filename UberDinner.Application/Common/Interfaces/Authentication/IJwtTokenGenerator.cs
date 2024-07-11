using UberDinner.Domain.Entities;

namespace UberDinner.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
