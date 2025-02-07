using Business.Features.Albums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AlbumsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Create.Command command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
