using FluentValidation;

namespace Application.Features.ScoreEntries.Queries.GetScoreEntries
{
    public class GetScoreEntriesValidator : AbstractValidator<GetScoreEntriesQuery>
    {
        public GetScoreEntriesValidator()
        {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("{Property} must be greater than or equal to 1");
        }
    }
}
