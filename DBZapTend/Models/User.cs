using System;
using System.Collections.Generic;

namespace DBZapTend.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public long CpfCnpj { get; set; }

    public long Telephone { get; set; }

    public string Adress { get; set; }

    public string IdAutentication { get; set; }

    public string Sobrenome { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }

    public virtual ICollection<Instance> Instances { get; set; } = new List<Instance>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();

    public virtual ICollection<UserNicho> UserNichos { get; set; } = new List<UserNicho>();

    public virtual ICollection<ValoresVariavei> ValoresVariaveis { get; set; } = new List<ValoresVariavei>();
}
