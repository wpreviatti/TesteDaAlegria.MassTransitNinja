using TesteDaAlegria.MassTransitNinja.Contracts.Order;

namespace TesteDaAlegria.MassTransitNinja.Api.Model
{
    public class SubmitOrderVo : SubmitOrder
    {
        public Guid OrderId { get; set; }
        public DateTime TimesTamp { get ; set ; }
        public string CustomerNumber { get ; set ; }
    }
}
