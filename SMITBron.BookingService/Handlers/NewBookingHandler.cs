using LinqToDB;
using SMITBron.Core.Entities;
using Paramore.Brighter;
using SMITBron.BookingService.Requests;
using SMITBron.Infrastructure;


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
                    await _db.InsertAsync<Guest>(guest);
                }

                //returned id
                command.NewId = Guid.NewGuid();

                //add the booking
                await _db.InsertAsync<Booking>(new Booking
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
