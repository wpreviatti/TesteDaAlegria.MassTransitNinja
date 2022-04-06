using MassTransit;
using MassTransit.Mediator ;
using System.Threading.Tasks;
using TesteDaAlegria.MassTransitNinja.Contracts.Order;
using TesteDaAlegria.MassTransitNinja.Contracts.Order.Interfaces;

namespace TesteDaAlegria.MassTransitNinja.Components.Consumers
{
    public class SubmitOrderConsumer :
        IConsumer<SubmitOrder>
    {
        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            await context.RespondAsync<OrderSubmissionAccepted>(new
            {
                OrderId = context.Message.OrderId,
                TimesTamp = InVar.Timestamp,
                CustomerNumber = context.Message.CustomerNumber
            });
        }
    }
}
