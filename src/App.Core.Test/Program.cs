using System;
using System.Collections.Generic;
using System.Diagnostics;
using Masuit.Tools;
using Masuit.Tools.Core;
using Masuit.Tools.Strings;
using Masuit.Tools.Systems;

namespace App.Core.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var set1 = new HashSet<string>();
            double time1 = HiPerfTimer.Execute(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    set1.Add(Stopwatch.GetTimestamp().ToBinary(36));
                }
            });
            Console.WriteLine(set1.Count == 1000000);//True
            Console.WriteLine("产生100w个id耗时" + time1 + "s");//1.6639039s

            var set = new HashSet<long>();
            double time = HiPerfTimer.Execute(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    SnowFlake.SetNumberFormater(new NumberFormater(10));
                    var ss = new SnowFlake(31,31).GetLongId();
                    Console.WriteLine(ss);
                    set.Add(ss);
                }
            });
            Console.WriteLine(set.Count == 1000000); //True
            Console.WriteLine("产生100w个id耗时" + time + "s"); //2.6891495s

            //Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
