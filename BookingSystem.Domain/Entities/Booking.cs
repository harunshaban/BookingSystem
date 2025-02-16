using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Entities
{
    public class Booking
    {
        public int Id { get; private set; }
        public DateTime DateFrom { get; private set; }
        public DateTime DateTo { get; private set; }
        public int BookedQuantity { get; private set; }
        public int ResourceId { get; private set; }
        public Resource Resource { get; private set; }

        public Booking(DateTime dateFrom, DateTime dateTo, int bookedQuantity, int resourceId)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
            BookedQuantity = bookedQuantity;
            ResourceId = resourceId;
        }

        private Booking() { }
    }
}
