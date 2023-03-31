namespace Parkinson_API.Helpers.Response
{
    public class ResponseData<T>
    {
        public T? InnerData { get; set; }
        public string? Type { get; set; }
    }
}
