using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace SMITBron.Core.Entities
{
    [Table("booking")]
    public class Booking
    {
        [PrimaryKey]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("startdate")]
        public DateTime StartDate { get; set; }

        [Column("enddate")]
        public DateTime EndDate { get; set; }

        [Column("canceldate", CanBeNull = true)]
        public DateTimeOffset? CancelDate { get; set; }

        [Column("apartmentid")]
        public Guid ApartmentId { get; set; }

        [Association(ThisKey = nameof(ApartmentId), OtherKey = nameof(Entities.Apartment.Id))]
        public Apartment Apartment { get; set; }

        [Column("guestid")]
        public Guid GuestId { get; set; }

        [Association(ThisKey = nameof(GuestId), OtherKey = nameof(Entities.Guest.Id))]
        public Guest Guest { get; set; }

    }
}
