using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace az_204_service_bus_Send
{
    

    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://az204servbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=kUCNb97UoNFoay5lrkhZyWJnPFrxoO3GmmB62nMVrCY=";
        const string QueueName = "az204-servbus-queue";
        static IQueueClient queueClient;
        static async Task Main(string[] args)
        {
            const int NumberOfMessages = 10;
            queueClient = new QueueClient(ServiceBusConnectionString,QueueName);
            Console.WriteLine("===============================================");
            Console.WriteLine("Press Enter to exist after sending all message");
            Console.WriteLine("===============================================");
            await SendMessageAsync(NumberOfMessages);
            Console.ReadKey();
            await queueClient.CloseAsync();
            Console.WriteLine("Hello World!");
        }
        static async Task SendMessageAsync(int NumberOfMessagesToSend){
            try{
                for(var i=0;i<NumberOfMessagesToSend;i++){
                    string messageBody = $"Message{i}";
                    var message = new Message((Encoding.UTF8.GetBytes(messageBody)));
                    Console.WriteLine($"Sending Message ... {messageBody}");
                    await queueClient.SendAsync(message);
                    
                }
            }catch(Exception e){
                    Console.WriteLine($"{DateTime.Now} :: Exception {e.Message}");
                }
            }
        }
}

