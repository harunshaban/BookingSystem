using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Entities
{
    public class Resource
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }

        public Resource(string name, int quantity) 
        {
            Name = name;
            Quantity = quantity;
        }

        private Resource() { }
    }
}
