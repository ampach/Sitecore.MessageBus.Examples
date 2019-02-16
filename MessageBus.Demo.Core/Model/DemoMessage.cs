using System;

namespace MessageBus.Demo.Core.Model
{
    public class DemoMessage
    {
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}