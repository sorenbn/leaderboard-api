using Application.Features.Leaderboards.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaderboardCommand, Leaderboard>();
        }
    }
}
