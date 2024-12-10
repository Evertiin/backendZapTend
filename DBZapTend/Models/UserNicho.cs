using System;
using System.Collections.Generic;

namespace DBZapTend.Models;

public partial class UserNicho
{
    public int IdUserNichos { get; set; }

    public int? UserId { get; set; }

    public int? NichosIdNichos { get; set; }

    public virtual Nicho? NichosIdNichosNavigation { get; set; }

    public virtual User? User { get; set; }
}
