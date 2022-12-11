using Paramore.Darker;
using SMITBron.HotelService.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.HotelService.Requests
{
    public class GetFreeApartments : IQuery<IEnumerable<HotelApartmentResult>>
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public GetFreeApartments(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }
}
