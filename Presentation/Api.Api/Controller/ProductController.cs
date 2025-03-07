using Api.Application.Features.Brands.Commands.CreateBrand;
using Api.Application.Features.Brands.Queries.GetAllBrands;
using Api.Application.Features.Products.Command.CreateProduct;
using Api.Application.Features.Products.Command.DeleteProduct;
using Api.Application.Features.Products.Command.UpdateProduct;
using Api.Application.Features.Products.Queries.GetAllProducts;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await mediator.Send(new GetAllProductsQueryRequest());

            if (response == null || !response.Any())
            {
                return NoContent(); // Eğer veri yoksa 204 No Content döndür
            }

            return Ok(response); // API yanıtında verileri döndür
        }

        [HttpPost]

        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {
            await mediator.Send(request);

            return Ok();
        }
        [HttpPost]

        public async Task<IActionResult> CreateBrand(CreateBrandCommandRequest request) //normalde burda brand oluşturmayız controller olmalı
                                                                                        //fakat test verisi ürettiğimiz için böyle yaptım.
        {
            await mediator.Send(request);

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> GetAllBrands()
        {
            var response = await mediator.Send(new GetAllBrandsQueryRequest());
            return Ok(response); // API yanıtında verileri döndür
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
