using Loan.API.Helpers;
using Loan.Core.Interfaces;
using Loan.Infrastructure.Data;
using Loan.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Loan.API.Extensions
{
    public static class InjectCustomServices
    {
        public static void InjectDbContext(this WebApplicationBuilder builder)
        {
            //Configure Database
            builder.Services.AddDbContext<LoanContext>(x=>x.UseSqlite(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlite(
              builder.Configuration.GetConnectionString("IdentityDefaultConnection")));
        }

        public static void InjectServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ITokenService, TokenService>();
        }

        public static void InjectEssentials(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
        }




    }
}
