using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAPresentation(this IServiceCollection services)
        {
            return services;
        }
    }
}
