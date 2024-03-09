namespace Rainfall.Data.Interfaces
{
    public interface IItem
    {
        DateTime dateTime { get; set; }
        string id { get; set; }
        string measure { get; set; }
        double value { get; set; }
    }
}