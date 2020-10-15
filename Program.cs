using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EventHubsSender
{
    class Program
    {
        private const string connectionString = "";
        private const string eventHubName = "myeventhub";
        
        static async Task Main()
        {
            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("First event")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Second event")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Third event")));

                await producerClient.SendAsync(eventBatch);
                Console.WriteLine("A batch of 3 events has been published");
            }
        }
    }
}
