using Application.Features.Leaderboards.Commands.Create;
using Application.Features.Leaderboards.Queries.GetLeaderboard;
using Application.Features.Leaderboards.Queries.GetLeaderboardList;
using Application.Features.ScoreEntries.Commands.Post;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Leaderboard
            CreateMap<CreateLeaderboardCommand, Leaderboard>();
            CreateMap<Leaderboard, LeaderboardViewModel>();
            CreateMap<IEnumerable<Leaderboard>, LeaderboardListViewModel>()
                .ForMember(dest => dest.Leaderboards, options =>
                options.MapFrom(src => src));

            //ScoreEntries
            CreateMap<PostScoreEntryCommand, ScoreEntry>();
        }
    }
}
