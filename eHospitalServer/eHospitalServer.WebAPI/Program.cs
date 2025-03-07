using DefaultCorsPolicyNugetPackage;
using eHospitalServer.Business;
using eHospitalServer.DataAccess;
using eHospitalServer.WebAPI.Middlewares;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultCors();

builder.Services.AddBusiness();
builder.Services.AddDataAccess(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

ExtensionsMiddleware.CreateFirstUser(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireAuthorization(policy =>
{
    policy.RequireClaim(ClaimTypes.NameIdentifier);
    policy.AddAuthenticationSchemes("Bearer");
}); ;

app.Run();
