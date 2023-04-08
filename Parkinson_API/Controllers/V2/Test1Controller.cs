using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parkinson_API.Helpers.Response;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models.Dto;
using System.Net;

namespace Parkinson_API.Controllers.V2
{
    [Route("api/v{vertion:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class Test1Controller : ControllerBase
    {
        private readonly IUniteOfWork _repository;
        private readonly IMapper _mapping;
        private readonly ILogger<Test1Controller> _logger;

        public Test1Controller(IUniteOfWork repository, IMapper mapping, ILogger<Test1Controller> logger)
        {
            _repository = repository;
            _mapping = mapping;
            _logger = logger;
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> getall()
        {
            var X = await _repository.Test1.GetAllAsync();
            _logger.LogInformation("get ienumreble of test1", DateTime.UtcNow.ToLongTimeString());
            var R = _mapping.Map<List<Test1Dto>>(X);
            _logger.LogInformation("mapp ienumreble of test1 with list of test dto", DateTime.UtcNow.ToLongTimeString());

            var resalut = new ResponseGenerator<List<Test1Dto>>(HttpStatusCode.OK, true, R, new List<ResponseErrorMessage>());
            return new JsonResult(resalut.Generate());

        }

    }
}
