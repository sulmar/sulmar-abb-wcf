using IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        public void Add(Employee employee)
        {
            Trace.WriteLine($"Added {employee.FirstName} {employee.LastName}");
        }

        public Employee Get(int id)
        {
            // return new Employee { Id = 1, FirstName = "Marcin", LastName = "Sulecki", IsSelected = true };

            // return new FullTimeEmployee { Id = 1, FirstName = "Marcin", LastName = "Sulecki", IsSelected = true, AnnualSalary = 100_000, Department = "IT" };

            PartTimeEmployee partTimeEmployee = new PartTimeEmployee { Id = 1, FirstName = "Marcin", LastName = "Sulecki", IsSelected = true, HourlyPay = 100 };

            Invoice invoice = new Invoice { Id = 1, TotalAmount = 10000, Employee = partTimeEmployee };

            partTimeEmployee.LastInvoice = invoice;

            return partTimeEmployee;

        }
    }
}
