using Application.Features.Leaderboards.Commands;
using Application.Features.Leaderboards.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaderboardCommand, Leaderboard>();
            CreateMap<Leaderboard, LeaderboardViewModel>();
        }
    }
}
