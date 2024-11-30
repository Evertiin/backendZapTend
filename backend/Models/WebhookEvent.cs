using System;

namespace backend.Models
{
    public class WebhookEvent
    {
        public string Event { get; set; }
        public string Instance { get; set; }
        public Data Data { get; set; }
        public string Destination { get; set; }
        public DateTime DateTime { get; set; }
        public string Sender { get; set; }
        public string ServerUrl { get; set; }
        public string Apikey { get; set; }
    }

    public class Data
    {
        public Key Key { get; set; }
        public string PushName { get; set; }
        public string Status { get; set; }
        public Message Message { get; set; }
        public string MessageType { get; set; }
        public long MessageTimestamp { get; set; }
        public string InstanceId { get; set; }
        public string Source { get; set; }
    }

    public class Key
    {
        public string RemoteJid { get; set; }
        public bool FromMe { get; set; }
        public string Id { get; set; }
    }

    public class Message
    {
        public string Conversation { get; set; }
    }


}
