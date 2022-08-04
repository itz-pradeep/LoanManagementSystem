using AutoMapper;
using Loan.API.Dtos.Loan;
using Loan.Core.Entities;
using Loan.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Loan.Core.Specifications;
using Loan.Core;
using Microsoft.AspNetCore.Authorization;
using Loan.API.Error;

namespace Loan.API.Controllers
{
    public class LoanController : BaseApiController
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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<LoanApplicationResponse>>> GetLoanApplicationByID(int id)
        {
            var spec = new LoanWithTypeAndStatus(id);
            var loanApplication = await _loanRepo.GetByFilterAsync(spec);
            return Ok(_mapper.Map<LoanApplicationResponse>(loanApplication));
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

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<LoanApplicationResponse>> PutLoanApplication(int id, [FromBody] UpdateLoanRequest request)
        {

            var loanApplication = await _loanRepo.GetByIdAsync(id);

            if (loanApplication == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var loanToSave = _mapper.Map<LoanApplication>(request);
            loanToSave.Id = id;
            loanToSave.ModifiedDate = DateTime.Now;
            loanToSave.LoanStatusId = loanApplication.LoanStatusId; //TODO statu by enum Pre approval

            await _loanRepo.PutAsync(loanToSave);

            return Ok();
        }


        [HttpGet("loanTypes")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<LoanTypesResponse>>> GetLoanTypes()
        {
            var loanTypes = await _loanTypeRepo.ListAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<LoanTypesResponse>>(loanTypes));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> CancelApplication(int id)
        {
            var loanApplication = await _loanRepo.GetByIdAsync(id);

            if (loanApplication == null)
            {
                return NotFound(new ApiResponse(404));
            }

            loanApplication.IsActive = false;

            await _loanRepo.PutAsync(loanApplication);

            return Ok();
        }

    }
}
