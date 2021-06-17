using Application.Features.Leaderboards.Queries.GetLeaderboard;
using System.Collections.Generic;

namespace Application.Features.Leaderboards.Queries.GetLeaderboardList
{
    public class LeaderboardListViewModel
    {
        public ICollection<LeaderboardViewModel> Leaderboards { get; set; }
    }
}
