using System;

namespace TesteDaAlegria.MassTransitNinja.Contracts.Order
{
    public interface SubmitOrder
    {
        public Guid OrderId { get; set; }
        public DateTime TimesTamp { get; set; }
        public string CustomerNumber { get; set; }
    }
}
