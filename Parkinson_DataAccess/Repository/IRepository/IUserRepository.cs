using Parkinson_Models.Dto;

namespace Parkinson_DataAccess.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsuniqueUser(string username);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<UserDto> Register(RegistrationRequestDto registrationRequestDto);

    }
}
