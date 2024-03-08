namespace Rainfall.Data.Interface
{
    public interface IErrorDetail
    {
        string Message { get; set; }
        string PropertyName { get; set; }
    }
}