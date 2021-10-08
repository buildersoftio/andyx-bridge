using Andy.X.Bridge.Core.Abstractions.Services;
using Andy.X.Bridge.Core.Configurations;
using Andy.X.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace Andy.X.Bridge.Core.Services.RabbitMQ
{
    public class RabbitMQConsumer : IConsumer
    {
        // Configurations
        private readonly AndyXConfiguration _andyXConfiguration;
        private readonly QueueEngine _queueEngine;
        private readonly QueueConfig _queueConfig;

        // RabbitMQ
        private IConnectionFactory factory;
        private IConnection connection;
        private IModel channel;

        private Thread consumingThread;

        // Andy X Producer
        private Producer<Object> andyXProducer;

        public RabbitMQConsumer(AndyXConfiguration andyXConfiguration, QueueEngine queueEngine, QueueConfig queueConfig)
        {
            _andyXConfiguration = andyXConfiguration;
            _queueEngine = queueEngine;
            _queueConfig = queueConfig;

            InitializeAndyXProducer();
            InitializeConnection();
        }

        private void InitializeAndyXProducer()
        {
            XClient xClient = new XClient(new Client.Configurations.XClientConfiguration()
            {
                XNodeUrl = _andyXConfiguration.BrokerServiceUrls[0],
                Tenant = _andyXConfiguration.Tenant,
                Product = _andyXConfiguration.Product
            });

            andyXProducer = new Producer<Object>(xClient, new Client.Configurations.ProducerConfiguration()
            {
                Component = _queueConfig.TopicTo.Component,
                Name = $"rabbit-mq-{_queueConfig.QueueName}",
                RetryProducing = false,
                Topic = $"{_queueConfig.TopicTo.Topic}"
            });

            andyXProducer.BuildAsync().Wait();
        }

        private void InitializeConnection()
        {
            factory = new ConnectionFactory() { HostName = _queueEngine.Hostname, UserName = _queueEngine.Username, Password = _queueEngine.Password, Port = _queueEngine.Port };
            connection = factory.CreateConnection();
            StartConsuming();
        }

        public void StartConsuming()
        {
            consumingThread = new Thread(() =>
            {
                ConsumerWorker(_queueConfig);
            });
            consumingThread.Start();
        }

        private void ConsumerWorker(QueueConfig queueConfig)
        {
            channel.QueueDeclare(queueConfig.QueueName, queueConfig.Settings.Durable, queueConfig.Settings.Exclusive, queueConfig.Settings.AutoDelete, queueConfig.Settings.Arguments);
            channel.BasicQos((uint)queueConfig.Settings.PrefetchSize, (ushort)queueConfig.Settings.PrefetchCount, queueConfig.Settings.IsGlobal);

            var actualChannel = channel;
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, e) =>
            {

                var body = e.Body;
                string message = Encoding.UTF8.GetString(body.ToArray());
                actualChannel.BasicAck(e.DeliveryTag, false);
            };

            channel.BasicConsume(queueConfig.QueueName, false, consumer);
        }

        public void StopConsuming()
        {
            throw new NotImplementedException();
        }
    }
}
