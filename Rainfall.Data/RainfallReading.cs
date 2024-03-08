using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainfall.Data.Interface;

namespace Rainfall.Data
{
    // Represents a single rainfall reading
    public class RainfallReading : IRainfallReading
    {
        public DateTime DateMeasured { get; set; }
        public decimal AmountMeasured { get; set; }
    }
}
