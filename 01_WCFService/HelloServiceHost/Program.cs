using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HelloServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {

            // add reference System.ServiceModel

            using (ServiceHost host = new ServiceHost(typeof(FakeHelloServices.FakeHelloService)))
            {
                host.Open();

                Console.WriteLine("Host started on");

                foreach (var uri in host.BaseAddresses)
                {
                    Console.WriteLine(uri);
                }

                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();
            }



        }
    }
}
