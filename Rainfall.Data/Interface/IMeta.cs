namespace Rainfall.Data.Interfaces
{
    public interface IMeta
    {
        string comment { get; set; }
        string documentation { get; set; }
        List<string> hasFormat { get; set; }
        string licence { get; set; }
        int limit { get; set; }
        string publisher { get; set; }
        string version { get; set; }
    }
}