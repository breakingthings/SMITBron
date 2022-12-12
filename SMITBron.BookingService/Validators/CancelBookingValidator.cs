using FluentValidation;
using FluentValidation.Results;
using LinqToDB;
using SMITBron.BookingService.Requests;
using SMITBron.Core.Entities;
using SMITBron.Infrastructure;

namespace SMITBron.BookingService.Validators
{
    public class CancelBookingValidator : AbstractValidator<CancelBooking>
    {
        private readonly RWDbConnection _db;

        public CancelBookingValidator(RWDbConnection db)
        {
            this._db = db;
            this.RuleFor(x => x).MustAsync(async (command, cancellation) =>
            {
                return await _db.GetTable<Booking>().Where(x => x.Id == command.BookingId 
                    && x.StartDate > DateTime.Now.AddDays(3)
                    && x.Guest.Email == command.Email
                    && x.Guest.IdCode == command.IdCode 
                    && x.CancelDate == null).FirstOrDefaultAsync() != null;
            }).WithMessage("Booking not found or cancellation time over");
        }

    }

}
