using FluentValidation;

namespace Application.Features.ScoreEntries.Queries.GetScoreEntries
{
    public class GetScoreEntriesValidator : AbstractValidator<GetScoreEntriesQuery>
    {
        public GetScoreEntriesValidator()
        {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("{PropertyName} must be greater than or equal to 1");

            RuleFor(p => p.PageSize)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0");
        }
    }
}
