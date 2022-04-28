using System;

namespace TesteDaAlegria.MassTransitNinja.Contracts.Order.Interfaces
{
    public interface OrderStatus
    {
        Guid OrderID { get; set; }
        string State { get; set; }
    }
}
