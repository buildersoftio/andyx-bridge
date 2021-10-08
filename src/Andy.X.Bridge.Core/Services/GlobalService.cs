using Andy.X.Bridge.Core.Configurations;
using Andy.X.Bridge.Core.Utilities.Extensions.Json;
using Andy.X.Bridge.Core.Utilities.Logging;
using Andy.X.Bridge.IO.Locations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Andy.X.Bridge.Core.Services
{
    public class GlobalService
    {
        private AndyXConfiguration andyXConfiguration;
        private QueueConfiguration queueConfiguration;
        private bool isQueueConfigImported;

        private List<Thread> rabbitMQThreads;

        public GlobalService()
        {
            isQueueConfigImported = false;
            ImportConfigurationFiles();
            InitializeRabbitMQConsumerServices();
        }

        private void ImportConfigurationFiles()
        {
            Logger.LogInformation("Importing configuration files");
            if (Directory.Exists(AppLocations.ConfigDirectory()) != true)
            {
                Logger.LogError($"Importing configuration files failed, config directory does not exists; path={AppLocations.ConfigDirectory()}");
                Directory.CreateDirectory(AppLocations.ConfigDirectory());
                Logger.LogInformation($"Importing configuration files failed, config directory created; path={AppLocations.ConfigDirectory()}");
            }

            if (Directory.Exists(AppLocations.ServicesDirectory()) != true)
                Directory.CreateDirectory(AppLocations.ServicesDirectory());

            if (Directory.Exists(AppLocations.TemplatesDirectory()) != true)
                Directory.CreateDirectory(AppLocations.TemplatesDirectory());

            // checking if files exits
            if (File.Exists(AppLocations.GetAndyXConfigurationFile()) != true)
            {
                Logger.LogError($"Importing configuration files failed, andyx_config.json file does not exists; path={AppLocations.GetAndyXConfigurationFile()}");
                throw new Exception($"ANDYX-BRIDGE|[error]|importing|andyx_config.json|file not exists|path={AppLocations.GetAndyXConfigurationFile()}");
            }

            // checking if queue files exits
            if (File.Exists(AppLocations.GetQueueConfigurationFile()) == true)
            {
                try
                {
                    isQueueConfigImported = true;
                    queueConfiguration = File.ReadAllText(AppLocations.GetQueueConfigurationFile()).JsonToObject<QueueConfiguration>();
                    Logger.LogInformation($"Message Queue engines are imported successfully");
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Importing Queue engines file configuration is skipped, queues_config.json, please check the configuration file at path={AppLocations.GetQueueConfigurationFile()}; error details=", ex.Message);
                    isQueueConfigImported = false;
                }
            }
            else
            {
                Logger.LogWarning($"Importing Queue engines file configuration is skipped, queues_config.json file does not exists; path={AppLocations.GetQueueConfigurationFile()}");
            }

            try
            {
                andyXConfiguration = File.ReadAllText(AppLocations.GetAndyXConfigurationFile()).JsonToObject<AndyXConfiguration>();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Importing configuration files failed, andyx_config.json, please check the configuration file at path={AppLocations.GetAndyXConfigurationFile()}; error details=", ex.Message);
                throw new Exception("ANDYX-BRIDGE|[error]|importing|andyx_config.json|please check the configuration|path={AppLocations.GetAndyXConfigurationFile()}");
            }

            Logger.LogInformation($"Andy X configuration settings are imported successfully");
        }

        private void InitializeRabbitMQConsumerServices()
        {
            foreach (var queueEngine in queueConfiguration.Engines)
            {
                if (queueEngine.Engine == QueueEngineTypes.RabbitMQ)
                {
                    foreach (var configs in queueEngine.Queues)
                    {
                        var thread = new Thread(() =>
                        {

                            new RabbitMQ.RabbitMQConsumer(andyXConfiguration, queueEngine, configs)
                                .StartConsuming();

                            Logger.LogInformation($"RabbitMQ consumer of queue [{configs.QueueName}] has been initialized");
                        });

                        rabbitMQThreads.Add(thread);
                        thread.Start();
                    }
                }
            }
        }
    }
}
