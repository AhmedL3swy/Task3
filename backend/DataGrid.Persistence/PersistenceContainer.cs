using DataGrid.Application.Contracts;
using DataGrid.Persistence.Repositories;
using DataGrid.Persistence.SingletonServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataGrid.Persistence
{
    public static class PersistenceContainer
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped(typeof(ISearchRepository<>), typeof(SearchRepository<>));
            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddSingleton<IMaritalStatusService, MaritalStatusService>();
            services.AddScoped<UserRepository>();

            return services;
        }
    }
}
