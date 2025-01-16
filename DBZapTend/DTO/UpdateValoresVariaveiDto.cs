namespace DBZapTend.DTO
{
    public record UpdateValoresVariaveiDto
    {
        public int? PromptsIdPrompts { get; set; }

        public string Value { get; set; }

        public int? VariaveisIdVariaveis { get; set; }
    }
}
