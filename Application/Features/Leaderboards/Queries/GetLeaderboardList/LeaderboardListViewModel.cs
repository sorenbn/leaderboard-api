using Application.Features.Leaderboards.Queries.GetLeaderboard;
using System.Collections.Generic;

namespace Application.Features.Leaderboards.Queries.GetLeaderboardList
{
    public class LeaderboardListViewModel
    {
        public IEnumerable<LeaderboardViewModel> Leaderboards { get; set; }
    }
}
