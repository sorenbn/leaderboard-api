using System;

namespace Application.Features.ScoreEntries.Queries.GetScoreEntries
{
    public class ScoreEntryViewModel
    {
        public string Username
        {
            get; set;
        }
        public int ScoreValue
        {
            get; set;
        }
        public DateTime DatePosted
        {
            get; set;
        }
    }
}
