using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBus.Demo.Core.Model;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Handlers;
using Serilog;
using Serilog.Events;

namespace MessageTrackerPubSub
{
    class Program
    {
        private static BuiltinHandlerActivator busActivator;

        static void Main(string[] args)
        {
            InitializeSubPubApplication();

            Helper.WriteIntro();

            var keepRunning = true;

            while (keepRunning)
            {
                var key = Console.ReadKey(true);

                switch (char.ToLower(key.KeyChar))
                {
                    case 'q':
                        Console.WriteLine("Quitting!");
                        keepRunning = false;
                        break;
                }
            }

            Console.WriteLine("Stopping the bus....");
        }

        private static void InitializeSubPubApplication()
            {
                busActivator = new BuiltinHandlerActivator();
                busActivator.Register(() => new MessageHandler());


                var logger = new LoggerConfiguration().WriteTo.ColoredConsole(LogEventLevel.Debug).CreateLogger();
                Log.Logger = logger;
                Configure.With(busActivator)
                    .Transport(t => t.UseSqlServer("messaging", "Sitecore_Transport", "PubSubDemoConsole"))
                    .Subscriptions(s => s.StoreInSqlServer("messaging", "Sitecore_Subscriptions", isCentralized: true))
                    .Logging(l => l.Serilog(logger))
                    .Start();

                busActivator.Bus.Subscribe<PubSubDemoMessage>().Wait();
            }

        public class MessageHandler : IHandleMessages<PubSubDemoMessage>
        {
            public async Task Handle(PubSubDemoMessage message)
            {
                Console.WriteLine("Message Tracker: " + message.Message + ", Time Stamp: " + message.TimeStamp.ToString("T"));
            }
        }
    }
    
}
