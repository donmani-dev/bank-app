
using BankTrackingSystem.Data;
using BankTrackingSystem.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BankTrackingSystem
{
    public class RedisMessageSubscriber : BackgroundService
    {
        private readonly ILogger<RedisMessageSubscriber> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private static readonly RedisChannel _channel = RedisChannel.Pattern("bank-account-status-changed-channel");

        public RedisMessageSubscriber(IConfiguration configuration, ILogger<RedisMessageSubscriber> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IConnectionMultiplexer connectionMultiplexer = EstablishRedisConnection();
            var subscriber = connectionMultiplexer.GetSubscriber();

            var scope = _serviceProvider.CreateAsyncScope();

            var applicantRepository = scope.ServiceProvider.GetRequiredService<IApplicantMessagesRespository>();

            try
            {
                // Subscribe to the channel
                await subscriber.SubscribeAsync(_channel, async (channel, message) =>
                {
                    var applicantMessage = JsonConvert.DeserializeObject<ApplicantMessagesModel>(message);
                    await applicantRepository.AddMessage(applicantMessage);
                    _logger.LogInformation("Received message: {Channel} {Message}", channel, message);
                });

                // Wait until cancellation is requested
                await Task.Delay(-1, stoppingToken);
            }
            finally
            {
                // Dispose of the scope when done
                scope.Dispose();
            }
        }

        private IConnectionMultiplexer EstablishRedisConnection()
        {
            // Reads the `ConnectionStrings` section from appsettings.json and stores it in an instance of `ConnectionStringsOptions`
            // Had to do this because otherwise a warning is emitted always saying `RedisConnectionString` may be null
            // Read more at https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-8.0
            ConnectionStringsOptions connectionStringsOptions = new ConnectionStringsOptions();
            _configuration.GetSection(ConnectionStringsOptions.ConnectionStrings).Bind(connectionStringsOptions);

            return ConnectionMultiplexer.Connect(new string(Environment.GetEnvironmentVariable("RedisConnectionString") ?? connectionStringsOptions.RedisConnectionString));
        }
    }
}
