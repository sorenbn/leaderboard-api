using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class LeaderboardAPIDbContext : DbContext
    {
        public DbSet<Leaderboard> Leaderboards
        {
            get; set;
        }

        public DbSet<ScoreEntry> ScoreEntries 
        { 
            get; set; 
        }

        public LeaderboardAPIDbContext(DbContextOptions<LeaderboardAPIDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaderboardAPIDbContext).Assembly);

            Guid spelunkyLeaderboardId = Guid.Parse("B0788D2F-8003-43C1-92A4-EDC76A7C5DDE");

            Random rnd = new();
            DateTime starDate = new DateTime(1995, 1, 1);
            int dateRange = (DateTime.Today - starDate).Days;

            var entries = new List<object>();

            for (int i = 0; i < 10000; i++)
            {
                entries.Add(new
                {
                    Id = Guid.NewGuid(),
                    ScoreValue = rnd.Next(10000),
                    Username = $"Sorne {i}",
                    CreatedDate = starDate.AddDays(rnd.Next(dateRange)),
                    LeaderboardId = spelunkyLeaderboardId,
                });
            }

            modelBuilder.Entity<ScoreEntry>().HasData(entries);

            modelBuilder.Entity<Leaderboard>().HasData(
                new Leaderboard
                {
                    Id = spelunkyLeaderboardId,
                    Name = "Spelunky Board",
                    CreatedDate = starDate,
                    MinAcceptedValue = 1,
                    MaximumAcceptedValue = 9999,
                }
            );

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
