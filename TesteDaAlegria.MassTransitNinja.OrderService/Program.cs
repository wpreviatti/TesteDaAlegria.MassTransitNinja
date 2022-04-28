using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using TesteDaAlegria.MassTransitNinja.Components.Consumers;
using Company.StateMachines;

namespace TesteDaAlegria.MassTransitNinja.OrderService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(cfg =>
                    {
                        //cfg.AddConsumersFromNamespaceContaining<SubmitOrderConsumer>();
                        cfg.AddSagaStateMachine<OrderStateMachine, OrderState>().RedisRepository();

                        var entryAssembly = Assembly.GetEntryAssembly();

                        //cfg.AddConsumers(entryAssembly);
                        cfg.UsingRabbitMq(ConfigureBus);

                    });
                });
        //Wellington -> A ideia aqui é poder configurar manualmente a politica de resiliencia futuramente
        static void ConfigureBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator)
        {
            //configurator.UseMessageData(new MongoDbMessageDataRepository("mongodb://127.0.0.1", "attachments"));
            //configurator.UseMessageScheduler(new Uri("queue:quartz"));
            configurator.ReceiveEndpoint(KebabCaseEndpointNameFormatter.Instance.Consumer<SubmitOrderConsumer>(), e =>
            {
                //e.PrefetchCount = 20;

                /*e.Batch<RoutingSlipCompleted>(b =>
                {
                    b.MessageLimit = 10;
                    b.TimeLimit = TimeSpan.FromSeconds(5);

                    b.Consumer<RoutingSlipBatchEventConsumer, RoutingSlipCompleted>(context);
                });*/
                e.Consumer<SubmitOrderConsumer>();
            });
            
            configurator.ConfigureEndpoints(context);
        }
    }
}
