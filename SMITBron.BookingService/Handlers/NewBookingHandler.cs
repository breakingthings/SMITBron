using LinqToDB;
using SMITBron.Core.Entities;
using Paramore.Brighter;
using SMITBron.BookingService.Requests;
using SMITBron.Infrastructure;
using FluentValidation.Results;
using FluentValidation;

namespace SMITBron.BookingService.Handlers
{
    public class NewBookingHandler : RequestHandlerAsync<NewBooking>
    {
        private readonly RWDbConnection _db;

        public NewBookingHandler(RWDbConnection db)
        {
            this._db = db;
        }

        [RequestValidator(1, HandlerTiming.Before)]
        public override async Task<NewBooking> HandleAsync(NewBooking command, CancellationToken cancellationToken = default)
        {
            using var tx = await _db.BeginTransactionAsync(cancellationToken);
            try
            {

                var existingBooking = await _db.GetTable<Booking>().Where(x => x.ApartmentId == command.ApartmentId &&
                   ((x.StartDate >= command.StartDate && x.StartDate <= command.EndDate)
                   || (x.EndDate >= command.StartDate && x.EndDate <= command.EndDate)) && x.CancelDate == null)
                   .FirstOrDefaultAsync();

                if (existingBooking != null)
                {
                    throw new ValidationException(new ValidationFailure[]
                    {
                        new ValidationFailure("startDate", "Apartment already booked")
                    });
                }

                var guest = await _db.GetTable<Guest>().Where(x => x.IdCode == command.IdCode).FirstOrDefaultAsync();

                //if new guest insert
                if (guest == null) 
                {
                    guest = new Guest()
                    {
                        Id = Guid.NewGuid(),
                        Email = command.Email,
                        Firstname = command.Firstname,
                        Lastname = command.Lastname,
                        IdCode = command.IdCode
                    };
                    await _db.InsertAsync(guest);
                }

                //returned id
                command.NewId = Guid.NewGuid();

                //add the booking
                await _db.InsertAsync(new Booking
                {
                    Id = command.NewId.Value,
                    GuestId = guest.Id,
                    ApartmentId = command.ApartmentId,
                    StartDate = command.StartDate,
                    EndDate = command.EndDate
                });

                await tx.CommitAsync();
            }
            catch
            {
                tx.Rollback();
            }
            
            return await base.HandleAsync(command, cancellationToken);
        }
       
    }
}
