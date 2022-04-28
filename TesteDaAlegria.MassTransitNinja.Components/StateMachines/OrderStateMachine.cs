namespace Company.StateMachines
{
    using MassTransit;
    using System;
    using TesteDaAlegria.MassTransitNinja.Contracts.Order.Events;
    using TesteDaAlegria.MassTransitNinja.Contracts.Order.Interfaces;

    public class OrderStateMachine :
        MassTransitStateMachine<OrderState> 
    {
        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderSubmittedEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderCheckEvent, x => {
                x.CorrelateById(context => context.Message.OrderId);
                x.OnMissingInstance(m => m.ExecuteAsync(async contextOnMissing =>
                      {
                          if (contextOnMissing.RequestId.HasValue)
                          {
                              await contextOnMissing.RespondAsync<OrderNotFound>(new
                              {
                                  contextOnMissing.Message.OrderId
                              });
                          }
                      }));
                });

            Initially(
                When(OrderSubmittedEvent)
                    .Then(context => Console.WriteLine("inicioooooooooooooooooooooooooooooooooooooooooooooooo hahsfshsfushfu"))
                    .Then(context => {
                        context.Instance.SubmitDate = context.Data.TimeStamp;
                        context.Instance.CustomerNumber = context.Data.CustomerNumber;
                        context.Instance.Updated = DateTime.UtcNow;
                        })
                    .TransitionTo(Submitted)
            );
            During(Submitted, Ignore(OrderSubmittedEvent));
            DuringAny(
                When(OrderSubmittedEvent)
                .Then(context => Console.WriteLine("teste hahsfshsfushfu"))
                    .Then(context =>
                    {
                        context.Instance.SubmitDate = context.Data.TimeStamp;
                        context.Instance.CustomerNumber ??= context.Data.CustomerNumber;
                    })
                    );
            DuringAny(
                When(OrderCheckEvent)
                    .RespondAsync(x => x.Init<OrderStatus>(new
                    {
                        OrderId = x.Instance.CorrelationId,
                        State = x.Instance.CurrentState,
                    })
                )
            );

            //SetCompletedWhenFinalized();
        }

        public State Created { get; private set; }
        public State Submitted { get; private set; }

        public Event<OrderSubmitted> OrderSubmittedEvent { get; private set; }
        public Event<CheckOrder> OrderCheckEvent { get; private set; }
    }
}