using Rainfall.Data.Interface;

namespace Rainfall.Data
{
    // Represents details of errors
    public class ErrorDetail : IErrorDetail
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
