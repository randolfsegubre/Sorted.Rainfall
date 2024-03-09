using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Services.Config
{
    public struct Constants
    {
        #region AppSettings
        public const string root = "{root}";
        public const string stationId = "{id}";
        public const string limit = "{limit}";
        #endregion #region AppSettings

        #region Error Messages
        public const string RainfallServiceErrorMsg = "RainfallDataService error!";
        public const string RainfallExternalApiResponseNotOk = "Rainfall External Api Response Not Ok!";
        public const string InternalServerErrorMsg = "Internal Server Error";
        #endregion Error Messages
    }
}
