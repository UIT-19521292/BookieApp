using System;
using System.Collections.Generic;
using System.Text;

namespace Bookie.Models
{
    public class Cart
    {
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public string BookGenre { get; set; }
        public string BookAuthor { get; set; }
        public string BookImg { get; set; }
        public double BookPrice { get; set; }
        public int BookQuantity { get; set; }
        public string BookDescription { get; set; }
        public int Quantity { get; set; }
    }
}
