using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDC_F24.Domain.Entities
{
    public class Payment :BaseEntity
    {

        public Guid OrderId { get; set; }
        public DateTime PaidAt { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }

        public Order Order { get; set; }
    }
}

