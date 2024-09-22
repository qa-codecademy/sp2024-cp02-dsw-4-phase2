using DropShipping.Dto.UserDto;

namespace DropShipping.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto registerUserDto);

        string LogInUser(LogInUserDto logInUserDto);
    }
}
