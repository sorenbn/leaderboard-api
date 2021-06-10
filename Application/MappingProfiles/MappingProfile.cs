using Application.Features.Leaderboards.Commands.Create;
using Application.Features.Leaderboards.Queries.GetLeaderboard;
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
