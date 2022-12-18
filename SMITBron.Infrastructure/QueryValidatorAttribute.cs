using Paramore.Brighter;
using Paramore.Darker.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.Infrastructure
{
    public class QueryValidatorAttribute : QueryHandlerAttribute
    {
        public QueryValidatorAttribute(int step) : base(step)
        { 
        }

        public override Type GetDecoratorType()
        {
            return typeof(QueryValidatorHandlerAsync<,>);
        }

        public override object[] GetAttributeParams()
        {
            return new object[0];
        }

    }
}
