﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter.Extensions.DependencyInjection;
using SMITBron.Web.Models;
using System.Collections.Generic;
using Paramore.Brighter;
using SMITBron.BookingService.Requests;
using System.Threading.Tasks;
using NLog.Common;
using SMITBron.Web.Helpers;
using Paramore.Darker;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMITBron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : APIBaseController
    {

        public BookingController(IAmACommandProcessor commandProcessor, 
            IQueryProcessor queryProcessor) : base(commandProcessor, queryProcessor)
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewBookingModel model)
        {
            return await base.SendCommandAsync(new NewBooking(
                apartmentId: model.ApartmentId,                 
                startDate: model.StartDate,
                endDate: model.EndDate, 
                email: model.Email,
                idCode: model.IdCode, 
                firstname: model.Firstname, 
                lastname: model.Lastname));

            ;
        }

        
    }
}