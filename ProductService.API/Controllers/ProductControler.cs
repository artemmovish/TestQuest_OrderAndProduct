using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Commands;
using ProductService.Application.DTOs.Response;
using ProductService.Application.DTOs.Response.Base;
using ProductService.Application.Queries;

namespace ProductService.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommandResponse<ProductResponse>>> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetProductQuery { Id = id });

            if (!response.Success || response.Data == null)
            {
                return NotFound(new CommandResponse<ProductResponse>(false, "Продукт не найден", null));
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] CreateProductCommand command)
        {
            var response = await _mediator.Send(command);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
        }


        [HttpGet("{id}/check-stock")]
        public async Task<ActionResult<CommandResponse<CheckStockQuantityResponse>>> CheckStockQuantity(Guid id, [FromQuery] int quantity)
        {
            CommandResponse<CheckStockQuantityResponse>? response = await _mediator.Send(new CheckStockQuantityQuery { Id = id, StockQuantity = quantity });

            if (!response.Success || response.Data == null)
            {
                return NotFound(new CommandResponse<CheckStockQuantityResponse>(false, "Продукт не найден", null));
            }

            return Ok(response);
        }

        [HttpPost("add-to-stock")]
        public async Task<ActionResult<CommandResponse<StockQuantityResponse>>> AddToStock([FromBody] RemoveFromStockCommand command)
        {
            var response = await _mediator.Send(command);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPost("remove-from-stock")]
        public async Task<ActionResult<CommandResponse<StockQuantityResponse>>> RemoveFromStock([FromBody] RemoveFromStockCommand command)
        {
            var response = await _mediator.Send(command);

            // Если операция неуспешна
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

    }
}
