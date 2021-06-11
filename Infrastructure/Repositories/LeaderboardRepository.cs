using Application.Contracts.Persistence;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class LeaderboardRepository : BaseRepository<Leaderboard>, ILeaderboardRepository
    {
        public LeaderboardRepository(LeaderboardAPIDbContext dbContext) : base(dbContext)
        {
        }
    }
}
