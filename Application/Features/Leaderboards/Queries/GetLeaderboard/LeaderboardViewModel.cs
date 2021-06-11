using System;

namespace Application.Features.Leaderboards.Queries.GetLeaderboard
{
    public class LeaderboardViewModel
    {
        public Guid Id { get; set; }

        public string Name
        {
            get; set;
        }
        public int? MinAcceptedValue
        {
            get; set;
        }
        public int? MaximumAcceptedValue
        {
            get; set;
        }
    }
}
