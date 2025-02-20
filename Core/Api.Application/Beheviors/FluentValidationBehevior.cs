using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Application.Behaviors
{
    public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validator;

        public FluentValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
        {
            this.validator = validator;
        }
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failtures = validator
                .Select(v => v.Validate(context))
                .SelectMany(v => v.Errors)
                .GroupBy(x => x.ErrorMessage)
                .Select(x => x.First())
                .Where(f => f != null)
                .ToList();
            //request nesnesiyle birlikte gelen contexti ValidationContext ile beraber yakalıyoruz.
            //request nesnesini doğrulayan tüm validatörleri çalıştırır,
            //hata mesajlarını gruplayarak tekrar edenleri filtreleyip ve yalnızca benzersiz hata mesajlarını içeren bir liste oluşturuyoruz.

            if (failtures.Any())
            {
                throw new ValidationException(failtures);
            }
            return next();
            //Eğer doğrulama hataları (failtures) varsa, bir ValidationException fırlatılır;
            //aksi takdirde, isteğin işlenmesine devam edilir (next() çağrılır).
        }
    }
}
