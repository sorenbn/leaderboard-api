using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IScoreEntryRepository : IAsyncRepository<ScoreEntry>
    {
        Task<IEnumerable<ScoreEntry>> GetPaginatedScoreEntries(Guid leadboardId, int pageNumber, int pageSize);
        Task<int> GetTotalScoreEntryCount(Guid leaderboardId);
        Task<ScoreEntry> GetScoreEntryByUsernameAndLeaderboardId(Guid leaderboardId, string username);
    }
}
