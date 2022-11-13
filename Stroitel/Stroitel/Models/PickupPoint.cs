using System;
using System.Collections.Generic;

namespace Stroitel.Models
{
    public partial class PickupPoint
    {
        public PickupPoint()
        {
            Orders = new HashSet<Order>();
        }

        public int PickupPointId { get; set; }
        public string PickupPointName { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
