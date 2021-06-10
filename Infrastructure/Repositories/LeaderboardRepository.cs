using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LeaderboardRepository : BaseRepository<Leaderboard>, ILeaderboardRepository
    {
        public LeaderboardRepository(LeaderboardAPIDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Leaderboard> GetByIdWithScoreEntriesAsync(Guid id)
        {
            return await dbContext.Leaderboards
                .Include(l => l.ScoreEntries)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}
