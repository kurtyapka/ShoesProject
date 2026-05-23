using System;
using System.Collections.Generic;

namespace ShoesProject.Models;

public partial class DeliveryPoint
{
    public int Id { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
