using ABB.WCF.SerializationConsoleClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ABB.WCF.SerializationConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            // WCF DataContractSerializer

            Customer customer = new Customer
            {
                Id = 1,
                FirstName = "Marcin",
                LastName = "Sulecki"
            };

            DataContractSerializer serializer = new DataContractSerializer(typeof(Customer));
            MemoryStream stream = new MemoryStream();

            // Serialization
            serializer.WriteObject(stream, customer);

            stream.Position = 0;

            string xml = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(xml);
          
            // Deserialization
            stream.Position = 0;            

            var deserializedCustomer = (Customer) serializer.ReadObject(stream);

            Console.WriteLine($"{deserializedCustomer.Id} {deserializedCustomer.FirstName} {deserializedCustomer.LastName}");

           



        }
    }
}
