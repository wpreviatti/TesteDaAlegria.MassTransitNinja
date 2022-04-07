using MassTransit;
using MassTransit.Mediator ;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TesteDaAlegria.MassTransitNinja.Contracts.Order;
using TesteDaAlegria.MassTransitNinja.Contracts.Order.Interfaces;

namespace TesteDaAlegria.MassTransitNinja.Components.Consumers
{
    public class SubmitOrderConsumer :
        IConsumer<SubmitOrder>
    {
        /*
        private readonly ILogger<SubmitOrderConsumer> _logger;

        public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
        {
            _logger = logger;
        }*/
        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            //_logger.Log(LogLevel.Information, "SubmitOrderConsumer: {CustomerNumber}", context.Message.CustomerNumber);
            if (context.Message.CustomerNumber.Contains("TEST"))
            {
                await context.RespondAsync<OrderSubmissionRejected>(new
                {
                    OrderId = context.Message.OrderId,
                    TimesTamp = InVar.Timestamp,
                    CustomerNumber = context.Message.CustomerNumber,
                    Reason = $"TEste de cliente sumbmit fail ordem: {context.Message.CustomerNumber}"
                });
            }
            else
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
}
