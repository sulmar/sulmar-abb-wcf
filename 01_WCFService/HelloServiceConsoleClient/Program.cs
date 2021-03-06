﻿using IServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HelloServiceConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Autogenerated Proxy class
            // await AutoProxyTest();

            // Proxy class with using ClientBase<IService>
            //  await ProxyTest();

           // await ChannelFactoryTest();

            await ChannelFactoryConfigureFromCodeTest();

            Console.ReadLine();


        }

        private static async Task ChannelFactoryTest()
        {
            string endpointConfigurationName = "BasicHttpBinding_IServiceAsync";

            ChannelFactory<IServiceAsync> proxy = new ChannelFactory<IServiceAsync>(endpointConfigurationName);

            IServiceAsync helloService = proxy.CreateChannel();

            await helloService.SendAsync("Hello World!");

            string result = await helloService.PingAsync("Hello again!");

            Console.WriteLine(result);

        }

        private static async Task ChannelFactoryConfigureFromCodeTest()
        {            
            Uri uri = new Uri(ConfigurationManager.AppSettings["HelloServiceUrl"]);

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(uri);

            ChannelFactory<IServiceAsync> proxy = new ChannelFactory<IServiceAsync>(binding, endpoint);

            IServiceAsync helloService = proxy.CreateChannel();

            await helloService.SendAsync("Hello World!");

            string result = await helloService.PingAsync("Hello again!");

            Console.WriteLine(result);

        }

        private static async Task ProxyTest()
        {
            string endpointConfigurationName = "NetTcpBinding_IService";

            HelloServiceProxy client = new HelloServiceProxy(endpointConfigurationName);

            await client.SendAsync("Hello World!");

            string result = await client.PingAsync("Hello again!");

            Console.WriteLine(result);
        }



        private static async Task AutoProxyTest()
        {
            string endpointConfigurationName = "NetTcpBinding_IService";


            // Proxy (design-pattern)
            ABBServiceReference.ServiceClient client = new ABBServiceReference.ServiceClient(endpointConfigurationName);

            await client.SendAsync("Hello World!");

            string result = await client.PingAsync("Hello again!");

            Console.WriteLine(result);
        }
    }
}
