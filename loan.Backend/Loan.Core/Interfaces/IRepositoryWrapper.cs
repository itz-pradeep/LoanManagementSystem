﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Core.Interfaces
{
    public interface IRepositoryWrapper
    {
        ILoanRepository LoanRepo { get; }
        void Save();
    }
}
