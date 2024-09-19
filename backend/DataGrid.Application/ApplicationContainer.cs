using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DataGrid.Application
{
    public static class ApplicationContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
        public static IServiceCollection AddMediatorGenericHandelers(this IServiceCollection services)
        {
            return services;

        }
    }
}
