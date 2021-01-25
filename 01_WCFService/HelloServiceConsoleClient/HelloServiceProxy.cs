using HelloServiceConsoleClient.ABBServiceReference;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HelloServiceConsoleClient
{
    public class HelloServiceProxy : ClientBase<IService>, IService
    {
        public HelloServiceProxy(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
        }

        public decimal Calculate(decimal amount)
        {
            throw new NotImplementedException();
        }

        public string Ping(string message)
        {
            return this.Channel.Ping(message);
        }

        public async Task<string> PingAsync(string message)
        {
            Trace.WriteLine($"Request: {message}");

            string response = await this.Channel.PingAsync(message);

            Trace.WriteLine($"Response: {response}");

            return response;
        }

        public void Send(string content)
        {
            this.Channel.Send(content);
        }

        public Task SendAsync(string content)
        {
            Trace.WriteLine(content);            

            return this.Channel.SendAsync(content);
        }
    }
}
