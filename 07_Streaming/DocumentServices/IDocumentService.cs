using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [OperationContract]
        void Add(Document document);
        [OperationContract] 
        Document Get();
    }


    [MessageContract]
    public class Document
    {
        [MessageHeader]
        public string Author { get; set; }
        [MessageHeader]
        public string Name { get; set; }
        [MessageHeader]
        public string Description { get; set; }
        [MessageBodyMember]
        public Stream Content { get; set; }
    }

    
    public class DocumentService : IDocumentService
    {
        public void Add(Document document)
        {
            Trace.WriteLine($"{document.Author} {document.Description}");

            AddLargeDocument(document.Content);
        }

        public void AddLargeDocument(Stream stream)
        {
            using (stream)
            using (FileStream fileStream = File.Create("uploaded.jpg"))
            {
                stream.CopyTo(fileStream);
            }
        }

        public Document Get()
        {
            Stream stream = GetLargeDocument();

            Document document = new Document { Author = "Marcin", Description = "Lorem ipsum", Content = stream };

            return document;
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
