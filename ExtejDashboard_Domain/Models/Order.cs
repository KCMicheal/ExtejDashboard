using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtejDashboard_Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<CartItem>? Items { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
