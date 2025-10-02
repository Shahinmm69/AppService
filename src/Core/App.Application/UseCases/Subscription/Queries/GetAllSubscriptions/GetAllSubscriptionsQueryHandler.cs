namespace App.Application.UseCases.Subscription.Queries.GetAllSubscriptions
{

    public class GetAllSubscriptionsQueryHandler : IRequestHandler<GetAllSubscriptionsQuery, IEnumerable<Domain.Entities.Subscription>>
    {
        private readonly ISubscriptionProvider _provider;

        public GetAllSubscriptionsQueryHandler(ISubscriptionProvider provider)
        {
            _provider = provider;
        }

        public async Task<IEnumerable<Domain.Entities.Subscription>> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var lastRowVersion = request.LastRowVersion ?? new byte[8];
            return await _provider.GetAllAsync(lastRowVersion, cancellationToken);
        }
    }
}
