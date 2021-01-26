using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServices
{
    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        int Add(int x, int y);

        [OperationContract]
        [FaultContract(typeof(DivideByZeroFault))]
        int Divide(int x, int y);
    }
}
