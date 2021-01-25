using IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeHelloServices
{
    public class FakeHelloService : IService
    {
        public decimal Calculate(decimal amount)
        {
            return amount * 1.23m;
        }

        public string Ping(string message)
        {
            return $"Hello {message}";
        }

        public Task<string> PingAsync(string message)
        {
            return Task.Run(() => Ping(message));
        }

        public void Send(string content)
        {
            Trace.WriteLine(content);
        }

        public Task SendAsync(string content)
        {
            return Task.Run(() => Send(content));
        }
    }
}
