using Microsoft.Extensions.DependencyInjection;
using UberDinner.Application.Services.Authentication;

namespace UberDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationSerive, AuthenticationService>();

        return services;
    }
}
