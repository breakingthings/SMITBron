using System;

namespace SMITBron.Web.Models
{
    public class CancelBookingModel
    {
        public Guid BookingId { get; set; }

        public string Email { get; set; }

        public string IdCode { get; set; }


    }
}
