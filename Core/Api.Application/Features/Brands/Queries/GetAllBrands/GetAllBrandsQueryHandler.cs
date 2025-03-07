using Api.Application.Bases;
using Api.Application.Interfaces.AutoMapper;
using Api.Application.Interfaces.UnitOfWorks;
using Api.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Api.Application.Features.Brands.Queries.GetAllBrands
{
    public class GetAllBrandsQueryHandler : BaseHandler,IRequestHandler<GetAllBrandsQueryRequest,IList<GetAllBrandsQueryResponse>>
    {
        public GetAllBrandsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }


        public async Task<IList<GetAllBrandsQueryResponse>> Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken)
        { 
            var brands = await unitOfWork.GetReadRepository<Brand>().GetAllAsync();

            return mapper.Map<GetAllBrandsQueryResponse,Brand>(brands);
        }

        
    }
}
