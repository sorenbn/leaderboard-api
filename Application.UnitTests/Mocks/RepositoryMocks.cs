using Application.Contracts.Persistence;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<ILeaderboardRepository> GetLeaderboardRepository()
        {
            var leaderboards = new List<Leaderboard>
            {
                new Leaderboard
                {
                    Id = new Guid("e2db890f-78a8-401e-bdc2-f05343e7403e"),
                    Name = "Spelunky Board",
                    CreatedDate = DateTime.Parse("2002-01-15 00:00:00.0000000"),
                    MinAcceptedValue = 1,
                    MaximumAcceptedValue = 9999,
                },
                new Leaderboard
                {
                    Id = new Guid("25d9dd5a-5676-40b8-9f78-9b4d573f39eb"),
                    Name = "Dak Suls Board",
                    CreatedDate = DateTime.Parse("2003-05-14 00:00:00.0000000"),
                    MinAcceptedValue = -100,
                    MaximumAcceptedValue = 100,
                },
                new Leaderboard
                {
                    Id = new Guid("d945baba-1e1e-4df8-94bb-ab3531d26ece"),
                    Name = "Elden Ring Board",
                    CreatedDate = DateTime.Parse("2021-01-21 00:00:00.0000000"),
                },
            };

            Mock<ILeaderboardRepository> mockLeaderboardRepository = new();

            mockLeaderboardRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(leaderboards);
            mockLeaderboardRepository.Setup(repo => repo.CreateAsync(It.IsAny<Leaderboard>())).ReturnsAsync(
                (Leaderboard leaderboard) => 
                {
                    leaderboards.Add(leaderboard);
                    return leaderboard;
                });
            mockLeaderboardRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
                (Guid id) =>
                {
                    return leaderboards.FirstOrDefault(l => l.Id == id);
                });

            return mockLeaderboardRepository;
        }
    }
}
