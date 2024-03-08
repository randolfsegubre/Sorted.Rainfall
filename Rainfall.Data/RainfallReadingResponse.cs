using Rainfall.Data.Interface;

namespace Rainfall.Data
{
    // Represents the response containing a list of rainfall readings
    public class RainfallReadingResponse : IRainfallReadingResponse
    {
        public List<RainfallReading> Readings { get; set; }
    }
}
