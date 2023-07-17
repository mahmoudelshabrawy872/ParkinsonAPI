using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parkinson_API.Helpers.Response;
using Parkinson_DataAccess.Data;
using System.Net;

namespace Parkinson_API.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{vertion:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetPatientsToDoctorByDoctorId")]
        public IActionResult GetPatientsToDoctorByDoctorId(int id)
        {
            try
            {
                var doctor = _context.Doctors.Find(id);
                if (doctor == null)
                {
                    var response = new ResponseGenerator<object>(HttpStatusCode.BadRequest, false, null, new List<ResponseErrorMessage>()
                {
                    new ResponseErrorMessage()
                    {
                        Message = "this Doctor Not Exist",
                        Title ="BadRequst",
                        Code="400"
                    }
                });
                    return new JsonResult(response.Generate());
                }
                else
                {
                    var patients = _context.Patients.FirstOrDefault(p => p.DoctorId == id);
                    var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.OK, true, patients, null);
                    return new JsonResult(responseGenerator.Generate());
                }
            }
            catch (Exception e)
            {
                var response = new ResponseGenerator<object>(HttpStatusCode.BadRequest, false, null, new List<ResponseErrorMessage>()
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
        [HttpGet("GetDoctors")]
        public IActionResult GetDoctors()
        {
            try
            {
                var doctor = _context.Doctors.ToList();

                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.OK, true, doctor, null);
                return new JsonResult(responseGenerator.Generate());
            }
            catch (Exception e)
            {
                var response = new ResponseGenerator<object>(HttpStatusCode.BadRequest, false, null, new List<ResponseErrorMessage>()
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
        [HttpPost("AddPatientToDoctor")]
        [Authorize(Roles = "Patient,Doctor,Admin")]
        public IActionResult AddPatientToDoctor(int doctorId, int patientId)
        {
            try
            {


                var doctorSearch = _context.Doctors.SingleOrDefault(d => d.Id == doctorId);
                if (doctorSearch is null)
                {
                    var res = new ResponseGenerator<object>(HttpStatusCode.NotFound, false, null, new() { new() { Message = "this DoctorId Not Found", Code = "404" } });
                    return new JsonResult(res.Generate());

                }
                var searchedPatient = _context.Patients.SingleOrDefault(p => p.Id == patientId);
                if (searchedPatient is null)
                {
                    var response = new ResponseGenerator<object>(HttpStatusCode.NotFound, false, null, new() { new() { Message = "this PatientId Not Found", Code = "404" } });
                    return new JsonResult(response.Generate());
                }
                searchedPatient.DoctorId = doctorId;
                _context.SaveChanges();
                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.OK, true, new { isAdded = true }, null);
                return new JsonResult(responseGenerator.Generate());
            }
            catch (Exception e)
            {
                var response = new ResponseGenerator<object>(HttpStatusCode.BadRequest, false, null, new List<ResponseErrorMessage>()
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
        [HttpGet("GetPatientByDoctorId")]
        public IActionResult GetPatientByDoctorId(int doctorId)
        {
            try
            {
                var doctorSearch = _context.Patients.FirstOrDefault(d => d.DoctorId == doctorId);
                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.OK, true, doctorSearch, null);
                return new JsonResult(responseGenerator.Generate());
            }
            catch (Exception e)
            {
                var response = new ResponseGenerator<object>(HttpStatusCode.BadRequest, false, null, new List<ResponseErrorMessage>()
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
