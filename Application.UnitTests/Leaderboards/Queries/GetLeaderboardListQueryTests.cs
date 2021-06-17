using Application.Contracts.Persistence;
using Application.Features.Leaderboards.Queries.GetLeaderboardList;
using Application.MappingProfiles;
using Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Leaderboards.Queries
{
    public class GetLeaderboardListQueryTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILeaderboardRepository> mockLeaderboardRepository;

        public GetLeaderboardListQueryTests()
        {
            mockLeaderboardRepository = RepositoryMocks.GetLeaderboardRepository();

            var mappingConfigProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            mapper = mappingConfigProvider.CreateMapper();
        }

        [Fact]
        public async Task GetLeaderboardListQuery_ShouldReturnFullList()
        {
            var handler = new GetLeaderboardListQuery.Handler(mockLeaderboardRepository.Object, mapper);
            var result = await handler.Handle(new GetLeaderboardListQuery(), CancellationToken.None);

            result.ShouldBeOfType<LeaderboardListViewModel>();
            result.Leaderboards.Count.ShouldBe(3);
        }
    }
}
