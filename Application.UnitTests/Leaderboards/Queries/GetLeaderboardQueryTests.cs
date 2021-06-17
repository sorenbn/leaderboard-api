using Application.Contracts.Persistence;
using Application.Exceptions;
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

namespace Application.UnitTests.Leaderboards.Queries
{
    public class GetLeaderboardQueryTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILeaderboardRepository> mockLeaderboardRepository;
        private readonly GetLeaderboardQuery.Handler handler;

        public GetLeaderboardQueryTests()
        {
            mockLeaderboardRepository = RepositoryMocks.GetLeaderboardRepository();

            var mappingConfigProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            mapper = mappingConfigProvider.CreateMapper();
            
            handler = new GetLeaderboardQuery.Handler(mockLeaderboardRepository.Object, mapper);
        }

        [Fact]
        public async Task GetLeaderboardQuery_ShouldReturnLeaderboard()
        {
            Guid leaderboardId = Guid.Parse("e2db890f-78a8-401e-bdc2-f05343e7403e");

            var result = await handler.Handle(new GetLeaderboardQuery()
            {
                Id = leaderboardId,
            }, CancellationToken.None);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(leaderboardId);
        }

        [Fact]
        public async Task GetLeaderboardQuery_EmptyIDShouldThrowValidationException()
        {
            await Should.ThrowAsync<ValidationException>(async () =>
            {
                await handler.Handle(new GetLeaderboardQuery(), CancellationToken.None);
            });
        }

        [Fact]
        public async Task GetLeaderboardQuery_InvalidIDShouldThrowNotFoundException()
        {
            await Should.ThrowAsync<NotFoundException>(async () =>
            {
                await handler.Handle(new GetLeaderboardQuery() 
                { 
                    Id = new Guid("a916805a-4048-43ca-840c-da7d9632ead3"),
                }, CancellationToken.None);
            });
        }
    }
}
