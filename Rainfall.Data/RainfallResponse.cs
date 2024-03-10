using Rainfall.Data.Interface;
using Rainfall.Data.Interfaces;
using System.Net;

namespace Rainfall.Data
{
    public class RainfallResponse : IRainfallResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; } // Updated property to use HttpStatusCode enum
        public IRoot? Data { get; set; }
        public IErrorResponse? Error { get; set; }
    }
}
