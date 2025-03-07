using Api.Application.Bases;
using Api.Application.Interfaces.AutoMapper;
using Api.Application.Interfaces.UnitOfWorks;
using Api.Domain.Entities;
using Bogus;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Api.Application.Features.Brands.Commands.CreateBrand
{
    public class CereateBrandCommandHandler : BaseHandler, IRequestHandler<CreateBrandCommandRequest, Unit>
    {
        public CereateBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            Faker faker = new("tr");
            List<Brand> brands = new();

            for (int i = 0; i < 1000000; i++)
            {
                brands.Add(new (faker.Commerce.Department(1)));
            }
            await unitOfWork.GetWriteRepository<Brand>().AddRangeAsync(brands);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
