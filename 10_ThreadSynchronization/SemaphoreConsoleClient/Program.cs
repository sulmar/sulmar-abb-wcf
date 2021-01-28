using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemaphoreConsoleClient
{

    // Install-Package BenchmarkDotNet
    class Program
    {
        static async Task Main(string[] args)
        {
            // Run in release mode!

            var summary = BenchmarkRunner.Run<LockBenchmarks>();

            Console.WriteLine(summary);
        }        
    }

    [RankColumn]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class LockBenchmarks
    {
        [Benchmark]
        public void LockTest()
        {
            var account = new AccountLock(initialBalance: 1000);

            var tasks = new Task[100];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => UpdateLock(account));
            }

        }

       private void UpdateLock(AccountLock account)
        {
            decimal[] amounts = { 0, 2, -3, 6, -2, -1, 8, -5, 11, -6 };

            foreach (var amount in amounts)
            {
                if (amount >= 0)
                {
                    account.Credit(amount);
                }
                else
                {
                    account.Debit(Math.Abs(amount));
                }
            }
        }

        [Benchmark]
        public void NoLockTest()
        {
            var account = new Account(initialBalance: 1000);

            var tasks = new Task[100];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => UpdateNoLock(account));
            }
        }
       
        private void UpdateNoLock(Account account)
        {
            decimal[] amounts = { 0, 2, -3, 6, -2, -1, 8, -5, 11, -6 };

            foreach (var amount in amounts)
            {
                if (amount >= 0)
                {
                    account.Credit(amount);
                }
                else
                {
                    account.Debit(Math.Abs(amount));
                }
            }
        }
    }

    public class AccountLock
    {
        private decimal balance;

        public decimal GetBalance()
        {
            lock (balanceLock)
            {
                return balance;
            }
        }

        public AccountLock(decimal initialBalance) => balance = initialBalance;

        private readonly object balanceLock = new object();

        public decimal Debit(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("The debit cannot be negative");

            decimal applitedAmount = 0;

            // critical section

            lock (balanceLock)
            {
                if (balance >= amount)
                {
                    balance -= amount;
                    applitedAmount = amount;
                }
            }

            return applitedAmount;
        }

        public void Credit(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("The debit cannot be negative");

            lock (balanceLock)
            {
                balance += amount;
            }
        }

    }

    public class Account
    {
        private decimal balance;

        public decimal GetBalance()
        {
            // lock (balanceLock)
           // {
                return balance;
            // }
        }

        public Account(decimal initialBalance) => balance = initialBalance;

        private readonly object balanceLock = new object();

        public decimal Debit(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("The debit cannot be negative");

            decimal applitedAmount = 0;

            // critical section

            //lock (balanceLock)
           // {
                if (balance >= amount)
                {
                    balance -= amount;
                    applitedAmount = amount;
                }
            // }

            return applitedAmount;
        }

        public void Credit(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("The debit cannot be negative");

            // lock (balanceLock)
            // {
                balance += amount;
            // }
        }

    }


}
