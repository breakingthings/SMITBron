using FluentValidation;
using Paramore.Brighter;
using Paramore.Darker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.Infrastructure
{
    public class QueryValidatorHandlerAsync<TQuery, TResult> : QueryHandlerAsync<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        

        public QueryValidatorHandlerAsync()
        {
            //this._validator = validator;
        }


        public override async Task<TResult> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default)
        {
            //await _validator.ValidateAndThrowAsync(query, cancellationToken);
            return await this.ExecuteAsync(query, cancellationToken);
        }

    }
}
