using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Paramore.Brighter;
using System.Threading.Tasks;
using System;
using Paramore.Darker;
using SMITBron.HotelService.Responses;
using SMITBron.HotelService.Requests;
using SMITBron.Web.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMITBron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : APIBaseController
    {
        

        public ApartmentsController(IAmACommandProcessor commandProcessor, IQueryProcessor queryProcessor)
            :base(commandProcessor, queryProcessor)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelApartmentResult>>> Get([FromQuery]DateTime from, [FromQuery]DateTime to)
        {
            var result = await _queryProcessor.ExecuteAsync(new GetFreeApartments(from, to));

            return Ok(result);
        }

    }
}
