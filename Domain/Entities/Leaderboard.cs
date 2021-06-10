using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Leaderboard : AuditableEntity
    {
        public Guid Id
        {
            get; set;
        }
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
        public IEnumerable<ScoreEntry> ScoreEntries
        {
            get; set;
        }
    }
}
