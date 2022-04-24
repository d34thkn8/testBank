
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quoting.Database.Database;
using Quoting.Database.Model;
using Quoting.Database.Repository;
namespace QuotingApi.MIddleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependecy(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }


        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChallengeDatabase>
             (
             options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            return services;
        }
    }
}
