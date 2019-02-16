using System.Threading.Tasks;
using log4net;
using MessageBus.Demo.Core.Model;
using Sitecore.Abstractions;
using Sitecore.Framework.Messaging;

namespace MessageBus.Demo.Handlers
{
    public class PubSubMessageHandler : IMessageHandler<PubSubDemoMessage>
    {
        private readonly ILog _logger;
        public PubSubMessageHandler()
        {
            _logger = Sitecore.Diagnostics.LoggerFactory.GetLogger("PubSubMessageBusDemoLog");
        }

        public Task Handle(PubSubDemoMessage message, IMessageReceiveContext receiveContext, IMessageReplyContext replyContext)
        {
            if (this.ValidateMessage(message))
            {
                //Here we can do something with message data
                _logger.Info("Message is got: Message text: " + message.Message 
                             + ", Time Stamp: " + message.TimeStamp.ToString("f"));
            }
            
            return Task.CompletedTask;
        }

        private bool ValidateMessage(PubSubDemoMessage message)
        {
            if (message == null)
            {
                _logger.Error($"[MessageBus.Demo.Processors.MessageHandler]: Message is null.");
                return false;
            }

            if (string.IsNullOrEmpty(message.Message))
            {
                _logger.Error($"[MessageBus.Demo.Processors.MessageHandler]: ContactId is wrong.");
                return false;
            }

            return true;
        }
    }
}