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

        [OperationContract]
        void AddLargeDocument(Stream stream);

        [OperationContract]
        byte[] GetBytesDocument();
    }

    
    public class DocumentService : IDocumentService
    {
        public void AddLargeDocument(Stream stream)
        {
            throw new NotImplementedException();
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
