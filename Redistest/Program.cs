using System;
using StackExchange.Redis;

namespace Redistest
{
    class Program
    {
        static void Main(string[] args)
        {
            string redisServer = Environment.GetEnvironmentVariable("REDIS_HOST");
            string password = Environment.GetEnvironmentVariable("REDIS_AUTH");;
            string connection = $"{redisServer},ssl=true,password={password}";
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connection);
            IDatabase db = redis.GetDatabase();
            string value = "12345678";
            db.StringSet("mykey", value);
            Console.WriteLine($"Value stored in Redis: {value}");
            value = db.StringGet("mykey");
            Console.WriteLine($"Value fetched from Redis: {value}");
        }
    }
}
