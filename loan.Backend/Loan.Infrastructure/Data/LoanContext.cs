using Loan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Infrastructure.Data
{
    public class LoanContext : DbContext
    {
        public LoanContext(DbContextOptions<LoanContext> options) : base(options)
        {
        }

        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<LoanStatus> LoanStatus { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityTypes in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityTypes.ClrType.GetProperties().Where(x => x.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityTypes.Name).Property(property.Name).HasConversion<double>(); ;
                    }
                }
            }
        }
    }
}
