using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DBZapTend.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long CpfCnpj { get; set; }

    public long Telephone { get; set; }

    public string Adress { get; set; } = null!;

    public string? IdAutentication { get; set; }
    [JsonIgnore]
    public virtual ICollection<Instance> Instances { get; set; } = new List<Instance>();
    [JsonIgnore]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    [JsonIgnore]
    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();
    [JsonIgnore]
    public virtual ICollection<UserNicho> UserNichos { get; set; } = new List<UserNicho>();
    [JsonIgnore]
    public virtual ICollection<ValoresVariavei> ValoresVariaveis { get; set; } = new List<ValoresVariavei>();
}
