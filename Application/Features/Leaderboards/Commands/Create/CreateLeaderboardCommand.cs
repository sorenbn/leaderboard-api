using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Leaderboards.Commands.Create
{
    public class CreateLeaderboardCommand : IRequest<Guid>
    {
        public string Name
        {
            get; set;
        }
        public int? MinAcceptedValue
        {
            get; set;
        }
        public int? MaximumAcceptedValue
        {
            get; set;
        }

        public class Handler : IRequestHandler<CreateLeaderboardCommand, Guid>
        {
            private readonly ILeaderboardRepository leaderboardRepository;
            private readonly IMapper mapper;

            public Handler(ILeaderboardRepository leaderboardRepository, IMapper mapper)
            {
                this.leaderboardRepository = leaderboardRepository;
                this.mapper = mapper;
            }

            public async Task<Guid> Handle(CreateLeaderboardCommand request, CancellationToken cancellationToken)
            {
                var leaderboard = mapper.Map<Leaderboard>(request);
                leaderboard = await leaderboardRepository.CreateAsync(leaderboard);

                return leaderboard.Id;
            }
        }
    }
}
