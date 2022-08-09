using Loan.API.Error;
using Loan.API.Helpers;
using Loan.Core.Interfaces;
using Loan.Infrastructure.Data;
using Loan.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Loan.API.Extensions
{
    public static class InjectCustomServices
    {
        public static void InjectDbContext(this WebApplicationBuilder builder)
        {
            //Configure Database
            builder.Services.AddDbContext<LoanContext>(x => x.UseSqlite(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlite(
              builder.Configuration.GetConnectionString("IdentityDefaultConnection")));
        }

        public static void InjectServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IRepositoryWrapper,RepositoryWrapper>();
            builder.Services.AddScoped<ITokenService, TokenService>();
        }

        public static void InjectEssentials(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(x => x.Value.Errors.Count() > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationError()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            builder.Services.AddEndpointsApiExplorer();

        }




    }
}
