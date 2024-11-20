using DropShipping.Dto.UserDto;
using DropShipping.Dto.UserDtos;

namespace DropShipping.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto registerUserDto);

        LogInResponseDto LogInUser(LogInUserDto logInUserDto);
    }
}
