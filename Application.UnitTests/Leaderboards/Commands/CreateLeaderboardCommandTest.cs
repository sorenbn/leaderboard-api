using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Leaderboards.Commands.Create;
using Application.Features.Leaderboards.Queries.GetLeaderboard;
using Application.MappingProfiles;
using Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Leaderboards.Commands
{
    public class CreateLeaderboardCommandTest
    {
        private readonly IMapper mapper;
        private readonly Mock<ILeaderboardRepository> mockLeaderboardRepository;
        private readonly CreateLeaderboardCommand.Handler handler;

        public CreateLeaderboardCommandTest()
        {
            mockLeaderboardRepository = RepositoryMocks.GetLeaderboardRepository();

            var mappingConfigProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            mapper = mappingConfigProvider.CreateMapper();
            handler = new CreateLeaderboardCommand.Handler(mockLeaderboardRepository.Object, mapper);
        }

        [Fact]
        public async Task CreateLeaderboardCommand_ShouldReturnValidId()
        {
            var leaderboard = new CreateLeaderboardCommand
            {
                Name = "Test",
                MinAcceptedValue = 1,
                MaximumAcceptedValue = 1000
            };

            var result = await handler.Handle(leaderboard, CancellationToken.None);
            var allLeaderboards = await mockLeaderboardRepository.Object.GetAllAsync();

            result.ShouldNotBeSameAs(Guid.Empty);
            allLeaderboards.Count.ShouldBe(4);
        }

        [Fact]
        public async Task CreateLeaderboardCommand_ShouldBePossibleToCreateWithoutMinMaxAcceptedRange()
        {
            var leaderboard = new CreateLeaderboardCommand
            {
                Name = "Test",
            };

            var result = await handler.Handle(leaderboard, CancellationToken.None);
            var allLeaderboards = await mockLeaderboardRepository.Object.GetAllAsync();

            result.ShouldNotBeSameAs(Guid.Empty);
            allLeaderboards.Count.ShouldBe(4);
        }

        [Fact]
        public async Task CreateLeaderboardCommand_NoNameShouldThrowValidationException()
        {
            var leaderboard = new CreateLeaderboardCommand();
            await Should.ThrowAsync<ValidationException>(async () => 
            {
                await handler.Handle(leaderboard, CancellationToken.None);
            });
        }
    }
}
