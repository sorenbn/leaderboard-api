using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ScoreEntry : AuditableEntity
    {
        public Guid Id
        {
            get; set;
        }
        public Guid LeaderboardId 
        { 
            get; set; 
        }
        public string Username
        {
            get; set;
        }
        public int ScoreValue
        {
            get; set;
        }
        [NotMapped]
        public int Rank
        {
            get; set;
        }
    }
}
