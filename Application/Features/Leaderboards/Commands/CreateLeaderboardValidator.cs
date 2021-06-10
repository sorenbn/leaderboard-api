using FluentValidation;

namespace Application.Features.Leaderboards.Commands
{
    public class CreateLeaderboardValidator : AbstractValidator<CreateLeaderboardCommand>
    {
        public CreateLeaderboardValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
