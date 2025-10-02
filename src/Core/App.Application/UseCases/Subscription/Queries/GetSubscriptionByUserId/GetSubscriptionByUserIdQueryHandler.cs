namespace App.Application.UseCases.Subscription.Queries.GetSubscriptionByUserId
{
    public class GetSubscriptionByUserIdQueryHandler : IRequestHandler<GetSubscriptionByUserIdQuery, Domain.Entities.Subscription?>
    {
        private readonly ISubscriptionProvider _provider;

        public GetSubscriptionByUserIdQueryHandler(ISubscriptionProvider provider)
        {
            _provider = provider;
        }

        public async Task<Domain.Entities.Subscription?> Handle(GetSubscriptionByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _provider.GetByUserIdAsync(request.UserId, cancellationToken);
        }
    }
}
