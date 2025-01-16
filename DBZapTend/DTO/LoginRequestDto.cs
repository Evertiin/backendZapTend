namespace DBZapTend.DTO
{
    public record LoginRequestDto
    {
        public string Email { get; set; }
        public string IdAuthentication { get; set; }
        public string Role { get; set; }    
    }
}
