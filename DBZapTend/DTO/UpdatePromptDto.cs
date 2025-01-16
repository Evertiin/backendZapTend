namespace DBZapTend.DTO
{
    public record UpdatePromptDto
    {
        public string Title { get; set; }

        public string Conteudo { get; set; }

        public int? NichosIdNichos { get; set; }
    }
}
