using Microsoft.AspNetCore.Http;

namespace Parkinson_Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
    }


    public class common
    {
        public IFormFile? files { get; set; }

        public Image _fileAPI { get; set; }

    }
}
