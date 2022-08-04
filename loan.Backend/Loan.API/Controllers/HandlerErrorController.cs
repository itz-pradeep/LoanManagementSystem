using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Loan.API.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Loan.API.Controllers
{
    [Route("handlerError/{code}")]
    public class HandlerErrorController : BaseApiController
    {
        [HttpGet]
        public ActionResult HandlerError(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}