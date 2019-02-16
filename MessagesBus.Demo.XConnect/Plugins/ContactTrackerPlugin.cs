using System;
using System.Linq;
using MessageBus.Demo.Core.Buses;
using MessageBus.Demo.Core.Model;
using Serilog;
using Sitecore.Framework.Messaging;
using Sitecore.XConnect;
using Sitecore.XConnect.Operations;
using Sitecore.XConnect.Service.Plugins;

namespace MessagesBus.Demo.XConnect.Plugins
{
    public class ContactTrackerPlugin : IXConnectServicePlugin, IDisposable
    {
        private XdbContextConfiguration _config;
        private readonly IMessageBus<DemoBus> _messageBus;
        private readonly IMessageBus<DemoPubSubBus> _pubSubMessageBus;

        public ContactTrackerPlugin(IMessageBus<DemoBus> messageBus, IMessageBus<DemoPubSubBus> pubSubMessageBus)
        {
            _messageBus = messageBus;
            _pubSubMessageBus = pubSubMessageBus;
            Log.Information("Create {0}", nameof(ContactTrackerPlugin));
        }

        /// <summary>Subscribes to events the current plugin listens to.</summary>
        /// <param name="config">
        ///   A <see cref="T:Sitecore.XConnect.XdbContextConfiguration" /> object that provides access to the configuration settings.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   Argument <paramref name="config" /> is a <b>null</b> reference.
        /// </exception>
        public void Register(XdbContextConfiguration config)
        {
            Log.Information("Register {0}", nameof(ContactTrackerPlugin));
            _config = config;
            RegisterEvents();
        }

        /// <summary>
        ///   Unsubscribes from events the current plugin listens to.
        /// </summary>
        public void Unregister()
        {
            Log.Information("Unregister {0}", nameof(ContactTrackerPlugin));
            UnregisterEvents();
        }

        private void RegisterEvents()
        {
            _config.OperationCompleted += OnOperationCompleted;
            _config.BatchExecuted += OnBatchExecuted;
        }

        private void UnregisterEvents()
        {
            _config.OperationCompleted -= OnOperationCompleted;
            _config.BatchExecuted -= OnBatchExecuted;
        }

        private void OnBatchExecuted(object sender, XdbOperationBatchEventArgs xdbOperationBatchEventArgs)
        {
            var operations = xdbOperationBatchEventArgs.Batch.Operations;

            if (operations.Any(q =>
                q is AddContactOperation && q.Target is IEntityReference<Contact> && (q.Target as IEntityReference<Contact>).EntityType == EntityType.Contact && q.Status == XdbOperationStatus.Succeeded && q.OperationType == XdbOperationType.Create) )
            {
                var filteredOperations = operations.Where(q =>
                    q is AddContactOperation && q.Target is IEntityReference<Contact> && (q.Target as IEntityReference<Contact>).EntityType == EntityType.Contact && q.Status == XdbOperationStatus.Succeeded && q.OperationType == XdbOperationType.Create);

                var i = 1;
                foreach (var operation in filteredOperations)
                {
                    _pubSubMessageBus.PublishAsync(new PubSubDemoMessage
                    {
                        Message = "PubSubBus: Message #" + i + ", Contact Facet was updated. Contact ID: " +
                                  ((IEntityReference<Contact>)operation.Target).Id.Value,
                        TimeStamp = DateTime.Now
                    }).Wait();

                    _messageBus.Send(new DemoMessage
                    {
                        Message = "Queue Bus: Message #" + i + ", Contact Facet was updated. Contact ID: " +
                                  ((IEntityReference<Contact>)operation.Target).Id.Value,
                        TimeStamp = DateTime.Now
                    });

                    i++;
                }
            }
        }

            /// <summary>
            /// Handles the event that is generated when an operation completes.
            /// </summary>
            /// <param name="sender">The <see cref="T:System.Object" /> that generated the event.</param>
            /// <param name="xdbOperationEventArgs">A <see cref="T:Sitecore.XConnect.Operations.XdbOperationEventArgs" /> object that provides information about the event.</param>
            private void OnOperationCompleted(object sender, XdbOperationEventArgs xdbOperationEventArgs)
        {
            //Check if no exceptions are occurred during executing the operation. If it is, it will not guarantee that contact was created.
            if (xdbOperationEventArgs.Operation.Exception != null)
                return;
        }

        /// <summary>
        ///   Releases managed and unmanaged resources used by the current class instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Releases managed and unmanaged resources used by the current class instance.
        /// </summary>
        /// <param name="disposing">
        ///   Indicates whether the current method was called from explicitly or implicitly during finalization.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;
            Log.Information("Dispose {0}", nameof(ContactTrackerPlugin));
            _config = null;
        }
    }
}