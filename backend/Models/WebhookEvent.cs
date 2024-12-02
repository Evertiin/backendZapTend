using Newtonsoft.Json.Linq;
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

        /*public override string ToString()
        {
            return $"Property1: {Event}, Property2: {Data.Message.Conversation}";
        }*/
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
        public MessageContextInfo MessageContextInfo { get; set; }
    }

    public class MessageContextInfo
    {
        public DeviceListMetadata DeviceListMetadata { get; set; }
        public int DeviceListMetadataVersion { get; set; }
        public string MessageSecret { get; set; }
    }

    public class DeviceListMetadata
    {
        public string SenderKeyHash { get; set; }
        public string SenderTimestamp { get; set; }
        public string SenderAccountType { get; set; }
        public string ReceiverAccountType { get; set; }
        public string RecipientKeyHash { get; set; }
        public string RecipientTimestamp { get; set; }
    }



}
