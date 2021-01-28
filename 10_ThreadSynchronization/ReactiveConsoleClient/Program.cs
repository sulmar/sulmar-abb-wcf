using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReactiveConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var cpuCounter1 = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            var cpuCounter2 = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            var cpuCounter3 = new PerformanceCounter("Processor", "% Processor Time", "_Total");


            // Install-Package System.Reactive              

            var source1 = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(_ => cpuCounter1.NextValue());

            var source2 = Observable.Interval(TimeSpan.FromSeconds(1))
               .Select(_ => cpuCounter2.NextValue());

            var source3 = Observable.Interval(TimeSpan.FromSeconds(1))
               .Select(_ => cpuCounter3.NextValue());

            var source = Observable.Merge(source1, source2, source3);

            source
                .Subscribe(cpu => Console.WriteLine($"CPU {cpu}%"));

            source
                .Where(cpu => cpu > 70)
                // .Buffer(TimeSpan.FromMinutes(1))
                .Window(TimeSpan.FromMinutes(1))
                .Subscribe(cpu =>
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ALERT {cpu}%");
                    Console.ResetColor();
                });


            source
                .Where(cpu => cpu < 20)
                .Subscribe(cpu =>
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ALERT {cpu}%");
                    Console.ResetColor();
                });


            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();


            //while (true)
            //{
            //    var cpu = cpuCounter.NextValue();

            //    Console.WriteLine($"CPU {cpu}%");

            //    if (cpu > 70)
            //    {
            //        Console.BackgroundColor = ConsoleColor.Red;
            //        Console.WriteLine($"ALERT {cpu}%");
            //        Console.ResetColor();
            //    }


            //    if (cpu < 20)
            //    {
            //        Console.BackgroundColor = ConsoleColor.Green;
            //        Console.WriteLine($"ALERT {cpu}%");
            //        Console.ResetColor();
            //    }


            //    Thread.Sleep(TimeSpan.FromSeconds(1));
            //}



        }
    }
}
