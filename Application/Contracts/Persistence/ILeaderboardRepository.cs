using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface ILeaderboardRepository : IAsyncRepository<Leaderboard>
    {
        Task<Leaderboard> GetWithAllScoreEntriesByIdAsync(Guid id);
    }
}
