using DeviceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace DeviceServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri uri = new Uri("http://localhost:50574/DeviceService.svc");

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(uri);

            ChannelFactory<IDeviceService> proxy = new ChannelFactory<IDeviceService>(binding, endpoint);

            IDeviceService deviceService = proxy.CreateChannel();

            using (OperationContextScope context = new OperationContextScope(((IContextChannel)deviceService)))
            {
                MessageHeader messageHeader = MessageHeader.CreateHeader("secret-key", string.Empty, "12345");

                OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader);

                var devices = deviceService.Get();

                var incommingHeaders = OperationContext.Current.IncomingMessageHeaders;
            }

          

            Console.ReadLine();
        }
    }
}
