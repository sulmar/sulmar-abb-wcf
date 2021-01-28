using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Semaphor2ConsoleClient
{
    class Program
    {
        static Semaphore semaphore = new Semaphore(initialCount: 3, maximumCount: 3);

        static Printer printer = new Printer();

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(DoSomeTask)
                {
                    Name = $"Thread {i}"
                };

                thread.Start();
            }

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

        }

        static void DoSomeTask()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} waiting...");

            try
            {
                semaphore.WaitOne();

                // critial section
                printer.Print("Hello");
            }
            finally
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} release.");
                semaphore.Release();
            }
        }

    }


    class Printer
    {
        public void Print(string content)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} printing {content}");
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }
    }
}
