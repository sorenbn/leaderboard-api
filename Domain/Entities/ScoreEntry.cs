﻿using Domain.Common;
using System;

namespace Domain.Entities
{
    public class ScoreEntry : AuditableEntity
    {
        public Guid Id
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
        public int Rank
        {
            get; set;
        }
        public DateTime Submitted
        {
            get; set;
        }
    }
}