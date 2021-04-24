using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace az_204_service_bus_Rec
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://az204servbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=kUCNb97UoNFoay5lrkhZyWJnPFrxoO3GmmB62nMVrCY=";
        const string QueueName = "az204-servbus-queue";
        static IQueueClient queueClient;

        static async Task Main(string[] args)

        {
            queueClient = new QueueClient(ServiceBusConnectionString,QueueName);
            Console.WriteLine("===============================================");
            Console.WriteLine("Press Enter key to Exit After Recieving messages");
            Console.WriteLine("================================================");
            RegisterOnMessageHandlerAndReceiveMessage();
            Console.ReadKey();
            await queueClient.CloseAsync();
            Console.WriteLine("Hello World!");
        }
        static void RegisterOnMessageHandlerAndReceiveMessage(){

            var messageHandlerOptions = new 
            MessageHandlerOptions(ExceptionRecievedHandler){
            MaxConcurrentCalls = 1,
            AutoComplete = false
           };
           queueClient.RegisterMessageHandler(ProcessMessageAsync,messageHandlerOptions);

        }
         static async Task ProcessMessageAsync(Message message,CancellationToken cancellationToken){
           var messageSeqNumber = message.SystemProperties.SequenceNumber;
           var messageBody = Encoding.UTF8.GetString(message.Body);
           Console.WriteLine($"Recieved Message: \n Sequence Number {messageSeqNumber} \n Body: {messageBody}");
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);

         }
         static Task ExceptionRecievedHandler(ExceptionReceivedEventArgs arg){
 Console.WriteLine($"Message Handler Encounterd Exception");
 var context = arg.ExceptionReceivedContext;
 Console.WriteLine("Exception Context for Trouble Shooting");
 Console.WriteLine($"- Endpoint {context.Endpoint}");
 Console.WriteLine($"- Entity Path: {context.EntityPath}");
 return Task.CompletedTask;
 }
    }
}
