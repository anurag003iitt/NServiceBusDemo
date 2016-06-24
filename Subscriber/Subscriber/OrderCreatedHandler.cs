using NServiceBus;
using NServiceBus.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber
{
    public class OrderCreatedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderCreatedHandler>();

        public void Handle(OrderPlaced message)
        {
            log.InfoFormat("Handling: OrderPlaced for Order Id: {0}", message.OrderId);
        }
    }
}
