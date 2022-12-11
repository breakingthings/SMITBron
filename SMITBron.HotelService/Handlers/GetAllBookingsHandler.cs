using LinqToDB;
using LinqToDB.SqlQuery;
using Paramore.Brighter;
using Paramore.Darker;
using SMITBron.Core.Entities;
using SMITBron.Core.Extensions;
using SMITBron.HotelService.Requests;
using SMITBron.HotelService.Responses;
using SMITBron.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.HotelService.Handlers
{
    public class GetAllBookingsHandler : QueryHandlerAsync<GetAllBookings, AllBookingsResult>
    {
        private readonly RWDbConnection _db;

        public GetAllBookingsHandler(RWDbConnection db)
        {
            this._db = db;
        }

        public override async Task<AllBookingsResult> ExecuteAsync(GetAllBookings query, CancellationToken cancellationToken = default)
        {

            var dbQuery = _db.GetTable<Booking>().LoadWith(x => x.Guest).LoadWith(x => x.Apartment)
                .Where(x => x.CancelDate == null).Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize);

            var sortFunc = GetSortFunc(query.SortField);

            var sortedQuery = query.SortDesc ? dbQuery.OrderByDescending(sortFunc) : dbQuery.OrderBy(sortFunc);

            var dbResult = await sortedQuery.Select(x => new
            {
                TotalCount = Linq2DbExtensions.CountAllOver,
                Booking = new AllBookingsResult.Booking
                {
                    Id = x.Id,
                    UserEmail = x.Guest.Email,
                    UserFirstname = x.Guest.Firstname,
                    UserId = x.Guest.Id,
                    UserIdCode = x.Guest.IdCode,
                    UserLastname = x.Guest.Lastname,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    CancelDate = x.CancelDate,
                    ApartmentNumber = x.Apartment.ApartmentNumber,
                    ApartmentId = x.ApartmentId,
                    ApartmentPrice = x.Apartment.Price
                }

            }).ToListAsync();

            dbResult.ForEach(x =>
            {
                var totalDays = (x.Booking.EndDate - x.Booking.StartDate).Days;
                x.Booking.TotalPrice = totalDays * x.Booking.ApartmentPrice;
            });

            return new AllBookingsResult
            {
                TotalCount = dbResult.Count > 0 ? dbResult.First().TotalCount : 0,
                Bookings = dbResult.Select(x => x.Booking).ToList()
            };
        }

        private Expression<Func<Booking, object>> GetSortFunc(string field)
        {
            Func<Booking, object> sortFunc;
            switch (field)
            {
                case nameof(Booking.StartDate):
                    sortFunc = x => x.StartDate;
                    break;
                case nameof(Booking.EndDate):
                    sortFunc = x => x.EndDate;
                    break;
                case nameof(Booking.Guest.Firstname):
                    sortFunc = x => x.Guest.Firstname;
                    break;
                default:
                    sortFunc = x => x.StartDate;
                    break;
            }
            
            return x => sortFunc;

        }
    }
}
