using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Parkinson_API.Helpers.Response;
using Parkinson_DataAccess.Data;
using Parkinson_Models;
using Parkinson_Models.Dto;
using System.Net;

namespace Parkinson_API.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{vertion:apiVersion}/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public RegisterController(ApplicationDbContext context, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        [HttpPost("DoctorRegister")]
        public async Task<IActionResult> DoctorRegister([FromQuery] DoctorRegisterRequestDto dto)
        {
            IdentityUser user = _mapper.Map<IdentityUser>(dto);
            try
            {
                var userCreation = await _userManager.CreateAsync(user, dto.Password);
                if (userCreation.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Doctor");
                    Doctor doctor = new()
                    {
                        Name = dto.UserName,
                        DateOfBirth = dto.DateOfBirth,
                        UserId = user.Id,
                    };
                    await _context.AddAsync(doctor);
                    await _context.SaveChangesAsync();
                    var jsonResult = new ResponseGenerator<DoctorRegisterResponseDto>(HttpStatusCode.OK, true, new DoctorRegisterResponseDto()
                    {
                        DateOfBirth = doctor.DateOfBirth,
                        Name = doctor.Name,
                        PhoneNumber = user.PhoneNumber,
                        UserName = user.UserName

                    }, new List<ResponseErrorMessage>());
                    return new JsonResult(jsonResult.Generate());
                }
                else
                {
                    var errorList = userCreation.Errors.ToList();
                    List<ResponseErrorMessage> responseErrorMessages = new List<ResponseErrorMessage>();
                    foreach (var itemIdentityError in errorList)
                    {
                        ResponseErrorMessage error = new()
                        {
                            Code = "400",
                            Message = itemIdentityError.Description,
                            Title = itemIdentityError.Code
                        };
                        responseErrorMessages.Add(error);
                    }
                    var jsonResult = new ResponseGenerator<DoctorRegisterRequestDto>(HttpStatusCode.BadRequest, false, null, responseErrorMessages);
                    return new JsonResult(jsonResult.Generate());
                }
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        [HttpPost("PatentRegister")]
        public async Task<IActionResult> PatentRegister([FromQuery] PatientRegisterRequestDto dto)
        {
            IdentityUser user = _mapper.Map<IdentityUser>(dto);

            try
            {
                var userCreation = await _userManager.CreateAsync(user, dto.Password);
                if (userCreation.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Patient");
                    Patient patient = new()
                    {
                        Name = dto.UserName,
                        DateOfBirth = dto.DateOfBirth,
                        UserId = user.Id,
                    };
                    await _context.AddAsync(patient);
                    await _context.SaveChangesAsync();
                    var jsonResult = new ResponseGenerator<PatientRegisterRequestDto>(HttpStatusCode.OK, true, new PatientRegisterRequestDto()
                    {
                        DateOfBirth = patient.DateOfBirth,
                        Name = patient.Name,
                        PhoneNumber = user.PhoneNumber,
                        UserName = user.UserName

                    }, new List<ResponseErrorMessage>());
                    return new JsonResult(jsonResult.Generate());

                }
                else
                {
                    var errorList = userCreation.Errors.ToList();
                    List<ResponseErrorMessage> responseErrorMessages = new List<ResponseErrorMessage>();
                    foreach (var itemIdentityError in errorList)
                    {
                        ResponseErrorMessage error = new()
                        {
                            Code = "400",
                            Message = itemIdentityError.Description,
                            Title = itemIdentityError.Code
                        };
                        responseErrorMessages.Add(error);
                    }
                    var jsonResult = new ResponseGenerator<PatientRegisterRequestDto>(HttpStatusCode.BadRequest, false, null, responseErrorMessages);
                    return new JsonResult(jsonResult.Generate());

                }
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }


    }
}
