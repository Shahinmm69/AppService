namespace App.Application.UseCases.Subscription.Queries.GetSubscriptionByUserId
{
    public record GetSubscriptionByUserIdQuery(int UserId) : IRequest<Domain.Entities.Subscription?>;
}
