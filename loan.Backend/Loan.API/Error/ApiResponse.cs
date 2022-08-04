using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.API.Error
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatuCode(statusCode);
        }

        private string GetDefaultMessageForStatuCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You have made a bad request.",
                404 => "Resource not found!",
                401 => "You are not authorized.",
                500 => "Internal server error.",
                _ => null
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}