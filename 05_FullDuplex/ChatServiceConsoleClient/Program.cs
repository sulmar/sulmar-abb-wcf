using ChatServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            WSDualHttpBinding binding = new WSDualHttpBinding();
            EndpointAddress endpoint = new EndpointAddress("http://localhost:5000/ChatService.svc");
            InstanceContext context = new InstanceContext(new ConsoleCallback());

            DuplexChannelFactory<IChatService> channelFactory = new DuplexChannelFactory<IChatService>(context, binding, endpoint);

            IChatService client = channelFactory.CreateChannel();

            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            client.Join(username);

            Console.Write("Enter message: ");
            string message = Console.ReadLine();

            while(message != "q")
            {
                client.Send(message);

                Console.Write("Enter message: ");
                message = Console.ReadLine();
            }
        }
    }

    public class ConsoleCallback : IChatServiceCallback
    {
        public void OnReceive(string username, string message)
        {
            Console.WriteLine($"[{username}] {message}");
        }
    }
}
