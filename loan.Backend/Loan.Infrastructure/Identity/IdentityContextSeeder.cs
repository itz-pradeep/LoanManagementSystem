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
    public static class IdentityContextSeeder
    {
        public static async void SeedUsersData(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Pradeep Singh",
                    Email = "hacker@crio.com",
                    UserName = "hacker@crio.com",
                };

                await userManager.CreateAsync(user, "Pass@word1");
            }

        }
    }
}
