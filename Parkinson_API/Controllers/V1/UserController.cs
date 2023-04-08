using Microsoft.AspNetCore.Mvc;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models.Dto;

namespace Parkinson_API.Controllers.V1
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _user;


        public UserController(IUserRepository user)
        {
            _user = user;

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginRequestDto dto)
        {
            var loginResponse = await _user.Login(dto);
            if (loginResponse.User is null || string.IsNullOrEmpty(loginResponse.Token))
            {
                return BadRequest("user or password is incorrect");
            }

            return Ok(loginResponse);
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromQuery] RegistrationRequestDto dto)
        {
            bool ifUserNameUnque = _user.IsuniqueUser(dto.UserName);
            if (!ifUserNameUnque)
            {

                return BadRequest("username is exists");
            }

            var user = await _user.Register(dto);
            if (user is null)
            {
                return BadRequest("error while registrastion");
            }
            else

                return Ok(user);
        }
    }
}
