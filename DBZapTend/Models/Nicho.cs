using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DBZapTend.Models;

public partial class Nicho
{
    public int IdNichos { get; set; }

    public string Name { get; set; } = null!;

    public int? CategoryIdCategory { get; set; }

    public virtual Category? CategoryIdCategoryNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Prompt> Prompts { get; set; } = new List<Prompt>();
    [JsonIgnore]
    public virtual ICollection<UserNicho> UserNichos { get; set; } = new List<UserNicho>();
}
