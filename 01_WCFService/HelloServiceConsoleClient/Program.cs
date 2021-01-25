using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloServiceConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Proxy (design-pattern)
            ABBServiceReference.ServiceClient client = new ABBServiceReference.ServiceClient();

            await client.SendAsync("Hello World!");

            string result = await client.PingAsync("Hello again!");

            Console.WriteLine(result);


        }
    }
}
