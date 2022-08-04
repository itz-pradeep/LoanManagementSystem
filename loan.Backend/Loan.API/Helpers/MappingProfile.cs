using AutoMapper;
using Loan.API.Dtos.Loan;
using Loan.Core.Entities;

namespace Loan.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<LoanApplication, LoanApplicationResponse>()
            .ForMember(dest => dest.LoanType, src => src.MapFrom(x => x.LoanType.Type))
            .ForMember(dest => dest.LoanTypeId, src => src.MapFrom(x => x.LoanType.Id))
            .ForMember(dest => dest.LoanStatus, src => src.MapFrom(x => x.LoanStatus.Status));

            CreateMap<CreateLoanRequest, LoanApplication>();
            CreateMap<UpdateLoanRequest, LoanApplication>();
            CreateMap<LoanType, LoanTypesResponse>();
        }
    }
}
