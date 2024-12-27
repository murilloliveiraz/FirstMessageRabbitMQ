using ConsumerAPI.BusConsumer;
using MassTransit;

namespace ConsumerAPI.Extensions
{
    public static class AppExtensions
    {
        public static void AddRabbitMQService(this IServiceCollection services)
        {
            services.AddTransient<IPublisherBus, PublisherBus>();

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumer<RequestedReportEventConsumer>();

                busConfigurator.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri("amqp://localhost:5672"), host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });
                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }
    }
}
