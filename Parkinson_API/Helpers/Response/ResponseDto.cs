using System.Net;

namespace Parkinson_API.Helpers.Response
{
    public class ResponseDto<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public ResponseData<T>? Data { get; set; }
        public List<ResponseError> Errors { get; set; } = new();


    }


}
