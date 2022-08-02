using Loan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Infrastructure.Data.Config
{
    internal class LoanStatusConfig : IEntityTypeConfiguration<LoanStatus>
    {
        public void Configure(EntityTypeBuilder<LoanStatus> builder)
        {
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValue(true);

        }
    }
}
