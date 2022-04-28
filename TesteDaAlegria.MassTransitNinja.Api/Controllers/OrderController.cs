using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TesteDaAlegria.MassTransitNinja.Api.Model;
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
        private readonly IRequestClient<CheckOrder> _checkOrderClient;

        public OrderController(ILogger<OrderController> logger, 
            IRequestClient<SubmitOrder> requestClient,
            IRequestClient<CheckOrder> checkOrderClient)
        {
            _logger = logger;
            _requestClient = requestClient;
            _checkOrderClient = checkOrderClient;
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid guid)
        {
            var (status,notFound) = await _checkOrderClient.GetResponse<OrderStatus, OrderNotFound>(
                new
                {
                    OrderId = guid
                });
            if (status.IsCompletedSuccessfully)
            {
                var resposta = await status;
                return Ok(resposta);
            }
            else {
                var respostaRuim = await notFound;
                return NotFound(respostaRuim.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubmitOrderVo vo)
        {
            var (aceita, rejeitada) = await _requestClient.GetResponse<OrderSubmissionAccepted, OrderSubmissionRejected>(new
            {

                OrderId = vo.OrderId,
                TimesTamp = vo.TimesTamp,
                CustomerNumber = vo.CustomerNumber
            });
            if (aceita.IsCompletedSuccessfully)
            {
                var resposta = await aceita;
                return Ok(resposta);
            }
            else {
                var resposta = await rejeitada;
                return BadRequest(resposta);
            }
        }

    }
}
