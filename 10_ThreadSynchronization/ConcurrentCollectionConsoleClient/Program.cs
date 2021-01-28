using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollectionConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Problem();

            // Solution();

            // QueueProblem();

            BlockingCollectionSolution();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            // Dictionary -> ConcurrentDictionary
            // Queue -> ConcurrentQueue
            // Stack -> ConcurrentStack
            // List -> ConcurrentBag


            // BlockingCollection
        }

        public static void BlockingCollectionSolution()
        {
            BlockingCollection<int> collection = new BlockingCollection<int>(boundedCapacity: 30);

            Console.CancelKeyPress += (s, e) =>
            {
                collection.CompleteAdding();
                e.Cancel = true;
            };

            // Producers
            Task task1 = Task.Run(() => Produce(collection));
            Task task2 = Task.Run(() => Produce(collection));

            // Consumers
            Task task3 = Task.Run(() => Consume(collection));
        }

        static Random random = new Random();

        private static void Produce(BlockingCollection<int> collection)
        {            
            
            while(!collection.IsAddingCompleted)
            {
                int i = random.Next(0, 100);

                collection.Add(i);

                Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Producent  {i}");

                Thread.Sleep(TimeSpan.FromMilliseconds(200));

            }

            // collection.CompleteAdding();


        }


        private static void Consume(BlockingCollection<int> collection)
        {
            foreach (var item in collection.GetConsumingEnumerable())
            {
                Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Consume: {item}");
            }

            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Finished (EOF).");
        }


        public static void Problem()
        {
            IDictionary<string, int> dictionary = new Dictionary<string, int>();

            Task.Run(() => dictionary.Add("A", 1));
            Task.Run(() => dictionary.Remove("A"));
            Task.Run(() => dictionary.Add("A", 2));

            // dictionary.Dump();
        }

        public static void QueueProblem()
        {
            Queue<string> queue = new Queue<string>();

            Task.Run(() => queue.Enqueue("A"));
            Task.Run(() => queue.Dequeue());
            Task.Run(() => queue.Enqueue("A"));

            // dictionary.Dump();
        }


        public static void ConcurrentQueueSolution()
        {
            ConcurrentQueue<string> queue = new ConcurrentQueue<string>();

            Task.Run(() => queue.Enqueue("A"));

            Task.Run(()=>queue.TryDequeue(out string result));

            Task.Run(() => queue.Enqueue("B"));
        }



        public static void Solution()
        {
            ConcurrentDictionary<string, int> dictionary = new ConcurrentDictionary<string, int>();

            Task t1 = Task.Run(() =>
            {
                bool result = dictionary.TryAdd("A", 1);
                Console.WriteLine($"Try Add #1 {result}");
            });

            Task t2 = Task.Run(() =>
            {
                bool result = dictionary.TryRemove("A", out int value);
                Console.WriteLine($"Try Remove {result}");
            });

            Task t3 = Task.Run(() =>
            {
                bool result = false;

                do
                {
                    Console.WriteLine($"Trying add...");

                    result = dictionary.TryAdd("A", 2);
                    Console.WriteLine($"Try Add #2 {result}");

                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                while (result);
            });

            Task.WaitAll(t1, t2, t3);

            dictionary.Dump();
        }
    }

    public static class DictionaryExtensions
    {
        public static void Dump(this IDictionary<string, int> dictionary)
        {
            foreach (var item in dictionary)
            {
                Console.WriteLine($"{item.Key} {item.Value}");
            }
        }
    }
}
