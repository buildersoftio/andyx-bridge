using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Andy.X.Bridge.Core.Configurations
{
    public class QueueConfiguration
    {
        public List<QueueEngine> Engines { get; set; }
    }

    public class QueueEngine
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public QueueEngineTypes Engine { get; set; }
        public string[] QueueBrokerUrls { get; set; }
        public List<QueueConfig> Queues { get; set; }
    }

    public enum QueueEngineTypes
    {
        RabbitMQ
    }

    public class QueueConfig
    {
        public string QueueName { get; set; }
        public TopicTo TopicTo { get; set; }
    }

    public class TopicTo
    {
        public string Component { get; set; }
        public string Topic { get; set; }
    }
}
