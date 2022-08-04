using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.API.Error
{
    public class ApiValidationError : ApiResponse
    {
        public ApiValidationError() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}