using UberDinner.Application.Common.Errors;
using UberDinner.Application.Common.Interfaces.Authentication;
using UberDinner.Application.Common.Interfaces.Persistence;
using UberDinner.Domain.Entities;

namespace UberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationSerive
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository
        )
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Register(
            string firstname,
            string lastname,
            string email,
            string password
        )
        {
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                throw new DuplicateEmailException();
            }
            var user = new User
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Password = password
            };
            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }

        public AuthenticationResult Login(string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User with given email does not exists");
            }

            if (user.Password != password)
            {
                throw new Exception("Invalid Password.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
