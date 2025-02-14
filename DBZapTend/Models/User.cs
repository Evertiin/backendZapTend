using DBZapTend.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CpfCnpj { get; set; }
    public int? PlanId { get; set; }
    public virtual Plan Plan { get; set; }  
    public long Telephone { get; set; }
    public string Address { get; set; }
    public string IdAutentication { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string Role { get; set; }

    public virtual ICollection<Instance> Instances { get; set; } = new List<Instance>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<UserNicho> UserNichos { get; set; } = new List<UserNicho>();
    public virtual ICollection<ValoresVariavei> ValoresVariaveis { get; set; } = new List<ValoresVariavei>();
}
