using System.Collections.Generic;

namespace AspNetMvcModelBinderSamples.Models
{
    public abstract class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CustomerType Type { get; set; }

        public List<Order> Orders { get; set; }
    }
}