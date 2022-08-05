using Loan.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Loan.Infrastructure.Data
{
    public static class LoanContextSeeder
    {
        public static void SeedData(LoanContext context)
        {
            string folderPath = "../Loan.Infrastructure/Data/SeedData/";

            //add loan types
            if (!context.LoanTypes.Any())
            {
                var loanTypeData = File.ReadAllText($"{folderPath}LoanTypes.json");

                var loanTypes = JsonSerializer.Deserialize<List<LoanType>>(loanTypeData);
                foreach (var loanType in loanTypes)
                {
                    context.LoanTypes.Add(loanType);
                }

            }

            //add loan types
            if (!context.LoanTypes.Any())
            {
                var loanStatuses = File.ReadAllText($"{folderPath}LoanStatus.json");

                var loanStatusData = JsonSerializer.Deserialize<List<LoanStatus>>(loanStatuses);

                foreach (var loanStatus in loanStatusData)
                {
                    context.LoanStatus.Add(loanStatus);
                }

            }

            context.SaveChanges();
        }
    }
}
