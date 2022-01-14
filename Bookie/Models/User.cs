using System;
using System.Collections.Generic;
using System.Text;

namespace Bookie.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public string UserGender { get; set; }
        public DateTime UserDob { get; set; }
        public string UserPassword { get; set; }
    }
}
