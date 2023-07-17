using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parkinson_API.Helpers.Response;
using Parkinson_DataAccess.Data;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models;
using Parkinson_Models.Dto.ClickTestDtos;
using Parkinson_Models.Dto.MemoryTestDtos;
using Parkinson_Models.Dto.SpiralTestDtos;
using System.Net;
using System.Security.Claims;

namespace Parkinson_API.Controllers.V2
{
    [Route("api/v{vertion:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [Authorize(Roles = "Patient,Doctor,Admin")]
    public class TestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapping;
        private readonly ILogger<TestsController> _logger;

        public TestsController(IUniteOfWork uniteOfWork, IMapper mapping, ILogger<TestsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _uniteOfWork = uniteOfWork;
            _mapping = mapping;
            _logger = logger;
        }


        [HttpGet("GetResultToClickTestByPatientId")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<IEnumerable<ClickTestDto>>>> GetResultToClickTestByPatientId(int id)
        {
            var userResultTests = await _uniteOfWork.Click.GetAllAsync(p => p.Patient.Id == id);
            if (userResultTests == null || !userResultTests.Any())
            {
                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.NotFound, false, null, new List<ResponseErrorMessage>()
                {
                    new ResponseErrorMessage()
                    {
                        Message = "this user not found",
                        Code = "404",
                        Title = "NotFound"
                    }
                });
                return new JsonResult(responseGenerator.Generate());
            }
            else
            {
                var res = _mapping.Map<IEnumerable<ClickTestDto>>(userResultTests);
                var responseGenerator = new ResponseGenerator<IEnumerable<ClickTestDto>>(HttpStatusCode.OK, true, res, null);
                return new JsonResult(responseGenerator.Generate());
            }
        }


        [HttpPost("AddResultToClickTestByPatientId")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<ClickTestDto>>> AddResultToClickTestByPatientId(CreateClickTestDto dto)
        {
            try
            {
                var createdTest = new ClickTest()
                {
                    LeftHand = dto.LeftHand,
                    RightHand = dto.RightHand,
                    PatientId = dto.PatientId,
                };
                var savedTest = await _uniteOfWork.Click.CreateAsync(createdTest);
                var res = _mapping.Map<ClickTestDto>(savedTest);
                var responseGenerator = new ResponseGenerator<ClickTestDto>(HttpStatusCode.OK, true, res, null);
                return new JsonResult(responseGenerator.Generate());

            }
            catch (Exception e)
            {
                var response = new ResponseGenerator<List<object>>(HttpStatusCode.BadRequest, false, null, new List<ResponseErrorMessage>()
                {

                    new ResponseErrorMessage()
                    {

                        Message = e.Message,

                    }
                });
                return new JsonResult(response.Generate());
            }


        }


        [HttpGet("GetResultToSpiralTestByPatientId")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<IEnumerable<SpiralTestDto>>>> GetResultToSpiralTestByPatientId(int id)
        {
            var userResultTests = await _uniteOfWork.Click.GetAllAsync(p => p.Patient.Id == id);
            if (userResultTests == null || !userResultTests.Any())
            {
                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.NotFound, false, null, new List<ResponseErrorMessage>()
                {
                    new ResponseErrorMessage()
                    {
                        Message = "this user not found",
                        Code = "404",
                        Title = "NotFound"
                    }
                });
                return new JsonResult(responseGenerator.Generate());
            }
            else
            {
                var res = _mapping.Map<IEnumerable<SpiralTestDto>>(userResultTests);
                var responseGenerator = new ResponseGenerator<IEnumerable<SpiralTestDto>>(HttpStatusCode.OK, true, res, null);
                return new JsonResult(responseGenerator.Generate());
            }
        }


        [HttpPost("AddResultToSpiralTestByPatientId")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<SpiralTestDto>>> AddResultToSpiralTestByPatientId(CreateSpiralTestDto dto)
        {
            try
            {
                var createdTest = new SpiralTest()
                {
                    Result = dto.Result,
                    PatientId = dto.PatientId,
                };
                var savedTest = await _uniteOfWork.Spiral.CreateAsync(createdTest);
                var res = _mapping.Map<SpiralTestDto>(savedTest);
                var responseGenerator = new ResponseGenerator<SpiralTestDto>(HttpStatusCode.OK, true, res, null);
                return new JsonResult(responseGenerator.Generate());

            }
            catch (Exception e)
            {
                var response = new ResponseGenerator<List<object>>(HttpStatusCode.BadRequest, false, null, new List<ResponseErrorMessage>()
                {

                    new ResponseErrorMessage()
                    {

                        Message = e.Message,

                    }
                });
                return new JsonResult(response.Generate());
            }


        }


        [HttpGet("GetResultToMemoryTestByPatientId")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<IEnumerable<MemoryTestDto>>>> GetResultToMemoryTestByPatientId(int id)
        {
            var userResultTests = await _uniteOfWork.Memory.GetAllAsync(p => p.Patient.Id == id);
            if (userResultTests == null || !userResultTests.Any())
            {
                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.NotFound, false, null, new List<ResponseErrorMessage>()
                {
                    new ResponseErrorMessage()
                    {
                        Message = "this user not found",
                        Code = "404",
                        Title = "NotFound"
                    }
                });
                return new JsonResult(responseGenerator.Generate());
            }
            else
            {
                var res = _mapping.Map<IEnumerable<MemoryTestDto>>(userResultTests);
                var responseGenerator = new ResponseGenerator<IEnumerable<MemoryTestDto>>(HttpStatusCode.OK, true, res, null);
                return new JsonResult(responseGenerator.Generate());
            }
        }


        [HttpPost("AddResultToMemoryTestByPatientId")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<MemoryTestDto>>> AddResultToMemoryTestByPatientId(CreateMemoryTestDto dto)
        {
            try
            {
                var createdTest = new MemoryTest()
                {
                    Result = dto.Result,
                    PatientId = dto.PatientId,
                };
                var savedTest = await _uniteOfWork.Memory.CreateAsync(createdTest);
                var res = _mapping.Map<MemoryTestDto>(savedTest);
                var responseGenerator = new ResponseGenerator<MemoryTestDto>(HttpStatusCode.OK, true, res, null);
                return new JsonResult(responseGenerator.Generate());

            }
            catch (Exception e)
            {
                var response = new ResponseGenerator<List<object>>(HttpStatusCode.BadRequest, false, null, new List<ResponseErrorMessage>()
                {

                    new ResponseErrorMessage()
                    {

                        Message = e.Message,

                    }
                });
                return new JsonResult(response.Generate());
            }


        }





        [HttpGet("GetResultToReactionTestByPatientId")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<IEnumerable<ReactionTestDto>>>> GetResultToReactionTestByPatientId(int id)
        {
            var userResultTests = await _uniteOfWork.Reaction.GetAllAsync(p => p.Patient.Id == id);
            if (userResultTests == null || !userResultTests.Any())
            {
                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.NotFound, false, null, new List<ResponseErrorMessage>()
                {
                    new ResponseErrorMessage()
                    {
                        Message = "this user not found",
                        Code = "404",
                        Title = "NotFound"
                    }
                });
                return new JsonResult(responseGenerator.Generate());
            }
            else
            {
                var res = _mapping.Map<IEnumerable<ReactionTestDto>>(userResultTests);
                var responseGenerator = new ResponseGenerator<IEnumerable<ReactionTestDto>>(HttpStatusCode.OK, true, res, null);
                return new JsonResult(responseGenerator.Generate());
            }
        }


        [HttpPost("AddResultToReactionTestByPatientId")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<ReactionTestDto>>> AddResultToReactionTestByPatientId(CreateReactionTestDto dto)
        {
            try
            {
                var createdTest = new ReactionTest()
                {
                    Result = dto.Result,
                    PatientId = dto.PatientId,
                };
                var savedTest = await _uniteOfWork.Reaction.CreateAsync(createdTest);
                var res = _mapping.Map<ReactionTestDto>(savedTest);
                var responseGenerator = new ResponseGenerator<ReactionTestDto>(HttpStatusCode.OK, true, res, null);
                return new JsonResult(responseGenerator.Generate());

            }
            catch (Exception e)
            {
                var response = new ResponseGenerator<List<object>>(HttpStatusCode.BadRequest, false, null, new List<ResponseErrorMessage>()
                {

                    new ResponseErrorMessage()
                    {

                        Message = e.Message,

                    }
                });
                return new JsonResult(response.Generate());
            }


        }



        [HttpGet("GetResult")]
        [MapToApiVersion("2.0")]
        [Authorize(Roles = "Patient,Doctor,Admin")]
        public async Task<ActionResult<ResponseGenerator<IEnumerable<ClickTestDto>>>> GetResult(int id)
        {

            // Get the authenticated user's ID
            string userId = User.Identity.Name;


            // Check if the user is authorized to access the requested patient's data
            bool isAuthorized = IsAuthorized(userId, id, User);

            if (!isAuthorized)
            {
                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.Forbidden, false, null, new List<ResponseErrorMessage>()
                {
                     new ResponseErrorMessage()
                        {
                             Message = "Access denied",
                             Code = "403",
                             Title = "Forbidden"
                        }
                });
                return new JsonResult(responseGenerator.Generate());
            }

            var userResultTests = await _uniteOfWork.Click.GetAllAsync(p => p.Patient.Id == id);

            if (userResultTests == null || !userResultTests.Any())
            {
                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.NotFound, false, null, new List<ResponseErrorMessage>()
        {
            new ResponseErrorMessage()
            {
                Message = "This user not found",
                Code = "404",
                Title = "NotFound"
            }
        });
                return new JsonResult(responseGenerator.Generate());
            }
            else
            {
                var res = _mapping.Map<IEnumerable<ClickTestDto>>(userResultTests);
                var responseGenerator = new ResponseGenerator<IEnumerable<ClickTestDto>>(HttpStatusCode.OK, true, res, null);
                return new JsonResult(responseGenerator.Generate());
            }
        }

        private bool IsAuthorized(string userId, int patientId, ClaimsPrincipal user)
        {
            var Patient = (_context.Patients.FirstOrDefault(p => p.Id == patientId));
            // Check if the user is an admin
            if (user.IsInRole("Admin"))
            {
                return true;
            }

            // Check if the user is a doctor and has access to the patient
            if (user.IsInRole("Doctor") && userId == Patient.Doctor.UserId)
            {
                return true;
            }

            // Check if the user is a patient and requesting their own data
            if (user.IsInRole("Patient") && userId == Patient.UserId)
            {
                return true;
            }

            return false;
        }



    }
}
