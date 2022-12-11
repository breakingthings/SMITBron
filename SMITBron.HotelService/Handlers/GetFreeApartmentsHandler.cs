using LinqToDB;
using Paramore.Brighter;
using Paramore.Darker;
using SMITBron.Core.Entities;
using SMITBron.HotelService.Requests;
using SMITBron.HotelService.Responses;
using SMITBron.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.HotelService.Handlers
{
    public class GetFreeApartmentsHandler : QueryHandlerAsync<GetFreeApartments, IEnumerable<HotelApartmentResult>>
    {
        private readonly RWDbConnection _db;

        public GetFreeApartmentsHandler(RWDbConnection db)
        {
            this._db = db;
        }

        public override async Task<IEnumerable<HotelApartmentResult>> ExecuteAsync(GetFreeApartments query,
                CancellationToken cancellationToken = default)
        {
            return await _db.GetTable<Apartment>().Where(x => _db.GetTable<Booking>().Where(y => y.ApartmentId == x.Id && y.CancelDate == null
                && ((y.StartDate >= query.From && y.StartDate <= query.To) || (y.EndDate >= query.From && y.EndDate <= query.To))
                ).FirstOrDefault() == null).Select(x => new HotelApartmentResult
                {
                    Id = x.Id,
                    NumberOfBeds = x.BedCount,
                    NumberOfRooms = x.RoomCount,
                    Price = x.Price
                }).ToListAsync();
        }
    }
}
