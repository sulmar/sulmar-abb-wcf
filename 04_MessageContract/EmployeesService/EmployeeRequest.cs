using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesService
{
    [MessageContract(IsWrapped = true, WrapperName = "MyEmployeeRequest", WrapperNamespace = "http://abb.com")]
    public class EmployeeRequest
    {
        [MessageHeader(Namespace = "http://abb.com")]
        public string LicenseKey { get; set; }

        [MessageBodyMember(Namespace = "http://abb.com")]
        public int EmployeeId { get; set; }
    }

    [MessageContract(IsWrapped = true, WrapperName = "MyEmployeeResponse", WrapperNamespace = "http://abb.com")]
    public class EmployeeResponse
    {
        [MessageBodyMember(Order = 1, Namespace = "http://abb.com")]
        public int Id { get; set; }
        [MessageBodyMember(Order = 2, Namespace = "http://abb.com")] 
        public string FirstName { get; set; }
        [MessageBodyMember(Order = 3, Namespace = "http://abb.com")] 
        public string LastName { get; set; }
    }
}
