namespace DBZapTend.DTO
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CpfCnpj { get; set; } 
        public long? Telephone { get; set; } 
        public string Address { get; set; }
        //public string Sobrenome { get; set; }
       public int? PlanId { get; set; }
        public bool TermsAccepted { get; set; }
        public string TermsVersion { get; set; }
        public string IdAutentication { get; set; }
        public string Role { get; set; }    
    }
}

