using System;
using System.Collections.Generic;

namespace DBZapTend.Models;

public class Plan
{
    public int Id { get; set; }
    public string Annually { get; set; }
    public string Monthly { get; set; }
    public int AmountInstance { get; set; } 
    public virtual User User { get; set; }  // Um plano pertence a apenas um usuário
    public string DiscountAnnually { get; set; }
    public string DiscountMonthly { get; set; }
    public string Title { get; set; }
}



