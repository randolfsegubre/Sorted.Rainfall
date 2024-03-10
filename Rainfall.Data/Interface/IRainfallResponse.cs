using Rainfall.Data.Interfaces;
using System.Net;

namespace Rainfall.Data.Interface
{
    public interface IRainfallResponse
    {
        bool Success { get; set; }
        HttpStatusCode StatusCode { get; set; } // Updated property to use HttpStatusCode enum
        IRoot Data { get; set; }
        IErrorResponse Error { get; set; }
    }
}