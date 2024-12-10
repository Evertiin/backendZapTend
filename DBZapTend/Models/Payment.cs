using System;
using System.Collections.Generic;

namespace DBZapTend.Models;

public partial class Payment
{
    public int IdPayments { get; set; }

    public string TypePayment { get; set; } = null!;

    public decimal ValuePayment { get; set; }

    public int? UserId { get; set; }

    public DateTime DueDate { get; set; }

    public string Cycle { get; set; } = null!;

    public string? Description { get; set; }

    public int? PlanId { get; set; }

    public virtual Plan? Plan { get; set; }

    public virtual User? User { get; set; }
}
