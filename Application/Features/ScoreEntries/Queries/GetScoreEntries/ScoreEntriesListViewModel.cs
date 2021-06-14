using System.Collections.Generic;

namespace Application.Features.ScoreEntries.Queries.GetScoreEntries
{
    public class ScoreEntriesListViewModel
    {
        public IEnumerable<ScoreEntryViewModel> ScoreEntries
        {
            get; set;
        }
    }
}
