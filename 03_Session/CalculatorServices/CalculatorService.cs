using System.ServiceModel;

namespace CalculatorServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CalculatorService : ICalculatorService
    {
        private int _counter = 0;

        public int Add(int x, int y)
        {
            _counter++;

            return x + y;
        }
    }
}
