using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatServices
{
    [ServiceContract(CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = true)]
        void Join(string username);

        [OperationContract(IsOneWay = true)]
        void Send(string message);
    }

    //[ServiceContract]
    public interface IChatServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnReceive(string username, string message);
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        private readonly IDictionary<IChatServiceCallback, string> users = new Dictionary<IChatServiceCallback, string>();

        public void Join(string username)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();

            users[connection] = username;

            connection.OnReceive("ABB Server", $"Welcome {username}!");
        }

        public void Send(string message)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();

            if (!users.TryGetValue(connection, out string username))
            {
                return;
            }

            foreach (var others in users.Keys)
            {
                if (others == connection)
                    continue;

                others.OnReceive(username, message);
            }
        }
    }
}
