using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaObject
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public int AccumulatedPoint { get; set; }
    }
}
