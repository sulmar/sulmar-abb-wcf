using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        Employee Get(int id);
        
        [OperationContract]
        void Add(Employee employee);
    }
}
