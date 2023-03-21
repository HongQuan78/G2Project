using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaObject
{
    public partial class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public int Idpeople { get; set; }
    }
}
