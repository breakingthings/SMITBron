using System;

namespace SMITBron.Web.Models
{
    public class NewBookingModel
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string IdCode { get; set; }

        public Guid ApartmentId { get; set;  }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
