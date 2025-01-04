using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DBZapTend.Models;

public partial class Prompt
{
    public int IdPrompts { get; set; }

    public string Title { get; set; } = null!;

    public string Conteudo { get; set; } = null!;

    public int? NichosIdNichos { get; set; }

    public virtual Nicho? NichosIdNichosNavigation { get; set; }
    //[JsonIgnore]
    public virtual ICollection<ValoresVariavei> ValoresVariaveis { get; set; } = new List<ValoresVariavei>();
    //[JsonIgnore]
    public virtual ICollection<Variavei> Variaveis { get; set; } = new List<Variavei>();
}
