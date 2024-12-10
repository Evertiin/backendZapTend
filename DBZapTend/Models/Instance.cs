using System;
using System.Collections.Generic;

namespace DBZapTend.Models;

public partial class Instance
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? UserIduser { get; set; }

    public virtual User? UserIduserNavigation { get; set; }
}
