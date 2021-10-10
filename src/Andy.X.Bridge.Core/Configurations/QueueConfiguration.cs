using System;
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


        // we use ServiceUrl for Kafka, Pulsar and Andy X
        public string ServiceUrl { get; set; }
        public string Hostname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public List<QueueConfig> Queues { get; set; }
    }

    public enum QueueEngineTypes
    {
        RabbitMQ,
        Kafka,
        Pulsar,
        AndyX
    }

    public class QueueConfig
    {
        public string QueueName { get; set; }
        public QueueSettings Settings { get; set; }
        public TopicTo TopicTo { get; set; }
    }

    public class QueueSettings
    {
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public Dictionary<string, Object> Arguments { get; set; }

        public int PrefetchSize { get; set; }
        public int PrefetchCount { get; set; }
        public bool IsGlobal { get; set; }

        public QueueSettings()
        {
            Durable = true;
            Exclusive = false;
            AutoDelete = false;
            Arguments = null;
        }
    }

    public class TopicTo
    {
        public string Component { get; set; }
        public string Topic { get; set; }
    }
}
