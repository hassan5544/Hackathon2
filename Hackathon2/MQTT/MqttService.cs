using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System.Text;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using Hackathon2.Data;
using Hackathon2.Models;
using Newtonsoft.Json;
using Hackathon2.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Hackathon2.MQTT
{
    public class MqttService : IHostedService
    {
        private readonly IMqttClient _mqttClient;
        private readonly IMqttClientOptions _mqttOptions;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<MessageHub> _hubContext;


        public MqttService(IServiceScopeFactory scopeFactory, IHubContext<MessageHub> hubContext)
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            _mqttOptions = new MqttClientOptionsBuilder()
                .WithClientId("AspNetCoreClient")
                .WithTcpServer("91.121.93.94", 1883) // Replace with your broker address and port
                .Build();

            _scopeFactory = scopeFactory;
            _hubContext = hubContext;

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _mqttClient.UseConnectedHandler(async e =>
            {
                Console.WriteLine("Connected to MQTT broker");
                await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("aswar").Build());
            });

            _mqttClient.UseApplicationMessageReceivedHandler(async e =>
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    try
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                        Console.WriteLine($"Received message: {message}");

                        var jsonPayload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                        jsonPayload = jsonPayload.Replace("Excellent", "\"Excellent\"");
                        var mqttMessage = JsonConvert.DeserializeObject<SensorData>(jsonPayload);

                        // Save the extracted properties to the database
                        var messageToSave = new Message
                        {
                            Topic = e.ApplicationMessage.Topic,
                            TempC = mqttMessage.TempC,
                            Humi = mqttMessage.Humi,
                            DsmConsentrate = mqttMessage.DsmConsentrate,
                            DsmParticle = mqttMessage.DsmParticle,
                            AirQualityLabel = mqttMessage.AirQualityLabel,
                            SensorValue = mqttMessage.SensorValue
                        };
                        
                        dbContext.Messages.Add(messageToSave);
                        await dbContext.SaveChangesAsync();

                        // Send SignalR notification
                        await _hubContext.Clients.All.SendAsync("ReceiveMessage", messageToSave);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    //{ "tempC": 26.9, "humi": 39.8, "dsm_consentrate": 650.21, "dsm_particle": 0.07, "air_quality_label": Excellent, "sensor_value": 39}

                }
            });

            await _mqttClient.ConnectAsync(_mqttOptions, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _mqttClient.DisconnectAsync();
        }
    }
}
