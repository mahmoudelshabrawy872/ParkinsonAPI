using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parkinson_API.Helpers.Response;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models;
using Parkinson_Models.Dto;
using System.Net;

namespace Parkinson_API.Controllers.V2
{
    [Route("api/v{vertion:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class TestController : ControllerBase
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapping;
        private readonly ILogger<TestController> _logger;

        public TestController(IUniteOfWork uniteOfWork, IMapper mapping, ILogger<TestController> logger)
        {
            _uniteOfWork = uniteOfWork;
            _mapping = mapping;
            _logger = logger;
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<List<TestDto>>>> GetAllTestesAsync()
        {
            var tests = await _uniteOfWork.Test.GetAllAsync();
            _logger.LogInformation("get list of test", DateTime.UtcNow.ToLongTimeString());


            var result = _mapping.Map<List<TestDto>>(tests);
            _logger.LogInformation("map list of test with list of testDto", DateTime.UtcNow.ToLongTimeString());

            var responseGenerator = new ResponseGenerator<List<TestDto>>(HttpStatusCode.OK, true, result, null);
            return new JsonResult(responseGenerator.Generate());
        }


        [HttpPut]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ResponseGenerator<Test>>> CreateNewTest([FromQuery] TestDto dto)
        {
            try
            {

                Test createdTest = new() { Name = dto.Name };
                var result = await _uniteOfWork.Test.CreateAsync(createdTest);


                var responseGenerator = new ResponseGenerator<Test>(HttpStatusCode.OK, true, result, null);
                return new JsonResult(responseGenerator.Generate());
            }
            catch (Exception e)
            {
                var response = new ResponseGenerator<List<Image>>(HttpStatusCode.BadRequest, false, null, new List<ResponseErrorMessage>()
                {

                    new ResponseErrorMessage()
                    {

                        Message = e.Message,
                        Title = e.Source
                    }
                });
                return new JsonResult(response.Generate());
            }


        }
    }
}
