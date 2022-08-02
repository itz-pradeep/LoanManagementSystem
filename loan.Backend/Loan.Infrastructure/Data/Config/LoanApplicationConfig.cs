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
    internal class LoanApplicationConfig : IEntityTypeConfiguration<LoanApplication>
    {
        public void Configure(EntityTypeBuilder<LoanApplication> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PropertyAddress).IsRequired().HasMaxLength(500);
            builder.Property(x => x.LoanTenure).IsRequired();
            builder.Property(x => x.LoanAmount).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasOne(x => x.LoanStatus).WithMany()
                .HasForeignKey(x=>x.LoanStatusId);
            builder.HasOne(x => x.LoanType).WithMany()
                .HasForeignKey(x => x.LoanTypeId);  
            builder.Property(x => x.IsActive).HasDefaultValue(true);
        }
    }
}
