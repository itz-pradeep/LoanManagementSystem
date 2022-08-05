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
        public static async void SeedUsersData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            SeedRoles(roleManager);
            await SeedUsersAsync(userManager);
        }

        private async static Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (userManager.FindByEmailAsync
("admin@lms.com").Result == null)
            {
                var user = new AppUser()
                {
                    DisplayName = "Administrator",
                    Email = "admin@lms.com",
                    UserName = "admin@lms.com",
                };

                var result = await userManager.CreateAsync(user, "Password@123");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Admin").Wait();
                }
            }

            if (userManager.FindByEmailAsync
("pradeep@lms.com").Result == null)
            {
                var user = new AppUser()
                {
                    DisplayName = "Pradeep Singh",
                    Email = "pradeep@lms.com",
                    UserName = "pradeep@lms.com",
                };

                var result = await userManager.CreateAsync(user, "Password@123");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "NormalUser").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("NormalUser").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "NormalUser";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            Console.WriteLine("Roles Inserted");


        }
    }
}
