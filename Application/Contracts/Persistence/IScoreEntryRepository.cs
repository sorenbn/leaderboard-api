using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IScoreEntryRepository : IAsyncRepository<ScoreEntry>
    {
        Task<ScoreEntry> GetScoreEntryByUsernameAndLeaderboardId(string username, Guid leaderboardId);
    }
}
