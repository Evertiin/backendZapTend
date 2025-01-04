using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DBZapTend.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    public string Name { get; set; } = null!;
    //[JsonIgnore]
    public virtual ICollection<Nicho> Nichos { get; set; } = new List<Nicho>();
}
