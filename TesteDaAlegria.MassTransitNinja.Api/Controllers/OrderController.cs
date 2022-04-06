using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TesteDaAlegria.MassTransitNinja.Api.Model;
using TesteDaAlegria.MassTransitNinja.Components.Consumers;
using TesteDaAlegria.MassTransitNinja.Contracts.Order;
using TesteDaAlegria.MassTransitNinja.Contracts.Order.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TesteDaAlegria.MassTransitNinja.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        readonly ILogger<OrderController> _logger;
        readonly IRequestClient<SubmitOrder> _requestClient;

        public OrderController(ILogger<OrderController> logger, IRequestClient<SubmitOrder> requestClient)
        {
            _logger = logger;
            _requestClient = requestClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubmitOrderVo vo)
        {
            var resposta = await _requestClient.GetResponse<OrderSubmissionAccepted>(new
            {

                OrderId = vo.OrderId,
                TimesTamp = vo.TimesTamp,
                CustomerNumber = vo.CustomerNumber
            });

            return Ok(resposta.Message);
        }

    }
}
