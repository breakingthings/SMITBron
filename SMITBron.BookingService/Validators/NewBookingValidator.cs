using FluentValidation;
using LinqToDB;
using SMITBron.BookingService.Requests;
using SMITBron.Core.Entities;
using SMITBron.Infrastructure;

namespace SMITBron.BookingService.Validators
{
    public class NewBookingValidator : AbstractValidator<NewBooking>
    {
        private readonly RWDbConnection _db;

        public NewBookingValidator(RWDbConnection db)
        {
            this._db = db;

            RuleFor(x => x.Email).NotEmpty().EmailAddress().NotNull();
            RuleFor(x => x.IdCode).NotNull().NotEmpty();
            RuleFor(x => x.StartDate).NotNull().LessThanOrEqualTo(x => x.EndDate).GreaterThanOrEqualTo(DateTime.Now.Date);
            RuleFor(x => x.EndDate).NotNull().GreaterThanOrEqualTo(x => x.StartDate).GreaterThanOrEqualTo(DateTime.Now.Date);
            RuleFor(x => x.ApartmentId).NotNull().NotEmpty();
            RuleFor(x => x.Firstname).NotNull().NotEmpty();
            RuleFor(x => x.Lastname).NotNull().NotEmpty();
            
        }
    }

}
