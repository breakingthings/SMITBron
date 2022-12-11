using FluentValidation;
using Paramore.Brighter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.Infrastructure
{
    public class RequestValidatorHandlerAsync<TRequest> : RequestHandlerAsync<TRequest> where TRequest : class, IRequest
    {
        private readonly IValidator<TRequest> _validator;

        public RequestValidatorHandlerAsync(IValidator<TRequest> validator)
        {
            this._validator = validator;
        }

        public override async Task<TRequest> HandleAsync(TRequest command, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(command, cancellationToken);
            return await base.HandleAsync(command, cancellationToken);
        }
    }
}
