using Microsoft.AspNetCore.Mvc;
using Parkinson_API.Helpers.Response;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models;
using System.Net;


namespace Parkinson_API.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{vertion:apiVersion}/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUniteOfWork _uniteOfWork;

        public ImageController(IWebHostEnvironment webHostEnvironment, IUniteOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewImagesAsync(List<IFormFile> files)
        {

            try
            {

                if (files == null || files.Count == 0)
                {
                    var response = new ResponseGenerator<List<Image>>(HttpStatusCode.BadRequest, false, null, new List<ResponseErrorMessage>()
                    {
                        new ResponseErrorMessage()
                        {
                            Code = null,
                            Message = "No files were uploaded",
                            Title = "BadRequest"
                        }
                    });
                    return new JsonResult(response.Generate());
                }

                var imageEntities = new List<Image>();

                foreach (var file in files)
                {

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    // Create a new Image entity and add it to the list
                    var image = new Image
                    {

                        ImageUrl = Path.Combine("images", fileName) // Relative path to the image
                    };

                    imageEntities.Add(image);
                }

                var savedImage = await _uniteOfWork.Image.CreateListAsync(imageEntities);
                var responseGenerator = new ResponseGenerator<List<Image>>(HttpStatusCode.OK, true, imageEntities, null);
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageAsync(int id)
        {
            try
            {
                var image = await _uniteOfWork.Image.GetAsync(i => i.Id == id);
                if (image == null)
                    return NotFound();

                // Delete the image file from the server
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, image.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                else
                {
                    var response = new ResponseGenerator<object>(HttpStatusCode.NotFound, false, null, new()
                    {
                        new()
                        {
                            Message = "Image file not found on the server.",
                            Title = "BadRequest"
                        }
                    });
                    return new JsonResult(response.Generate());
                }

                // Delete the image from the database
                await _uniteOfWork.Image.DeleteAsync(image);
                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.OK, true, "Image deleted successfully.", null);
                return new JsonResult(responseGenerator.Generate());
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.InternalServerError, false, null, new()
                {
                    new()
                    {
                        Message = "An error occurred while deleting the image.",
                        Title = "BadRequest"
                    }
                });
                return new JsonResult(responseGenerator.Generate());
            }
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomImages(short numberOfImages)
        {
            string GetImageUrl(Image image)
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                return $"{baseUrl}/{image.ImageUrl}";
            }
            try
            {
                var imageEntities = (await _uniteOfWork.Image.GetAllAsync()).ToList();
                var random = new Random();
                var randomImages = imageEntities.OrderBy(x => random.Next()).Take(numberOfImages).Select(image => new { Url = GetImageUrl(image) });

                var responseGenerator = new ResponseGenerator<IEnumerable<object>>(HttpStatusCode.OK, true, randomImages, null);
                return new JsonResult(responseGenerator.Generate());
            }
            catch (Exception ex)
            {
                var responseGenerator = new ResponseGenerator<object>(HttpStatusCode.InternalServerError, false, null, new()
                {
                    new()
                    {
                        Message = "An error occurred while git the image.",
                        Title = "BadRequest"
                    }
                });
                return new JsonResult(responseGenerator.Generate());
            }
        }



    }
}
