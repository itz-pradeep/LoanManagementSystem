using Loan.Core.Entities;
using Loan.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Loan.API.Extensions
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();


                try
                {
                    using var loanContext = scope.ServiceProvider.GetRequiredService<LoanContext>();
                    loanContext.Database.Migrate();
                    LoanContextSeeder.SeedData(loanContext); //seed loan data

                    using var identityContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                    using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                    using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    identityContext.Database.Migrate();
                    IdentityContextSeeder.SeedUsersData(userManager, roleManager); //seed user data


                }
                catch (Exception ex)
                {
                    logger.LogInformation($"Applying migration failure..{ex.Message}");
                }
            }

            return webApp;
        }
    }
}
