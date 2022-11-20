using System;
using System.Net;

namespace WitnessReport.Models
{
    public class ApiException : WebException
    {
        public HttpStatusCode StatusCode { get; }

        public string ErrorMessage { get; }

        public ApiException(string message, HttpStatusCode statusCode, string errorMessage, Exception innerException)
        : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
