namespace Rainfall.Data.Interface
{
    public interface IErrorResponse
    {
        List<ErrorDetail> Details { get; set; }
        string Message { get; set; }
    }
}