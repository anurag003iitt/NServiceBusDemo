using NServiceBus;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // This makes it easier to tell console windows apart
            Console.Title = "Client";

            var busConfiguration = new BusConfiguration();

            // The endpoint name will be used to determine queue names and serves
            // as the address, or identity, of the endpoint
            busConfiguration.EndpointName("Client");

            // Use JSON to serialize and deserialize messages (which are just
            // plain classes) to and from message queues
            busConfiguration.UseSerialization<JsonSerializer>();

            // Ask NServiceBus to automatically create message queues
            busConfiguration.EnableInstallers();

            // Store information in memory for this example, rather than in
            // a database. In this sample, only subscription information is stored
            busConfiguration.UsePersistence<InMemoryPersistence>();

            // Initialize the endpoint with the finished configuration
            using (var bus = Bus.Create(busConfiguration).Start())
            {
                SendOrder(bus);
            }
        }

        public static void SendOrder(IBus bus)
        {
            Console.WriteLine("Press enter to send a message");
            Console.WriteLine("Press any key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key != ConsoleKey.Enter)
                {
                    return;
                }
                var id = Guid.NewGuid();
                var placeOrder = new PlaceOrder
                {
                    Product = "New Laptop",
                    Id = id
                };
                bus.Send("Server", placeOrder);
                Console.WriteLine("Sent a new PlaceOrder message with id:" + id.ToString("N"));
            }
        }

    }
}
