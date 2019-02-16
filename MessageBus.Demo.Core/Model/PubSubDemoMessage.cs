using System;

namespace MessageBus.Demo.Core.Model
{
    public class PubSubDemoMessage
    {
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}