using Paramore.Brighter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.BookingService.Requests
{
    public class CancelBooking : Command
    {   
        public Guid BookingId { get; }
        
        public string Email { get; }
        
        public string IdCode { get; }

        public CancelBooking(Guid bookingId, string email, string idCode) : base(new Guid())
        {
            BookingId = bookingId;
            Email = email;
            IdCode = idCode;
        }

    }
}
