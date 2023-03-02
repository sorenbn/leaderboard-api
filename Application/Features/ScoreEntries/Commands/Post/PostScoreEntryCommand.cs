using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ScoreEntries.Commands.Post
{
    public class PostScoreEntryCommand : IRequest
    {
        public Guid LeaderboardId
        {
            get; set;
        }
        public string Username
        {
            get; set;
        }
        public int ScoreValue
        {
            get; set;
        }

        public class Handler : IRequestHandler<PostScoreEntryCommand>
        {
            private readonly ILeaderboardRepository leaderboardRepository;
            private readonly IScoreEntryRepository scoreEntryRepository;
            private readonly IMapper mapper;

            public Handler(ILeaderboardRepository leaderboardRepository,
                IScoreEntryRepository scoreEntryRepository,
                IMapper mapper)
            {
                this.leaderboardRepository = leaderboardRepository;
                this.scoreEntryRepository = scoreEntryRepository;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(PostScoreEntryCommand request, CancellationToken cancellationToken)
            {
                var leaderboard = await leaderboardRepository.GetWithAllScoreEntriesByIdAsync(request.LeaderboardId);

                if (leaderboard == null)
                    throw new NotFoundException(nameof(leaderboard), request.LeaderboardId);

                var scoreEntryEntity = await scoreEntryRepository.GetScoreEntryByUsernameAndLeaderboardId(leaderboard.Id, request.Username);

                if (scoreEntryEntity == null)
                {
                    var scoreEntry = mapper.Map<PostScoreEntryCommand, ScoreEntry>(request);

                    leaderboard.ScoreEntries.Add(scoreEntry);
                    await scoreEntryRepository.CreateAsync(scoreEntry);
                }
                else
                {
                    scoreEntryEntity.ScoreValue = request.ScoreValue;
                    await scoreEntryRepository.UpdateAsync(scoreEntryEntity);
                }

                return Unit.Value;
            }
        }
    }
}
