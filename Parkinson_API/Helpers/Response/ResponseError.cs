namespace Parkinson_API.Helpers.Response
{
    public class ResponseError
    {
        public string? Domain { get; set; }
        public List<ResponseErrorMessage> Messages { get; set; } = new();
    }
}
