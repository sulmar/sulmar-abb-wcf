﻿using CalculatorServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri uri = new Uri(ConfigurationManager.AppSettings["CalculatorServiceUrl"]);

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(uri);

            ChannelFactory<ICalculatorService> proxy = new ChannelFactory<ICalculatorService>(binding, endpoint);

            ICalculatorService calculatorService = new CalculatorService();

            calculatorService.Add(1, 2);
            calculatorService.Add(6, 4);
            calculatorService.Add(2, 2);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
