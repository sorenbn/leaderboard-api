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
            DateTime now = DateTime.Parse("2021-06-10 14:26:53.0995875");

            Random rnd = new();
            var entries = new List<object>();

            for (int i = 0; i < 500; i++)
            {
                entries.Add(new
                {
                    Id = Guid.NewGuid(),
                    ScoreValue = rnd.Next(10000),
                    Username = $"Sorne {i}",
                    CreatedDate = now,
                    LeaderboardId = spelunkyLeaderboardId,
                });
            }

            modelBuilder.Entity<ScoreEntry>().HasData(entries);

            modelBuilder.Entity<Leaderboard>().HasData(
                new Leaderboard
                {
                    Id = spelunkyLeaderboardId,
                    Name = "Spelunky Board",
                    CreatedDate = now,
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
