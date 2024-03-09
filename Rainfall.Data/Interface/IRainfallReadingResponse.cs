namespace Rainfall.Data.Interfaces
{
    public interface IRainfallReadingResponse
    {
        string context { get; set; }
        List<Item> items { get; set; }
        Meta meta { get; set; }
    }
}