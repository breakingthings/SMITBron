using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;
using Paramore.Darker;
using SMITBron.HotelService.Requests;
using SMITBron.HotelService.Responses;
using SMITBron.Web.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMITBron.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : APIBaseController
    {
        public HotelController(IAmACommandProcessor commandProcessor, IQueryProcessor queryProcessor)
            : base(commandProcessor, queryProcessor)
        {

        }

        [HttpGet]
        public async Task<ActionResult<AllBookingsResult>> GetAllBookings([FromQuery]int page, 
            [FromQuery]int pageSize, [FromQuery]bool showCanceled, [FromQuery]string sortField, [FromQuery]bool sortDesc)
        {
            return await DoQueryAsync(new GetAllBookings(page, pageSize, showCanceled, sortField, sortDesc));
        }

    }
}
