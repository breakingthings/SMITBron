using FluentValidation;
using SMITBron.BookingService.Requests;

namespace SMITBron.BookingService.Validators
{
    public class NewBookingValidator : AbstractValidator<NewBooking>
    {
        public NewBookingValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().NotNull();
            RuleFor(x => x.IdCode).NotNull().NotEmpty();
            RuleFor(x => x.StartDate).NotNull().LessThanOrEqualTo(x => x.EndDate);
            RuleFor(x => x.EndDate).NotNull().GreaterThanOrEqualTo(x => x.StartDate);
            RuleFor(x => x.ApartmentId).NotNull().NotEmpty();
            RuleFor(x => x.Firstname).NotNull().NotEmpty();
            RuleFor(x => x.Lastname).NotNull().NotEmpty();
        }
    }

}
