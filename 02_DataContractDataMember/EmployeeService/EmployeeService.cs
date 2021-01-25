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
            return new Employee { Id = 1, FirstName = "Marcin", LastName = "Sulecki", IsSelected = true };
        }
    }
}
