using DocumentServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:5000/DocumentService";

            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = 64000000;

            EndpointAddress endpoint = new EndpointAddress(url);

            ChannelFactory<IDocumentService> proxy = new ChannelFactory<IDocumentService>(binding, endpoint);

            IDocumentService client = proxy.CreateChannel();

            string filepath = "photo1-downloaded.jpg";

            using (Stream stream = client.GetLargeDocument())
            using (FileStream fileStream = File.Create(filepath))
            {
                stream.CopyTo(fileStream);
                               
            } // stream.Dispose();
            // fileStream.Dispose();
        }
    }
}
