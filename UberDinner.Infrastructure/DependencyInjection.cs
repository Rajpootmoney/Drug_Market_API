using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UberDinner.Application.Common.Interfaces.Authentication;
using UberDinner.Application.Common.Interfaces.Persistence;
using UberDinner.Application.Common.Interfaces.Services;
using UberDinner.Infrastructure.Authentication;
using UberDinner.Infrastructure.Persistence;
using UberDinner.Infrastructure.Services;

namespace UberDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
