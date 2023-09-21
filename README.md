# Service Bus Queue producer/consumer demo

## Azure Service Bus

This demo is intended to exemplify the producer and consumer actors, focusing in batching processing of messages on both sides. The demo uses queues but can easily be changed to use topics/subscriptions.

This demo contains 2 projects, one for the producer and one for the consumer. Each one is a console that accepts two arguments: queue name and connection string, as follows:

    serviceBusProducer.exe 'my-queue' 'Endpoint=sb://my-demo-1.servicebus.windows.net/;SharedAccessKeyName=keyName;SharedAccessKey=xyzabc...'

    serviceBusConsumer.exe 'my-queue' 'Endpoint=sb://my-demo-1.servicebus.windows.net/;SharedAccessKeyName=keyName;SharedAccessKey=xyzabc...'

In order to load against the queue, you can usera for loop in a script, in the following example, a powershell loop:

    for($i=0;$i -ne 5000;$++){
        serviceBusProducer.exe 'my-queue' 'Endpoint=sb://my-demo-1.servicebus.windows.net/;SharedAccessKeyName=keyName;SharedAccessKey=xyzabc...'
    }

Start one or more threads to start consuming from queue, keeping in mind this is a competing scenario, where only one consumer will receive the message:

    serviceBusConsumer.exe 'my-queue' 'Endpoint=sb://my-demo-1.servicebus.windows.net/;SharedAccessKeyName=keyName;SharedAccessKey=xyzabc...'

