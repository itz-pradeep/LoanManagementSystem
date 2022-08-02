using Loan.API.Extensions;
using Loan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



ConfigureServices();

Configure();


void ConfigureServices()
{
    builder.InjectDbContext();
    builder.InjectServices();
    builder.InjectEssentials();
    builder.AddIdentity();
    builder.AddSwaggerDocumentation();
    builder.Services.AddCors(opt =>
           {
               opt.AddPolicy("CorsPolicy", policy =>
               {
                   policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
               });
           });
}

void Configure()
{

    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("CorsPolicy");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    //apply migrations
    app.MigrateDatabase();

    app.Run();
}

