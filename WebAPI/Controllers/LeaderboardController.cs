using Application.Features.Leaderboards.Commands.Create;
using Application.Features.Leaderboards.Queries.GetLeaderboard;
using Application.Features.Leaderboards.Queries.GetLeaderboardList;
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

        [HttpGet(nameof(GetLeaderboardById))]
        public async Task<ActionResult<LeaderboardViewModel>> GetLeaderboardById([FromQuery] Guid id)
        {
            var viewModel = await mediator.Send(new GetLeaderboardQuery
            {
                Id = id
            });

            return Ok(viewModel);
        }

        [HttpGet(nameof(GetLeaderboardList))]
        public async Task<ActionResult<LeaderboardListViewModel>> GetLeaderboardList()
        {
            var viewmodel = await mediator.Send(new GetLeaderboardListQuery());
            return Ok(viewmodel);
        }

        [HttpPost(nameof(CreateLeaderboard))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> CreateLeaderboard([FromBody] CreateLeaderboardCommand requestDto)
        {
            var id = await mediator.Send(requestDto);
            return Ok(id);
        }
    }
}
