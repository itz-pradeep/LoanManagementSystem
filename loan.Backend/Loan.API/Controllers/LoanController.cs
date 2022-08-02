using AutoMapper;
using Loan.API.Dtos.Loan;
using Loan.Core.Entities;
using Loan.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Loan.Core.Specifications;
using Loan.Core;
using Microsoft.AspNetCore.Authorization;

namespace Loan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IGenericRepository<LoanApplication> _loanRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<LoanType> _loanTypeRepo;

        public LoanController(IGenericRepository<LoanApplication> loanRepo,
        IGenericRepository<LoanType> loanTypeRepo, IMapper mapper)
        {
            _loanTypeRepo = loanTypeRepo;
            _loanRepo = loanRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<LoanApplicationResponse>>> GetLoanApplications([FromQuery] LoanApplicationFilter filter)
        {
            var spec = new LoanWithTypeAndStatus(filter);
            var loanApplications = await _loanRepo.GetListByFilterAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<LoanApplicationResponse>>(loanApplications));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<LoanApplicationResponse>> PostLoanApplication([FromBody] CreateLoanRequest request)
        {

            var loanToSave = _mapper.Map<LoanApplication>(request);
            loanToSave.CreatedDate = DateTime.Now;
            loanToSave.ModifiedDate = DateTime.Now;
            loanToSave.LoanStatusId = 1; //TODO statu by enum Pre approval


            await _loanRepo.PostAsync(loanToSave);

            return Ok();
        }

        [HttpGet("loanTypes")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<LoanTypesResponse>>> GetLoanTypes()
        {
            var loanTypes = await _loanTypeRepo.ListAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<LoanTypesResponse>>(loanTypes));
        }

    }
}
