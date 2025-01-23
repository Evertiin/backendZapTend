namespace APIWhatssApp.Models
{
    public class overrideConfig
    {
        public string sessionId { get; set; }
    }
        public record SendFlowiseMessage
    {
        public string question { get; set; }
        public overrideConfig overrideConfig { get; set; }
    }
}
