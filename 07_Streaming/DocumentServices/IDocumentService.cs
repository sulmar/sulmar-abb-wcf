using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices
{
    [ServiceContract]
    public interface IDocumentService
    {
        [OperationContract]
        // [ChunkingBehavior(ChunkingAppliesTo.OutMessage)]
        // https://github.com/SebastiaanLubbers/WF_WCF_Samples/blob/master/WCF/Extensibility/Channels/ChunkingChannel/CS/ChunkingChannel/ChunkingBehavior.cs
        Stream GetLargeDocument();

        // MTOM encoding
        // https://docs.microsoft.com/pl-pl/dotnet/framework/wcf/samples/mtom-encoding

        // TODO: implement AddLargeDocument method on the server-side and client-side
        [OperationContract]
        void AddLargeDocument(Stream stream);

        [OperationContract]
        byte[] GetBytesDocument();
    }

    
    public class DocumentService : IDocumentService
    {
        public void AddLargeDocument(Stream stream)
        {
            using (stream)
            using (FileStream fileStream = File.Create("uploaded.jpg"))
            {
                stream.CopyTo(fileStream);
            }
        }

        public byte[] GetBytesDocument()
        {
            throw new NotImplementedException();
        }

        public Stream GetLargeDocument()
        {
            string filepath = @"C:\temp\photo1.jpg";

            FileStream stream = File.OpenRead(filepath);

            return stream;
        }
    }
}
