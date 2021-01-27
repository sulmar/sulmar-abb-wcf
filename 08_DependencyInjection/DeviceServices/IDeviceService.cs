using Bogus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DeviceServices
{
    [ServiceContract]
    public interface IDeviceService
    {
        [OperationContract]
        IEnumerable<Device> Get();
        [OperationContract] 
        Device GetById(int id);
        [OperationContract] 
        void Add(Device device);
    }

    public interface IDeviceRepository
    {
        IEnumerable<Device> Get();
        Device Get(int id);
        void Add(Device device);
    }

    public class DbDeviceRepository : IDeviceRepository
    {
        public void Add(Device device)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Device> Get()
        {
            throw new NotImplementedException();
        }

        public Device Get(int id)
        {
            throw new NotImplementedException();
        }
    }


    public class DeviceFaker : Faker<Device>
    {
        public DeviceFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Hacker.Phrase());
            RuleFor(p => p.MacAddress, f => f.Internet.Mac());
        }
    }

    public class FakeDeviceRepository : IDeviceRepository
    {
        private readonly ICollection<Device> devices = new List<Device>();

        public FakeDeviceRepository(Faker<Device> faker)
        {
            devices = faker.Generate(100);
        }
        
        public void Add(Device device)
        {
            devices.Add(device);
        }

        public IEnumerable<Device> Get()
        {
            return devices;
        }

        public Device Get(int id)
        {
            return devices.SingleOrDefault(d => d.Id == id);
        }
    }

    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository repository;

        public DeviceService(IDeviceRepository repository)
        {
            this.repository = repository;
        }

        public void Add(Device device)
        {
            repository.Add(device);

            Trace.WriteLine($"Added {device.Name}");
        }

        public IEnumerable<Device> Get()
        {
            if (OperationContext.Current.IncomingMessageHeaders.FindHeader("secret-key", string.Empty) != -1)
            {
                string secretKey = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("secret-key", string.Empty);

                if (!string.IsNullOrEmpty(secretKey))
                {
                    Trace.WriteLine($"Secret-Key {secretKey}");

                    if (secretKey == "12345")
                    {
                        return repository.Get();
                    }
                }
            }

            throw new FaultException<string>("Invalid secret-key", new FaultReason("Invalid secret-key"));


        }

        public Device GetById(int id)
        {
            return repository.Get(id);
        }
    }

    public class Device
    {
        public int Id { get; set; }
        public string MacAddress { get; set; }
        public string Name { get; set; }
    }
}
