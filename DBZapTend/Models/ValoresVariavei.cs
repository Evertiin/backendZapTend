using System;
using System.Collections.Generic;

namespace DBZapTend.Models;

public partial class ValoresVariavei
{
    public int IdValoresVariaveis { get; set; }

    public string UserId { get; set; }

    public int? PromptsIdPrompts { get; set; }

    public string Value { get; set; }

    public int? VariaveisIdVariaveis { get; set; }

    public virtual Prompt PromptsIdPromptsNavigation { get; set; }

    public virtual User User { get; set; }

    public virtual Variavei VariaveisIdVariaveisNavigation { get; set; }
}
