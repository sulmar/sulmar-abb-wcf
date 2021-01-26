using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesService
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        EmployeeResponse Get(EmployeeRequest request);

        [OperationContract]
        void Add(EmployeeRequest request);
    }

    public class EmployeeService : IEmployeeService
    {
        public void Add(EmployeeRequest request)
        {
            Trace.WriteLine($"{request.EmployeeId} {request.LicenseKey}");
        }

        public EmployeeResponse Get(EmployeeRequest request)
        {
            return new EmployeeResponse { Id = 1, FirstName = "Marcin", LastName = "Sulecki" };
        }
    }
}
