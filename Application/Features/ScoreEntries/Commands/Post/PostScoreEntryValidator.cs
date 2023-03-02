using Application.Contracts.Persistence;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ScoreEntries.Commands.Post
{
    public class PostScoreEntryValidator : AbstractValidator<PostScoreEntryCommand>
    {
        private readonly ILeaderboardRepository leaderboardRepository;

        public PostScoreEntryValidator(ILeaderboardRepository leaderboardRepository)
        {
            this.leaderboardRepository = leaderboardRepository;

            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p)
                .Cascade(CascadeMode.Stop)
                .MustAsync(LeaderboardExists).WithMessage("Leaderboard does not exist.")
                .MustAsync(InsideMinRange).WithMessage("Score must be greater than or equal to the accepted minimum range of the leaderboard")
                .MustAsync(InsideMaxRange).WithMessage("Score must be less than or equal to the accepted maximum range of the leaderboard");
        }

        private async Task<bool> InsideMinRange(PostScoreEntryCommand command, CancellationToken token)
        {
            var leaderboard = await leaderboardRepository.GetByIdAsync(command.LeaderboardId);
            return leaderboard.MinAcceptedValue == null || command.ScoreValue >= leaderboard.MinAcceptedValue;
        }

        private async Task<bool> InsideMaxRange(PostScoreEntryCommand command, CancellationToken token)
        {
            var leaderboard = await leaderboardRepository.GetByIdAsync(command.LeaderboardId);
            return leaderboard.MaximumAcceptedValue == null || command.ScoreValue <= leaderboard.MaximumAcceptedValue.Value;
        }

        private async Task<bool> LeaderboardExists(PostScoreEntryCommand command, CancellationToken token)
        {
            return await leaderboardRepository.GetByIdAsync(command.LeaderboardId) != null;
        }
    }
}
