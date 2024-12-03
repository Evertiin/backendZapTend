using System.Collections.Generic;

namespace backend.Models
{
    public class WebhookRequest
    {
        public webhook webhook { get; set; }

        public WebhookRequest()
        {
            webhook = new webhook();
        }
    }

    public class webhook
    {
        public bool enabled { get; set; }
        public string url { get; set; }
        public List<string> events { get; set; }

        public webhook()
        {
            events = new List<string>();
        }
    }
}
