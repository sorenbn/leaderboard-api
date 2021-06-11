using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ScoreEntryRepository : BaseRepository<ScoreEntry>, IScoreEntryRepository
    {
        public ScoreEntryRepository(LeaderboardAPIDbContext dbContext) 
            : base(dbContext)
        {

        }

        public async Task<ScoreEntry> GetScoreEntryByUsernameAndLeaderboardId(string username)
        {
            return await dbContext.ScoreEntries.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<ScoreEntry> GetScoreEntryByUsernameAndLeaderboardId(string username, Guid leaderboardId)
        {
            return await dbContext.ScoreEntries
                .FirstOrDefaultAsync(x => x.Username == username && x.LeaderboardId == leaderboardId);
        }
    }
}
