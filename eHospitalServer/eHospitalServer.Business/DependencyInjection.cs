using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eHospitalServer.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(
        this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;

        // FluentValidation AspNetCore indirmemiz gerek DependencyInject etmek için
        //Otomatik dependency injection yapmak için bu iki paket.
    }
}
