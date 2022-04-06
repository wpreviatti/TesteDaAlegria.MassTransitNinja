using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDaAlegria.MassTransitNinja.Contracts.Order.Interfaces
{
    public interface OrderSubmissionAccepted
    {
        Guid OrderId { get; }

        DateTime TimesTamp { get; }
        string CustomerNumber { get; }
    }
}
