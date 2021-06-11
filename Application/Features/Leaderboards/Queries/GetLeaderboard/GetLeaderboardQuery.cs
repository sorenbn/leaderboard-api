using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Leaderboards.Queries.GetLeaderboard
{
    public class GetLeaderboardQuery : IRequest<LeaderboardViewModel>
    {
        public Guid Id
        {
            get; set;
        }

        public class Handler : IRequestHandler<GetLeaderboardQuery, LeaderboardViewModel>
        {
            private readonly ILeaderboardRepository leaderboardRepository;
            private readonly IMapper mapper;

            public Handler(ILeaderboardRepository leaderboardRepository, IMapper mapper)
            {
                this.leaderboardRepository = leaderboardRepository;
                this.mapper = mapper;
            }

            public async Task<LeaderboardViewModel> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
            {
                Leaderboard leaderboard = await leaderboardRepository.GetByIdAsync(request.Id);

                if (leaderboard == null)
                    throw new NotFoundException(nameof(leaderboard), request.Id);

                var leaderboardVm = mapper.Map<LeaderboardViewModel>(leaderboard);
                return leaderboardVm;
            }
        }
    }
}
