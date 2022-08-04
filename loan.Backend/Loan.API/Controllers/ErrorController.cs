using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loan.API.Error;
using Loan.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Loan.API.Controllers
{
    public class ErrorController : BaseApiController
    {
        private readonly ILogger<ErrorController> _logger;
        private readonly LoanContext _context;

        public ErrorController(LoanContext context)
        {
            _context = context;
        }

        [HttpGet("not-found")]
        public ActionResult GetNotFoundResponse()
        {
            var data = _context.LoanApplications.Find(0);
            if (data == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok();

        }

        [HttpGet("servererror")]
        public ActionResult GetServerErrorResponse()
        {
            var data = _context.LoanApplications.Find(0);
            var result = data.ToString();

            return Ok();
        }
        [HttpGet("bad-request")]
        public ActionResult GetBadRequestResponse()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("bad-request/{id}")]
        public ActionResult GetBadRequestWithId(int id)
        {
            return Ok();
        }


    }
}