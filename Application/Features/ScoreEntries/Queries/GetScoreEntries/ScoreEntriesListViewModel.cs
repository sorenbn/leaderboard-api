using System.Collections.Generic;

namespace Application.Features.ScoreEntries.Queries.GetScoreEntries
{
    public class ScoreEntriesListViewModel
    {
        public IList<ScoreEntryViewModel> ScoreEntries
        {
            get; set;
        }
    }
}
