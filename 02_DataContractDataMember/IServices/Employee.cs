using System.Runtime.Serialization;

namespace IServices
{
    [DataContract]
    public class Employee
    {
        [DataMember(Name = "EmployeeId", Order = 4)]
        public int Id { get; set; }
        [DataMember(Order = 1)] 
        public string FirstName { get; set; }
        [DataMember(Order = 2)] 
        public string LastName { get; set; }
        [DataMember(Order = 3)]
        public decimal Salary { get; set; }

        public bool IsSelected { get; set; }
    }
}
