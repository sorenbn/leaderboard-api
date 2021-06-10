using Domain.Entities;
using System.Collections.Generic;

namespace Application.Features.Leaderboards.Queries.GetLeaderboard
{
    public class LeaderboardViewModel
    {
        public string Name
        {
            get; set;
        }

        // TODO: Replace with ScoreEntryViewModel 
        public IEnumerable<ScoreEntry> ScoreEntries
        {
            get; set;
        }
    }
}
