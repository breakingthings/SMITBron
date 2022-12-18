using Paramore.Brighter;
using Paramore.Darker;
using SMITBron.HotelService.Responses;
using SMITBron.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.HotelService.Requests
{
    public class GetAllBookings : IQuery<AllBookingsResult>
    {
        public int PageNumber { get; }
        
        public int PageSize { get; }

        public bool ShowCanceled { get; }
        
        public string SortField { get; }
        
        public bool SortDesc { get; }

        
        public GetAllBookings(int pageNumber, int pageSize, bool showCanceled, string sortField, bool sortDesc)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            ShowCanceled = showCanceled;
            SortField = sortField;
            SortDesc = sortDesc;
        }



    }
}
