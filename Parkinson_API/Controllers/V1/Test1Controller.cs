using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parkinson_API.Helpers.Response;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models.Dto;
using System.Net;

namespace Parkinson_API.Controllers.V1
{
    [Route("api/v{vertion:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class Test1Controller : ControllerBase
    {
        private readonly IUniteOfWork _repository;
        private readonly IMapper _mapping;

        public Test1Controller(IUniteOfWork repository, IMapper mapping)
        {
            _repository = repository;
            _mapping = mapping;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> getall()
        {
            var X = await _repository.Test.GetAllAsync();

            var R = _mapping.Map<List<TestDto>>(X);

            var resalut = new ResponseGenerator<List<TestDto>>(HttpStatusCode.OK, true, R, new List<ResponseErrorMessage>());
            return new JsonResult(resalut.Generate());

        }

    }
}
