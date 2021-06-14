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
                GetScoreEntriesValidator validator = new();
                var result = validator.Validate(request);

                if (result.Errors.Count > 0)
                    throw new ValidationException(result);

                var scoreEntries = await scoreEntryRepository.GetPaginatedScoreEntries(request.LeaderboardId, request.PageNumber, request.PageSize);
                return mapper.Map<IEnumerable<ScoreEntry>, ScoreEntriesListViewModel>(scoreEntries);
            }
        }
    }
}
