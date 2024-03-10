namespace Rainfall.Data.Interfaces
{
    public interface IRoot
    {
        string Context { get; set; }
        List<Item> items { get; set; }
        Meta meta { get; set; }
    }
}