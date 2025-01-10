using System;
using System.Collections.Generic;

namespace DBZapTend.Models;

public partial class Plan
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string UserId { get; set; }

    public DateTime? SubscribeAt { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; }
}
