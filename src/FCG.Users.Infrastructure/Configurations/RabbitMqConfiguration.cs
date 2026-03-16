using FCG.Users.Infrastructure.Messaging.Configurations;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Users.Infrastructure.Configurations;

public static class RabbitMqConfiguration
{
    extension(IServiceCollection services)
    {
        public void ConfigureMessaging(IConfiguration configuration)
        {
            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMQ"));

            var rabbit = configuration
                .GetSection("RabbitMQ")
                .Get<RabbitMqSettings>()!;

            services.AddMassTransit(x =>
            {
                x.AddConsumers(typeof(RabbitMqConfiguration).Assembly);
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.MessageTopology.SetEntityNameFormatter(new CustomNameEntityNameFormatter());
                    cfg.Host(
                        rabbit.Host,
                        rabbit.VirtualHost,
                        h =>
                        {
                            h.Username(rabbit.Username);
                            h.Password(rabbit.Password);
                        });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}