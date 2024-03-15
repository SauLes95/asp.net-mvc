using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Main
{
    internal class MainClass
    {
        
        static Task t1 = Task.Run(() =>
        {
            Console.WriteLine($"Sleeping T1 started");
            Thread.Sleep(1000);
            Console.WriteLine($"Sleeping T1 completed");
        });

        static Task t2 = Task.Run(() =>
        {
            Console.WriteLine($"Sleeping T2 started");
            Thread.Sleep(1500);
            Console.WriteLine($"Sleeping T2 completed");
        });
        static async Task SleepF2()
        {
            Console.WriteLine($"Sleeping F2 started");
            await Task.Delay(1000);
            Console.WriteLine($"Sleeping F2 completed");
        }
        static async Task SleepF1()
        {
            Console.WriteLine($"Sleeping F1 started");
            await SleepF2();
            await Task.Delay(1000);
            Console.WriteLine($"Sleeping F1 completed");
        }

        

        static void Main(string[] args)
        {
            //Task.WaitAll(t1, t2);
            //Task.WaitAny(t1, t2);   

            //Task t3 = SleepF1();
            //t3.Wait();
        }
        
    }
}
