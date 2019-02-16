using System;
using System.Threading.Tasks;
using log4net;
using MessageBus.Demo.Core.Model;
using Sitecore.Abstractions;
using Sitecore.Framework.Messaging;
using Sitecore.Framework.Messaging.DeferStrategies;
using Sitecore.Framework.Messaging.Rebus.Gateway;
using Sitecore.Messaging.GatewayClient.WebClients;

namespace MessageBus.Demo.Handlers
{
    public class MessageHandler : IMessageHandler<DemoMessage>
    {
        private readonly ILog _logger;
        public MessageHandler()
        {
            _logger = Sitecore.Diagnostics.LoggerFactory.GetLogger("MessageBusDemoLog");
        }

        public Task Handle(DemoMessage message, IMessageReceiveContext receiveContext, IMessageReplyContext replyContext)
        {
            if (this.ValidateMessage(message))
            {
                //Here we can do something with message data
                _logger.Info("Message is got: Message text: " + message.Message 
                             + ", Time Stamp: " + message.TimeStamp.ToString("f"));
            }

            return Task.CompletedTask;
        }

        private bool ValidateMessage(DemoMessage message)
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