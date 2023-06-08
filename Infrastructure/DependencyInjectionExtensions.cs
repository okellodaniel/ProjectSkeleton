using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrustructure(this IServiceCollection services)
        {
            return services;
        }
    }
}
