using Application.Exceptions;
using Application.Features.ScoreEntries.Commands.Post;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreEntryController : ControllerBase
    {
        private readonly IMediator mediator;

        public ScoreEntryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(nameof(PostScoreEntry))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Guid>> PostScoreEntry([FromBody] PostScoreEntryCommand requestDto)
        {
            try
            {
                await mediator.Send(requestDto);
                return NoContent();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValdationErrors);
            }
        }
    }
}
