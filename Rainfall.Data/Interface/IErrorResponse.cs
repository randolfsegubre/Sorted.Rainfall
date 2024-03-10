namespace Rainfall.Data.Interface
{
    public interface IErrorResponse
    {
        List<IErrorDetail> Details { get; set; }
        string Message { get; set; }
    }
}