using System.Reflection;
using Dapr.Client;
using RecommendCoffee.Ratings.Application.Common;
using RecommendCoffee.Ratings.Domain.Common;

namespace RecommendCoffee.Ratings.Infrastructure.EventBus;

public class DaprEventPublisher : IEventPublisher
{
    private readonly DaprClient _daprClient;

    public DaprEventPublisher(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    public async Task PublishEventsAsync(IEnumerable<IDomainEvent> events)
    {
        foreach (var evt in events)
        {
            var topic = evt.GetType().GetCustomAttribute<TopicAttribute>();
            
            await _daprClient.PublishEventAsync<object>(
                "pubsub",
                topic?.Name ?? "ratings.deadletter.v1",
                evt);
        }
    }
}