using Application.Features.Leaderboards.Commands.Create;
using Application.Features.Leaderboards.Queries.GetLeaderboard;
using Application.Features.Leaderboards.Queries.GetLeaderboardList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class LeaderboardsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<LeaderboardViewModel>> GetLeaderboardById(Guid id)
        {
            var result = await Mediator.Send(new GetLeaderboardQuery
            {
                Id = id
            });

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<LeaderboardListViewModel>> GetLeaderboardList()
        {
            var result = await Mediator.Send(new GetLeaderboardListQuery());
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> CreateLeaderboard([FromBody] CreateLeaderboardCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }
    }
}
