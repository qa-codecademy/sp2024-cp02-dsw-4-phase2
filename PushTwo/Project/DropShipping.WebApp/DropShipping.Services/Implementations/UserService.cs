using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using DropShipping.Dto.UserDto;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.Configuration;
using DropShipping.Shared.CustomExceptions.UserException;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace DropShipping.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private DroppshippingAppSettings _settings;

        public UserService(IUserRepository userRepository, IOptions<DroppshippingAppSettings> options)
        {
            _userRepository = userRepository;
            _settings = options.Value;
        }

        public string LogInUser(LogInUserDto logInUserDto)
        {
            if ((string.IsNullOrEmpty(logInUserDto.Username)) || (string.IsNullOrEmpty(logInUserDto.Password)))
            {
                throw new UserDataException("Username & passwords are required fields!");
            }

            string hashPassword = HashPassword(logInUserDto.Password);

            User userDb = _userRepository.LogInUser(logInUserDto.Username, hashPassword);
            if (userDb == null)
            {
                throw new UserNotFoundException("User was not found!");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_settings.SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, userDb.Username),
                        new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}"),
                        new Claim("userId", userDb.Id.ToString())
                    })
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            ValidateUser(registerUserDto);

            var hashPassword = HashPassword(registerUserDto.Password);

            User user = new User
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Username = registerUserDto.Username,
                Email = registerUserDto.Email,
                Password = hashPassword
            };

            _userRepository.Add(user);
        }

        private string HashPassword(string password)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            string hashPassword = Encoding.ASCII.GetString(hashBytes);

            return hashPassword;
        }

        private void ValidateUser(RegisterUserDto registerUserDto)
        {
            if ((string.IsNullOrEmpty(registerUserDto.Username)) || (string.IsNullOrEmpty(registerUserDto.Password)))
            {
                throw new UserDataException("Username & Password are required fields!");
            }

            if (registerUserDto.Username.Length > 50)
            {
                throw new UserDataException("Username: Maximum length of the username is longer than 50 characters");
            }

            if ((string.IsNullOrEmpty(registerUserDto.FirstName)) || (string.IsNullOrEmpty(registerUserDto.LastName)))
            {
                throw new UserDataException("Firstname and Lastname are required fields");
            }

            if (registerUserDto.FirstName.Length > 30)
            {
                throw new UserDataException("Firstname: Firstname is longer than 30 characters");
            }

            if (registerUserDto.LastName.Length > 50)
            {
                throw new UserDataException("Lastname: Lastname is longer than 50 characters");
            }

            if (registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new UserDataException("Password doesn't match!");
            }

            var userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if (userDb != null)
            {
                throw new UserDataException($"The user with username: {registerUserDto.Username} already exists");
            }
        }
    }
}
