using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDC_F24.Domain.Entities
{
    public class Order: BaseEntity
    {

        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }

        public User User { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
