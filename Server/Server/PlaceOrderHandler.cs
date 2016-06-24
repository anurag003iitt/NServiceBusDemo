using NServiceBus;
using NServiceBus.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();
        IBus bus;

        public PlaceOrderHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(PlaceOrder message)
        {
            log.InfoFormat("Order for Product:{0} placed with id: {1}", message.Product, message.Id);

            log.InfoFormat("Publishing: OrderPlaced for Order Id: {0}", message.Id);

            var orderPlaced = new OrderPlaced
            {
                OrderId = message.Id
            };
            bus.Publish(orderPlaced);
        }
    }
}
