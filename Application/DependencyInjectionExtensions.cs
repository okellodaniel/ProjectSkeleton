using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjectionExtensions).Assembly;
            services.AddMediatR(C => C.RegisterServicesFromAssemblies(assembly));
            services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}
