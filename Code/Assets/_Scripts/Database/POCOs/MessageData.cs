using System;
using Newtonsoft.Json;

public class MessageData : Data
{
    [JsonProperty("sender_id")] public string SenderID { get; set; }
    [JsonProperty("receiver_id")] public string ReceiverID { get; set; }
    [JsonProperty("sentAt")] public DateTime Timestamp { get; set; }
    [JsonProperty("content")] public string Content { get; set; }
}