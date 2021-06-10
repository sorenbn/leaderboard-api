using Application.Contracts.Persistence;
using Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;

namespace Application.Features.Leaderboards.Commands
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
            private readonly IMapper maper;

            public Handler(ILeaderboardRepository leaderboardRepository, IMapper maper)
            {
                this.leaderboardRepository = leaderboardRepository;
                this.maper = maper;
            }

            public async Task<Guid> Handle(CreateLeaderboardCommand request, CancellationToken cancellationToken)
            {
                CreateLeaderboardValidator validator = new ();
                var result = await validator.ValidateAsync(request, cancellationToken);

                if (result.Errors.Count > 0)
                    throw new ValidationException(result);

                var leaderboard = maper.Map<Leaderboard>(request);
                leaderboard = await leaderboardRepository.CreateAsync(leaderboard);

                return leaderboard.Id;
            }
        }
    }
}
