using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using IServices;

namespace EmployeeServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            GetEmployeeTest();

            Console.WriteLine("Press Enter key to exit.");
            Console.ReadLine();
        }

        private static void GetEmployeeTest()
        {
            Uri uri = new Uri(ConfigurationManager.AppSettings["EmployeeServiceUrl"]);

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(uri);

            ChannelFactory<IEmployeeService> proxy = new ChannelFactory<IEmployeeService>(binding, endpoint);

            IEmployeeService employeeService = proxy.CreateChannel();

            Employee employee = new Employee { Id = 2, FirstName = "Bartek", LastName = "Sulecki" };

            employeeService.Add(employee);

            Employee anotherEmployee = employeeService.Get(1);

            Console.WriteLine($"{anotherEmployee.Id} {anotherEmployee.FirstName} {anotherEmployee.LastName}");

           

          

        }
    }
}
