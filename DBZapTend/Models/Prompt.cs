using System;
using System.Collections.Generic;

namespace DBZapTend.Models;

public partial class Prompt
{
    public int IdPrompts { get; set; }

    public string Title { get; set; }

    public string Conteudo { get; set; }

    public int? NichosIdNichos { get; set; }

    public virtual Nicho NichosIdNichosNavigation { get; set; }

    public virtual ICollection<ValoresVariavei> ValoresVariaveis { get; set; } = new List<ValoresVariavei>();

    public virtual ICollection<Variavei> Variaveis { get; set; } = new List<Variavei>();
}
