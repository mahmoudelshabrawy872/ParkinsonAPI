using Microsoft.AspNetCore.Mvc;
using Parkinson_API.Helpers.Response;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models.Dto;
using System.Net;

namespace Parkinson_API.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{vertion:apiVersion}/[controller]")]
    public class LoginController : ControllerBase
    {


        private readonly IUserRepository _userRepository;
        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromQuery] LoginRequestDto dto)
        {
            var loginResponse = await _userRepository.Login(dto);
            if (loginResponse.User is null || string.IsNullOrEmpty(loginResponse.Token))
            {
                var Result = new ResponseGenerator<LoginResponseDto>(HttpStatusCode.BadRequest, false, null, new List<ResponseErrorMessage>()
                {
                    new ResponseErrorMessage()
                    {
                        Code = "400",
                        Message = "UserName or Password is Correct",
                        Title = "BadRequest"
                    }
                });
                return new JsonResult(Result.Generate());

            }
            var jsonResult = new ResponseGenerator<LoginResponseDto>(HttpStatusCode.OK, true, loginResponse, new List<ResponseErrorMessage>());
            return new JsonResult(jsonResult.Generate());
        }






    }
}
