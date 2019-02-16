using MessageBus.Demo.Core.Model;
using MessageBus.Demo.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Framework.Messaging;

namespace MessageBus.Demo.Services
{
    public class RegisterDependencies : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            var assemblyArray = new[]
            {
                GetType().Assembly
            };
            serviceCollection.AddTransient<IMessageHandler<PubSubDemoMessage>, PubSubMessageHandler>();
        }
    }
}