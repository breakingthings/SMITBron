using Paramore.Brighter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.Infrastructure
{
    public class RequestValidatorAttribute : RequestHandlerAttribute
    {
        public RequestValidatorAttribute(int step, HandlerTiming timing) : base(step, timing)
        { 
        }

        public override Type GetHandlerType()
        {
            return typeof(RequestValidatorHandlerAsync<>);
        }

        public override object[] InitializerParams()
        {
            return base.InitializerParams();
        }
    }
}
