using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.HotelService.Responses
{
    public class AllBookingsResult
    {
        public class Booking
        {
            public Guid Id { get; set; }

            public Guid UserId { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }
            
            public string UserEmail { get; set; }

            public string UserIdCode { get; set; }

            public string UserFirstname { get; set; }

            public string UserLastname { get; set; }

            public DateTimeOffset? CancelDate { get; set; }

            public int ApartmentNumber { get; set; }

            public Guid ApartmentId { get; set; }

            public decimal ApartmentPrice { get; set; }

            public decimal TotalPrice { get; set; }
        }

        public int TotalCount { get; set; }

        public IEnumerable<Booking> Bookings { get; set; }
    }
}
