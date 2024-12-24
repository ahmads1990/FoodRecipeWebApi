using Autofac;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace FoodRecipeWebApi.Config.AutofacModules;

public class RedisCacheModule : Module
{
    private readonly string _redisConnectionString;

    public RedisCacheModule(string redisConnectionString)
    {
        _redisConnectionString = redisConnectionString;
    }
    protected override void Load(ContainerBuilder builder)
    {
        // Configure Redis Cache options
        builder.RegisterInstance(new RedisCacheOptions
        {
            Configuration = _redisConnectionString,
            InstanceName = "FoodApp"
        }).As<IOptions<RedisCacheOptions>>();

        // Register IDistributedCache as RedisCache
        builder.RegisterType<RedisCache>()
               .As<IDistributedCache>()
               .SingleInstance();
    }
}
