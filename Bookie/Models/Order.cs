using System;
using System.Collections.Generic;
using System.Text;

namespace Bookie.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string ShipAddress { get; set; }
        public DateTime DatePurchase { get; set; }
        public double OrderTotal { get; set; }
        public bool isTransported { get; set; }
        public bool isComplete { get; set; }
        public bool isCancelled { get; set; }
        public int UserID { get; set; }
        public string StatusTitle { get; set; }
        public string StatusTitleColor { get; set; }
        public string StatusCoverColor { get; set; }
    }
}
