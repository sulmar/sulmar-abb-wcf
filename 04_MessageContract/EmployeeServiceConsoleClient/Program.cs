using EmployeesService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri uri = new Uri(ConfigurationManager.AppSettings["EmployeeServiceUrl"]);

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(uri);

            ChannelFactory<IEmployeeService> proxy = new ChannelFactory<IEmployeeService>(binding, endpoint);

            IEmployeeService employeeService = proxy.CreateChannel();

            EmployeeRequest request = new EmployeeRequest { EmployeeId = 1, LicenseKey = "AVBRR44D4" };
            
            EmployeeResponse response = employeeService.Get(request);
            
            employeeService.Add(request);

            Console.WriteLine($"{response.Id} {response.FirstName} {response.LastName}");

            Console.ReadLine();
        }
    }
}
