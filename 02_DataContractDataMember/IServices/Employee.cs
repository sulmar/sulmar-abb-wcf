using System.Runtime.Serialization;

namespace IServices
{
    public class PocoEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public bool IsSelected { get; set; }
    }

    [KnownType(typeof(FullTimeEmployee))]
    [KnownType(typeof(PartTimeEmployee))]
    [DataContract(Namespace = "http://abb.com/2021/01/25/Employee")]
    public class Employee
    {
        [DataMember(Name = "EmployeeId", Order = 4)]
        public int Id { get; set; }

        [DataMember(Order = 1, Name = "fm")] 
        public string FirstName { get; set; }

        [DataMember(Order = 2, Name = "ln")] 
        public string LastName { get; set; }
        [DataMember(Order = 3)]
        public decimal Salary { get; set; }

        public bool IsSelected { get; set; }
    }

    [DataContract]
    public class Invoice
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
        [DataMember] 
        public PartTimeEmployee Employee { get; set; }
    }

    [DataContract]
    public class FullTimeEmployee : Employee
    {
        [DataMember]
        public decimal AnnualSalary { get; set; }
        [DataMember] 
        public string Department { get; set; }
    }

    [DataContract]
    public class PartTimeEmployee : Employee
    {
        [DataMember]
        public decimal HourlyPay { get; set; }

        [DataMember]
        public Invoice LastInvoice { get; set; }
    }
}
