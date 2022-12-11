using LinqToDB.Mapping;

namespace SMITBron.Core.Entities
{
    [Table("apartment")]
    public class Apartment
    {
        [PrimaryKey]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("roomcount")]
        public int RoomCount { get; set; }

        [Column("apartmentnumber")]
        public int ApartmentNumber { get; set; }

        [Column("bedcount")]
        public int BedCount { get; set; }

        [Column("price")]
        public decimal Price{ get; set; }

    }
}
