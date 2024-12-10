using System;
using System.Collections.Generic;

namespace DBZapTend.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Nicho> Nichos { get; set; } = new List<Nicho>();
}
