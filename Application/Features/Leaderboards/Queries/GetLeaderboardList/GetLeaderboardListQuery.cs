using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Leaderboards.Queries.GetLeaderboardList
{
    public class GetLeaderboardListQuery : IRequest<LeaderboardListViewModel>
    {
        public class Handler : IRequestHandler<GetLeaderboardListQuery, LeaderboardListViewModel>
        {
            private readonly IAsyncRepository<Leaderboard> leaderboardRepository;
            private readonly IMapper mapper;

            public Handler(IAsyncRepository<Leaderboard> leaderboardRepository, IMapper mapper)
            {
                this.leaderboardRepository = leaderboardRepository;
                this.mapper = mapper;
            }

            public async Task<LeaderboardListViewModel> Handle(GetLeaderboardListQuery request, CancellationToken cancellationToken)
            {
                var leaderboards = await leaderboardRepository.GetAllAsync();
                return mapper.Map<IEnumerable<Leaderboard>, LeaderboardListViewModel>(leaderboards);
            }
        }
    }
}
