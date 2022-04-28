using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDaAlegria.MassTransitNinja.Contracts.Order.Events
{
    public interface OrderSubmitted
    {
        public Guid OrderId { get; }
        public DateTime TimeStamp { get; }
        public string CustomerNumber { get; }
    }
}
