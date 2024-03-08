namespace Rainfall.Data.Interface
{
    public interface IRainfallReading
    {
        decimal AmountMeasured { get; set; }
        DateTime DateMeasured { get; set; }
    }
}