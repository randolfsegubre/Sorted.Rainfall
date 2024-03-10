using Rainfall.Data.Interface;

namespace Rainfall.Data
{
    // Represents an error response returned by the API
    public class ErrorResponse : IErrorResponse
    {
        public string Message { get; set; }
        public List<IErrorDetail> Details { get; set; }
    }
}
