using FluentValidation;

namespace Application.Features.Leaderboards.Queries.GetLeaderboard
{
    public class GetLeaderboardQeuryValidator : AbstractValidator<GetLeaderboardQuery>
    {
        public GetLeaderboardQeuryValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("{PropertName} must not be an empty GUID");
        }
    }
}
