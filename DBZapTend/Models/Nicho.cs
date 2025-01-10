using System;
using System.Collections.Generic;

namespace DBZapTend.Models;

public partial class Nicho
{
    public int IdNichos { get; set; }

    public string Name { get; set; }

    public int? CategoryIdCategory { get; set; }

    public virtual Category CategoryIdCategoryNavigation { get; set; }

    public virtual ICollection<Prompt> Prompts { get; set; } = new List<Prompt>();

    public virtual ICollection<UserNicho> UserNichos { get; set; } = new List<UserNicho>();
}
