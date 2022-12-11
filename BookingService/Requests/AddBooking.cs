using Paramore.Brighter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.BookingService.Requests
{
    public class NewBooking : Command
    {
        public Guid ApartmentId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public string Email { get; set; }

        public string IdCode { get; set; }
        
        public string Firstname { get; }
        
        public string Lastname { get; }

        public NewBooking(Guid apartmentId, DateTime startDate, DateTime endDate, string email, string idCode, string firstname,
            string lastname) : base(new Guid())
        {
            ApartmentId = apartmentId;
            StartDate = startDate;
            EndDate = endDate;
            Email = email;
            IdCode = idCode;
            Firstname = firstname;
            Lastname = lastname;
        }

        
    }
}
