using Azure.Messaging.ServiceBus;

// receive a parameter in the console run to specify the queue name
string queueName = args[0];

// create a Service Bus client
string connectionString = args[1];
var client = new ServiceBusClient(connectionString);

// create a sender for the queue
ServiceBusSender sender = client.CreateSender(queueName);

// create a batch of 10 messages to be sent
using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

// create a loop to create a service bus message named Message-i and try to add the message to the messageBatch
for (int i = 1; i <= 10; i++)
{
    // create a new message to send to the queue
    ServiceBusMessage message = new ServiceBusMessage($"Message-{i}");

    // try adding the message to the batch
    if (!messageBatch.TryAddMessage(message))
    {
        // if it is too large for the batch
        throw new Exception($"The message {i} is too large to fit in the batch.");
    }
}
// send the batch of messages to the queue
await sender.SendMessagesAsync(messageBatch);

Console.WriteLine($"A batch of 10 messages has been published to the queue.");

await sender.DisposeAsync();
await client.DisposeAsync();
