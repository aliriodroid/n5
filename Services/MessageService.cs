using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using N5User.Data.Dtos;

namespace N5User.Services;

public class MessageService
{
    private readonly IConfiguration _configuration;

    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<MessageService> _logger;

    public MessageService(IConfiguration configuration, ILogger<MessageService> logger)
    {
        _configuration = configuration;
        _logger = logger;

        var producerconfig = new ProducerConfig
        {
            BootstrapServers = _configuration["Kafka:BootstrapServers"]
        };

        _producer = new ProducerBuilder<Null, string>(producerconfig).Build();
    }

    public async Task ProduceAsync(string topic, MessageDto message)
    {
        var msg = JsonSerializer.Serialize(message);
        var kafkamessage = new Message<Null, string> { Value = msg, };
        _logger.LogInformation(msg);
        await _producer.ProduceAsync(topic, kafkamessage);
    }
}