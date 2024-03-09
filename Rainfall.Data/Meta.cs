using Rainfall.Data.Interfaces;

namespace Rainfall.Data
{
    public class Meta : IMeta
    {
        public string publisher { get; set; }
        public string licence { get; set; }
        public string documentation { get; set; }
        public string version { get; set; }
        public string comment { get; set; }
        public List<string> hasFormat { get; set; }
        public int limit { get; set; }
    }


}
