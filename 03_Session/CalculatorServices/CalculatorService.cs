using System.Runtime.Serialization;
using System.ServiceModel;

namespace CalculatorServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class CalculatorService : ICalculatorService
    {
        private int _counter = 0;

        public int Add(int x, int y)
        {
            _counter++;

            return x + y;
        }

        public int Divide(int numerator, int denominator)
        {
            if (denominator==0)
            {
                DivideByZeroFault divideByZeroFault = new DivideByZeroFault
                {
                    Code = 101,
                    Error = "Divide by zero",
                    Details = "Denominator is equal 0!"
                };

                throw new FaultException<DivideByZeroFault>(divideByZeroFault, "DivideByZero");
            }

            return numerator / denominator;
        }
    }

    [DataContract]
    public class DivideByZeroFault
    {
        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Error { get; set; }
        [DataMember]
        public string Details { get; set; }
    }
}
