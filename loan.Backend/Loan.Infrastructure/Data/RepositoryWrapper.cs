using Loan.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Infrastructure.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private LoanContext _repoContext;
        private ILoanRepository _loanRepo;
        public ILoanRepository LoanRepo {
            get
            {
                if (_loanRepo == null)
                {
                    _loanRepo = new LoanRepository(_repoContext);
                }
                return _loanRepo;
            }
        }

        public RepositoryWrapper(LoanContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
