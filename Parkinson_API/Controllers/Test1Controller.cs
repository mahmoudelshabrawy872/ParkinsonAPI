using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parkinson_API.Helpers.Response;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models.Dto;
using System.Net;

namespace Parkinson_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> getall()
        {
            var X = await _repository.Test1.GetAllAsync();

            var R = _mapping.Map<List<Test1Dto>>(X);

            var resalut = new ResponseGenerator<List<Test1Dto>>(HttpStatusCode.OK, true, R, new List<ResponseErrorMessage>());
            return new JsonResult(resalut.Generate());

        }

    }
}
