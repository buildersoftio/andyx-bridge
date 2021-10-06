using Andy.X.Bridge.Core.Abstractions.Services;
using Andy.X.Bridge.Core.Configurations;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace Andy.X.Bridge.Core.Services.RabbitMQ
{
    public class RabbitMQConsumer : IConsumer
    {
        private readonly AndyXConfiguration _andyXConfiguration;

        private readonly QueueEngine _queueEngine;
        private readonly QueueConfig _queueConfig;

        private IConnectionFactory factory;
        private IConnection connection;
        private IModel channel;
        private Thread consumingThread;


        public RabbitMQConsumer(AndyXConfiguration andyXConfiguration, QueueEngine queueEngine, QueueConfig queueConfig)
        {
            _andyXConfiguration = andyXConfiguration;
            _queueEngine = queueEngine;
            _queueConfig = queueConfig;

            InitializeConnection();
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
                //string message = Encoding.UTF8.GetString(e.Body);
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
