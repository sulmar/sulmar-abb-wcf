using System.ServiceModel;
using System.Threading.Tasks;

namespace IServices
{
    [ServiceContract(Name = "IService")]
    public interface IServiceAsync
    {
        [OperationContract]
        Task<string> PingAsync(string message);

        [OperationContract]
        Task SendAsync(string content);
    }
}
