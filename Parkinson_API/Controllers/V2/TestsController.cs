using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parkinson_API.Helpers.Response;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models;
using System.Net;
using ClickTest = Parkinson_Models.ClickTest;

namespace Parkinson_API.Controllers.V2
{
    [Route("api/v{vertion:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class TestsController : ControllerBase
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapping;
        private readonly ILogger<TestsController> _logger;

        public TestsController(IUniteOfWork uniteOfWork, IMapper mapping, ILogger<TestsController> logger)
        {
            _uniteOfWork = uniteOfWork;
            _mapping = mapping;
            _logger = logger;
        }


        [HttpGet("GetResultToClickTestByPatientId")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<IEnumerable<ClickTest>>>> GetResultToClickTestByPatientId(int id)
        {
            var userResultTests = await _uniteOfWork.Click.GetAllAsync(p => p.Patient.Id == id);
            if (userResultTests == null || id == 0)
            {
                var responseGenerator = new ResponseGenerator<Test>(HttpStatusCode.OK, false, null, new List<ResponseErrorMessage>()
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
                var responseGenerator = new ResponseGenerator<IEnumerable<ClickTest>>(HttpStatusCode.OK, true, userResultTests, null);
                return new JsonResult(responseGenerator.Generate());
            }
        }


        [HttpPost("AddResultToClickTestByPatientId")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<ClickTest>>> AddResultToClickTestByPatientId(int patientId, decimal leftHand, decimal rightHand)
        {
            try
            {
                var createdTest = new ClickTest()
                {
                    LeftHand = leftHand,
                    RightHand = rightHand,
                    PatientId = patientId
                };
                var res = await _uniteOfWork.Click.CreateAsync(createdTest);
                var responseGenerator = new ResponseGenerator<ClickTest>(HttpStatusCode.OK, true, createdTest, null);
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
        public async Task<ActionResult<ResponseGenerator<IEnumerable<SpiralTest>>>> GetResultToSpiralTestByPatientId(int id)
        {
            var userResultTests = await _uniteOfWork.Spiral.GetAllAsync(p => p.Patient.Id == id);
            if (userResultTests == null || !userResultTests.Any())
            {
                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.OK, false, null, new List<ResponseErrorMessage>()
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
                var responseGenerator = new ResponseGenerator<IEnumerable<SpiralTest>>(HttpStatusCode.OK, true, userResultTests, null);
                return new JsonResult(responseGenerator.Generate());
            }
        }


        [HttpPost("AddResultToSpiralTestByPatientId")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<SpiralTest>>> AddResultToSpiralTestByPatientId(int patientId, decimal result)
        {
            try
            {
                var createdTest = new SpiralTest()
                {
                    Result = result,
                    PatientId = patientId
                };
                var res = await _uniteOfWork.Spiral.CreateAsync(createdTest);
                var responseGenerator = new ResponseGenerator<SpiralTest>(HttpStatusCode.OK, true, createdTest, null);
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
    }
}
