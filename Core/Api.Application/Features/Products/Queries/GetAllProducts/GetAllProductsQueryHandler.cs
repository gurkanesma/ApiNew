using Api.Application.DTOs;
using Api.Application.Interfaces.AutoMapper;
using Api.Application.Interfaces.UnitOfWorks;
using Api.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork; //constructor acaılığıyla uow enjekte ediyoruz
            this.mapper = mapper;
        }

        public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await unitOfWork.GetReadRepository<Product>().GetAllAsync(include: x => x.Include(b => b.Brand)); //veritanından tüm veri çekiyoruz

           var brand= mapper.Map<BrandDto,Brand>(new Brand());

            var map = mapper.Map<GetAllProductsQueryResponse, Product>(products);
            foreach (var item in map)
                item.Price -= (item.Price * item.Discount / 100);

            return map;
            
        }
    }
}
