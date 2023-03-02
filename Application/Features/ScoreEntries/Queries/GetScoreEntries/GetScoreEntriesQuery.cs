using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ScoreEntries.Queries.GetScoreEntries
{
    public class GetScoreEntriesQuery : IRequest<ScoreEntriesListViewModel>
    {
        public Guid LeaderboardId
        {
            get; set;
        }
        public int PageNumber
        {
            get; set;
        }
        public int PageSize
        {
            get; set;
        }

        public class Handler : IRequestHandler<GetScoreEntriesQuery, ScoreEntriesListViewModel>
        {
            private readonly IMapper mapper;
            private readonly IScoreEntryRepository scoreEntryRepository;

            public Handler(IMapper mapper, IScoreEntryRepository scoreEntryRepository)
            {
                this.mapper = mapper;
                this.scoreEntryRepository = scoreEntryRepository;
            }

            public async Task<ScoreEntriesListViewModel> Handle(GetScoreEntriesQuery request, CancellationToken cancellationToken)
            {
                var scoreEntries = await scoreEntryRepository.GetPaginatedScoreEntries(request.LeaderboardId, request.PageNumber, request.PageSize);
                var mappedScoreEntries = mapper.Map<IEnumerable<ScoreEntry>, ScoreEntriesListViewModel>(scoreEntries);

                var startRank = (request.PageNumber - 1) * request.PageSize;

                for (int i = 0; i < request.PageSize; i++)
                    mappedScoreEntries.ScoreEntries[i].Rank = startRank + (i + 1);

                return mappedScoreEntries;
            }
        }
    }
}
