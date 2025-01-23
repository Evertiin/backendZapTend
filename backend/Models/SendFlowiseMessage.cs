namespace APIWhatssApp.Models
{
    public record SendFlowiseMessage
    {
        public string question { get; set; }
        public string sessionId { get; set; }
    }
}
