﻿using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using DropShipping.Dto.UserDto;
using DropShipping.Dto.UserDtos;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.Configuration;
using DropShipping.Shared.CustomExceptions;
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

        public LogInResponseDto LogInUser(LogInUserDto logInUserDto)
        {
            if (string.IsNullOrEmpty(logInUserDto.Email) || string.IsNullOrEmpty(logInUserDto.Password))
            {
                throw new NoDataException("Username & password are required fields!");
            }

            string hashPassword = HashPassword(logInUserDto.Password);
            User userDb = _userRepository.LogInUser(logInUserDto.Email, hashPassword);

            if (userDb == null)
            {
                throw new NotFoundException("User was not found!");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_settings.SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                new Claim(ClaimTypes.Name, userDb.FirstName),
                new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}"),
                new Claim("UserId", userDb.Id.ToString()),
                new Claim(ClaimTypes.Email, userDb.Email)

                    }
                )
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new LogInResponseDto
            {
                UserId = userDb.Id.ToString(),
                Token = token,
                UserName = userDb.FirstName
            };
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
                PhoneNumber = registerUserDto.PhoneNumber,
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
                throw new NoDataException("Username & Password are required fields!");
            }

            if (registerUserDto.Username.Length > 50)
            {
                throw new NoDataException("Username: Maximum length of the username is longer than 50 characters");
            }

            if ((string.IsNullOrEmpty(registerUserDto.FirstName)) || (string.IsNullOrEmpty(registerUserDto.LastName)))
            {
                throw new NoDataException("Firstname and Lastname are required fields");
            }

            if (registerUserDto.FirstName.Length > 30)
            {
                throw new NoDataException("Firstname: Firstname is longer than 30 characters");
            }

            if (registerUserDto.LastName.Length > 50)
            {
                throw new NoDataException("Lastname: Lastname is longer than 50 characters");
            }

            if (registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new NoDataException("Password doesn't match!");
            }

            var userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if (userDb != null)
            {
                throw new NoDataException($"The user with username: {registerUserDto.Username} already exists");
            }
        }
    }
}
