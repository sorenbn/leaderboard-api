using FluentValidation;

namespace Application.Features.ScoreEntries.Commands.Post
{
    public class PostScoreEntryValidator : AbstractValidator<PostScoreEntryCommand>
    {
        public PostScoreEntryValidator(int? leaderboardMinRange, int? leaderboardMaxRange)
        {
            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            if (leaderboardMinRange.HasValue)
            {
                RuleFor(p => p.ScoreValue)
                    .GreaterThanOrEqualTo(leaderboardMinRange.Value)
                    .WithMessage("{PropertyName} must be greater than or equal to the accepted minimum range of the leaderboard");
            }

            if (leaderboardMaxRange.HasValue)
            {
                RuleFor(p => p.ScoreValue)
                    .LessThanOrEqualTo(leaderboardMaxRange.Value)
                    .WithMessage("{PropertyName} must be less than or equal to the accepted maximum range of the leaderboard");
            }
        }
    }
}
