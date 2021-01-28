using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphorSlimConsoleClient
{
    public class HappyNumbers
    {
        public IEnumerable<int> GetHappyNumbers()
        {
            yield return 10;
            yield return 20;
            yield return 30;
            yield return 40;
            yield return 50;           
        }

        Random random = new Random();

        public IEnumerable<int> GetMeasures()
        {
            while(true)
            {
                yield return random.Next(0, 100);
            }
        }
    }

    public class Measure
    {
        public float Value { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static SemaphoreSlim semaphore = new SemaphoreSlim(3);

        static void Main(string[] args)
        {
            // LazyCollectionTest();

            Task.WaitAll(CallOtherApi().ToArray());

            Console.ReadLine();
        }

        private static void LazyCollectionTest()
        {
            // IEnumerable<Measure> measures = new Faker<Measure>().RuleFor(p => p.Value, f => f.Random.Float()).GenerateLazy(1000);

            IEnumerable<Measure> measures = new Faker<Measure>().RuleFor(p => p.Value, f => f.Random.Float()).GenerateForever();

            foreach (var measure in measures)
            {
                Console.WriteLine(measure);

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }


            HappyNumbers happyNumbers = new HappyNumbers();

            foreach (var measure in happyNumbers.GetMeasures())
            {
                Console.WriteLine(measure);

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }




            IEnumerable<int> numbers = happyNumbers.GetHappyNumbers();

            foreach (var number in numbers)
            {
                Console.WriteLine(number);

                if (number < 4)
                {
                    return;
                }
            }
        }




        public static IEnumerable<Task> CallOtherApi()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return CallApi();
            }
        }



        public static async Task CallApi()
        {
            try
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Waiting...");

                await semaphore.WaitAsync();

                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} downloading...");

                var response = await client.GetAsync("http://www.google.com");

                semaphore.Release();

                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} released.");

                Console.WriteLine(response.StatusCode);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
