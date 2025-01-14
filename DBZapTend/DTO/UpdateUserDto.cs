namespace DBZapTend.DTO
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long? CpfCnpj { get; set; } 
        public long? Telephone { get; set; } 
        public string Adress { get; set; }
    }
}

