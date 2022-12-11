using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.HotelService.Responses
{
    public class HotelApartmentResult
    {
        public Guid Id { get; set; }
        
        public int NumberOfRooms {  get; set; } 

        public int NumberOfBeds { get; set; }

        public decimal Price { get; set; }
    }
}
