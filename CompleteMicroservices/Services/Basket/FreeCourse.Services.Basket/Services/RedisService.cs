using StackExchange.Redis;

namespace FreeCourse.Services.Basket.Services
{
    public class RedisService
    {
        private readonly string Host;
        private readonly int Port ;
        public RedisService(string host, int port)
        {
            Host = host;
            Port = port;
        }

        private ConnectionMultiplexer _ConnectionMultiplexer;
        public void Connect() => _ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{Host}:{Port}");
        // redis hazır 15 tane db geliyor
        public IDatabase GetDatabase(int db = 1) => _ConnectionMultiplexer.GetDatabase(db);
    }
}
