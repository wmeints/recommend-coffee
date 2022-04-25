using System.Reflection;
using Dapr.Client;
using Microsoft.Extensions.Logging;
using RecommendCoffee.Recommendations.Application.Common;
using RecommendCoffee.Recommendations.Domain.Common;

namespace RecommendCoffee.Recommendations.Infrastructure.EventBus;

public class DaprEventPublisher : IEventPublisher
{
    private const string DEADLETTER_TOPIC = "recommendations.deadletter.v1";
    private readonly ILogger<DaprEventPublisher> _logger;

    private readonly DaprClient _daprClient;

    public DaprEventPublisher(DaprClient daprClient, ILogger<DaprEventPublisher> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    public async Task PublishEventsAsync(IEnumerable<IDomainEvent> events)
    {
        foreach (var evt in events)
        {
            var topic = evt.GetType().GetCustomAttribute<TopicAttribute>();

            if (topic == null)
            {
                _logger.MissingTopicAttribute();
            }
            
            using (var activity = Activities.PublishEvent(topic?.Name ?? DEADLETTER_TOPIC))
            {
                await _daprClient.PublishEventAsync<object>(
                    "pubsub",
                    topic?.Name ?? DEADLETTER_TOPIC,
                    evt);
            }

            Metrics.EventsPublished.Add(1);
        }
    }
}