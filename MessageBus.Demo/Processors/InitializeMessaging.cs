using System;
using MessageBus.Demo.Models;
using Sitecore.Framework.Messaging;
using Sitecore.Pipelines;

namespace MessageBus.Demo.Processors
{
    public class InitializeMessaging
    {
        private readonly IServiceProvider serviceProvider;

        public InitializeMessaging(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Process(PipelineArgs args)
        {
            this.serviceProvider.StartMessageBus<DemoMessageProducer>();
        }
    }
}