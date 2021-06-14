using Application.Exceptions;
using Application.Features.ScoreEntries.Commands.Post;
using Application.Features.ScoreEntries.Queries.GetScoreEntries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class ScoreEntriesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ScoreEntriesListViewModel>> GetPaginatedScoreEntries([FromQuery] GetScoreEntriesQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValdationErrors);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> PostScoreEntry([FromBody] PostScoreEntryCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return NoContent();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValdationErrors);
            }
        }
    }
}
