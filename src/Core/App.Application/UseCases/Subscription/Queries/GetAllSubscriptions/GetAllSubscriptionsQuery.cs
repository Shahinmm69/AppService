namespace App.Application.UseCases.Subscription.Queries.GetAllSubscriptions
{
    public record GetAllSubscriptionsQuery(byte[]? LastRowVersion) : IRequest<IEnumerable<Domain.Entities.Subscription>>;
}
