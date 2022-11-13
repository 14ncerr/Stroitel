using System;
using System.Collections.Generic;

namespace Stroitel.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public int OrderPickupPointId { get; set; }
        public string? OrderClientFullName { get; set; }
        public string OrderPickupCode { get; set; } = null!;
        public string OrderStatus { get; set; } = null!;

        public virtual PickupPoint OrderPickupPoint { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
