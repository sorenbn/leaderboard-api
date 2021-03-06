using Application.Contracts.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LeaderboardAPIDbContext>(options => 
            {
                options.UseSqlServer(configuration.GetConnectionString("LeaderboardAPIConnectionString"));
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ILeaderboardRepository, LeaderboardRepository>();
            services.AddScoped<IScoreEntryRepository, ScoreEntryRepository>();

            return services;
        }
    }
}
