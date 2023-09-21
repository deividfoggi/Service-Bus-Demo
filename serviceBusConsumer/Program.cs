using Azure.Messaging.ServiceBus;
using System;
using System.IO;
using System.Threading.Tasks;

// receive a parameter in the console run to specify the queue name
string queueName = args[0];

// create a Service Bus client
string connectionString = args[1];
var client = new ServiceBusClient(connectionString);

// create a receiver for the queue
ServiceBusReceiver receiver = client.CreateReceiver(queueName);

IReadOnlyList<ServiceBusReceivedMessage> messages; 
do
{
    // receive a batch of messages from the queue
    messages = await receiver.ReceiveMessagesAsync(10, TimeSpan.FromSeconds(5));

    if (messages.Count > 0)
    {
        foreach (ServiceBusReceivedMessage message in messages)
        {
            // complete the message to remove it from the queue
            await receiver.CompleteMessageAsync(message);

            // Define a string variable that uses the message id for a txt file name
            string fileName = $"{message.MessageId}.txt";

            // create a file in the receivedMessages folder and write the message content on it
            await File.WriteAllTextAsync($".\\receivedMessages\\{fileName}", message.Body.ToString());

            Console.WriteLine($"Received and completed message: {message.MessageId}");
        }
    }
} while (messages.Count > 0);

await receiver.DisposeAsync();
await client.DisposeAsync();