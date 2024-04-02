using Confluent.Kafka;

namespace N5User.Services;

public class MessageReceiver:BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;

    private readonly ILogger<MessageReceiver> _logger;

    public MessageReceiver(IConfiguration configuration, ILogger<MessageReceiver> logger)
    {
        _logger = logger;

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = "PermissionsGroup",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
    }

    protected override  Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("GetPermissions");

        while (!stoppingToken.IsCancellationRequested)
        {
            ProcessKafkaMessage(stoppingToken);

            Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }

        _consumer.Close();

        return Task.CompletedTask;
    }

    public void ProcessKafkaMessage(CancellationToken stoppingToken)
    {
        try
        {
            var consumeResult = _consumer.Consume(stoppingToken);

            var message = consumeResult.Message.Value;

            _logger.LogInformation($"Message: {message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error processing Kafka message: {ex.Message}");
        }
    }
    
}