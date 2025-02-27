using Api.Application.Features.Products.Command.CreateProduct;
using Api.Application.Features.Products.Command.DeleteProduct;
using Api.Application.Features.Products.Command.UpdateProduct;
using Api.Application.Features.Products.Queries.GetAllProducts;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Api.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllProducts()
        {
            var response = await mediator.Send(new GetAllProductsQueryRequest());

            return Ok();
        }

        [HttpPost]

        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {
            await mediator.Send(request);

            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
        {
            await mediator.Send(request);

            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommandRequest request)
        {
            await mediator.Send(request);

            return Ok();
        }
    }
}
