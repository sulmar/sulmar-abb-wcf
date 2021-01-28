using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TasksConsoleClient
{


    class Program
    {
        // C# 7.0
        // public void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();


        //static async Task MainAsync(string[] args)
        //{

        //}

        // C# >= 7.1
        static async Task Main(string[] args)
        {
            Sender sender = new Sender();

            IProgress<int> progress = new Progress<int>(copy => Console.WriteLine($"Printing copy of {copy}"));

            await sender.PrintAsync("Hello", 5, progress);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        //static public void Main(string[] args)
        //{
        //    Sender sender = new Sender();

        //    sender.GetLog();

        //    Console.WriteLine("Press Enter to exit.");
        //    Console.ReadLine();
        //}
    }


    public static class TaskExtensions
    {
        public static TResult MyWait<TResult>(this Task<TResult> task)
        {
            try
            {
                task.Wait();
            }
            catch (Exception e)
            {

            }

            return task.Result;

        }
    }

    public class Sender
    {
        public Task<int> PrintAsync(string content, int copies, IProgress<int> progress = null)
        {
            // Task.Delay(TimeSpan.FromSeconds(5))

            return Task.Run(() => Print(content, copies, progress));
        }

        public int Print(string content, int copies, IProgress<int> progress = null)
        {
            for (int i = 0; i < copies; i++)
            {
                // Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Printing... {content}  copy of {i} ");

                progress?.Report(i);

                Thread.Sleep(TimeSpan.FromSeconds(5));

                if (i > 3)
                {
                    throw new ApplicationException();
                }

                // Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Printed copy of {i}.");
            }

            return copies;
        }

        public decimal Calculate(decimal amount, decimal tax)
        {
            return amount + tax;
        }

        public Task<decimal> CalculateAsync(decimal amount, decimal tax)
        {
            // return Task.Run(() => Calculate(amount, tax));

            return Task.FromResult(Calculate(amount, tax));
        }

        public void DoWork()
        {
            decimal result = CalculateAsync(100, 4).Result;
        }

        internal int GetLog()
        {
            Task<int> task = Task.Run<int>(async () => await PrintAsync("Hello", 5));

            try
            {
                task.Wait();
            }
            catch(Exception e)
            {

            }
            
            return task.Result;
        }


        //public async Task<LogEntity> GetLogAsync()
        //{​​​​​
        //    var result = await _logger.GetAsync();
        //    // more code here...
        //    return result as LogEntity;
        //}​​​​​


        //public Task DoWorkAsync()
        //{
        //    DoWork();

        //    return Task.CompletedTask;
        //}

    }




}
