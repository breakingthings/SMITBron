using LinqToDB;
using SMITBron.Core.Entities;
using Paramore.Brighter;
using SMITBron.BookingService.Requests;
using SMITBron.Infrastructure;


namespace SMITBron.BookingService.Handlers
{
    public class CancelBookingHandler : RequestHandlerAsync<CancelBooking>
    {
        private readonly RWDbConnection _db;

        public CancelBookingHandler(RWDbConnection db)
        {
            this._db = db;
        }

        [RequestValidator(1, HandlerTiming.Before)]
        public override async Task<CancelBooking> HandleAsync(CancelBooking command, CancellationToken cancellationToken = default)
        {
            return await base.HandleAsync(command, cancellationToken);
        }
       
    }
}
