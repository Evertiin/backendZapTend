using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DBZapTend.Models;

public partial class Variavei
{
    public int IdVariaveis { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? PromptsIdPrompts { get; set; }

    public virtual Prompt? PromptsIdPromptsNavigation { get; set; }
    //[JsonIgnore]
    public virtual ICollection<ValoresVariavei> ValoresVariaveis { get; set; } = new List<ValoresVariavei>();
}
