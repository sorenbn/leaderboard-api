using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ScoreEntryRepository : BaseRepository<ScoreEntry>, IScoreEntryRepository
    {

        public ScoreEntryRepository(LeaderboardAPIDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<ScoreEntry>> GetPaginatedScoreEntries(Guid leadboardId, int pageNumber, int pageSize)
        {
            // TODO: Support for both desc/asce sorting
            // Also, this is probably inefficient with leaderboards which has _lots_ of score entries.
            // Could perhaps be improved by indexing

            var scores = await dbContext.ScoreEntries
                .Where(e => e.LeaderboardId == leadboardId)
                .OrderByDescending(e => e.ScoreValue)
                    .ThenBy(e => e.CreatedDate)
                .AsNoTracking()
                .ToListAsync();

            var rankedScores = scores.Select((e, i) => new ScoreEntry
            {
                Id = e.Id,
                CreatedBy = e.CreatedBy,
                CreatedDate = e.CreatedDate,
                LastModifiedBy = e.LastModifiedBy,
                LastModifiedDate = e.LastModifiedDate,
                LeaderboardId = e.LeaderboardId,
                Username = e.Username,
                ScoreValue = e.ScoreValue,
                Rank = i + 1
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

            return rankedScores;
        }

        public async Task<ScoreEntry> GetScoreEntryByUsernameAndLeaderboardId(Guid leaderboardId, string username)
        {
            return await dbContext.ScoreEntries
                .FirstOrDefaultAsync(e => e.LeaderboardId == leaderboardId && e.Username == username);
        }

        public async Task<int> GetTotalScoreEntryCount(Guid leaderboardId)
        {
            return await dbContext.ScoreEntries
                .Where(e => e.LeaderboardId == leaderboardId)
                .CountAsync();
        }
    }
}
