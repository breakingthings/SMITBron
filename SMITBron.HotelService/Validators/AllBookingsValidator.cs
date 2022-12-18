using FluentValidation;
using SMITBron.HotelService.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.HotelService.Validators
{
    public class AllBookingsValidator : AbstractValidator<GetAllBookings>
    {
        public AllBookingsValidator() 
        {
            RuleFor(x => x.PageSize).LessThanOrEqualTo(100);
        
        }
    }
}
