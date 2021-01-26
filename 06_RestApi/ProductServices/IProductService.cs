using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ProductServices
{

    // add-reference System.ServiceModel.Web
    [ServiceContract(Namespace = "http://abb.com")]
    public interface IProductService
    {
        // GET api/products
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, UriTemplate = "api/products")]
        IEnumerable<Product> Get();

        // GET api/products/{id}
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, UriTemplate = "api/products/{id}")]
        Product GetById(string id);

        // GET api/products?from={from}&to={to}
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, UriTemplate = "api/products?from={from}&to={to}")]
        IEnumerable<Product> GetByPrice(decimal from, decimal to);

        // POST api/products
        [OperationContract]
        [WebInvoke(
            Method = "POST", 
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Bare, 
            UriTemplate = "api/products") ]
        void Add(Product product);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "api/products/{id}")]
        void Update(string id, Product product);

        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "api/products/{id}")]
        void Remove(string id);

    }


    // Install-Package Bogus

    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.UnitPrice, f => Math.Round(f.Random.Decimal(1, 100), 2));
        }
    }

    // https://docs.microsoft.com/pl-pl/dotnet/api/system.servicemodel.instancecontextmode?view=netframework-4.8#System_ServiceModel_InstanceContextMode_Single

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ProductService : IProductService
    {
        private readonly ICollection<Product> products;

        //private static object syncLock = new object();

        //private static ProductService _instance;
        //public static ProductService Instance
        //{
        //    get
        //    {
        //        lock (syncLock)
        //        {
        //            if (_instance == null)
        //            {
        //                _instance = new ProductService();
        //            }
        //        }

        //        return _instance;
        //    }

        //}

        public ProductService()
        {
            ProductFaker faker = new ProductFaker();

            products = faker.Generate(100);
        }

        public void Add(Product product)
        {
            products.Add(product);
        }

        public IEnumerable<Product> Get()
        {
            return products;
        }

        public Product GetById(string id)
        {
            int productId = int.Parse(id);

            return products.SingleOrDefault(p => p.Id == productId);
        }

        public IEnumerable<Product> GetByPrice(decimal from, decimal to)
        {
            return products.Where(p => p.UnitPrice >= from && p.UnitPrice <= to).ToList();
        }

        public void Remove(string id)
        {
            products.Remove(GetById(id));
        }

        public void Update(string id, Product product)
        {
            Product existsproduct = GetById(id);
            existsproduct.Name = product.Name;
            existsproduct.UnitPrice = product.UnitPrice;
        }

        // JSON Patch
        //   contentType: 'application/merge-patch+json
        // { "color: "red", "unitprice": 100 } 
        public void Patch(int id, Product product)
        {
            throw new NotSupportedException();           
        }

        public decimal GetSalary()
        {
            int amount = 1000;

            throw new NotImplementedException();

#if DEBUG
            Console.WriteLine("XXXX");
#endif
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
