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
    internal class LoanTypesConfig : IEntityTypeConfiguration<LoanType>
    {
        public void Configure(EntityTypeBuilder<LoanType> builder)
        {
            builder.Property(x=>x.Type).IsRequired();
            builder.Property(x=>x.IsActive).HasDefaultValue(true);
        }
    }
}
