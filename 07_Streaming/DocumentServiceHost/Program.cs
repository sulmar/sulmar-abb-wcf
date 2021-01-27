using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(DocumentServices.DocumentService)))
            {
                host.Open();

                Console.WriteLine("Press any Enter to exit.");
                Console.ReadLine();
            }
            
        }
    }
}
