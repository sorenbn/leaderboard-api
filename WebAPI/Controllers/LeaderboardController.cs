using Application.Features.Leaderboards.Commands.Create;
using Application.Features.Leaderboards.Queries.GetLeaderboard;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaderboardController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetLeaderboard")]
        public async Task<ActionResult<LeaderboardViewModel>> GetLeaderboardById([FromQuery] Guid id, [FromQuery] bool includeScoreEntries)
        {
            var viewModel = await mediator.Send(new GetLeaderboardQuery
            {
                Id = id,
                IncludeScoreEntries = includeScoreEntries
            });

            return Ok(viewModel);
        }

        [HttpPost(Name = "CreateLeaderboard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateLeaderboardCommand requestDto)
        {
            var id = await mediator.Send(requestDto);
            return Ok(id);
        }
    }
}
