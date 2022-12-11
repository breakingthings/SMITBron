using FluentValidation;
using SMITBron.BookingService.Requests;
using SMITBron.Infrastructure;

namespace SMITBron.BookingService.Validators
{
    public class CancelBookingValidator : AbstractValidator<CancelBooking>
    {
        private readonly RWDbConnection _db;

        public CancelBookingValidator(RWDbConnection db)
        {
            this._db = db;

            
        }

        
    }

}
