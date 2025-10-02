using App.Application.UseCases.Subscription.Queries.GetAllSubscriptions;
using App.Application.UseCases.Subscription.Queries.GetSubscriptionByUserId;

namespace App.Presentation.Controllers
{
    [ApiController]
    [Route("api/subscriptions")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("by-user-id")]
        public async Task<IActionResult> GetByUserId([FromQuery] GetSubscriptionByUserIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            if (result == null)
                return NotFound(new { Message = "Subscription not found" });

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSubscriptionsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}