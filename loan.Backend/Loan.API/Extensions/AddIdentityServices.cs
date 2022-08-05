using Loan.Core.Entities;
using Loan.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Loan.API.Extensions
{
    public static class AddIdentityServices
    {
        public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;
            var core = builder.Services.AddIdentityCore<AppUser>();
            core = new IdentityBuilder(core.UserType, typeof(IdentityRole), core.Services);
            core.AddEntityFrameworkStores<AppIdentityDbContext>();
            core.AddSignInManager<SignInManager<AppUser>>();
            core.AddRoleManager<RoleManager<IdentityRole>>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });

            return builder;
        }
    }
}
