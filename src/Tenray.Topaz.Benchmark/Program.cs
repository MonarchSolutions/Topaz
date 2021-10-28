﻿using System;
using System.Diagnostics;

namespace Tenray.Topaz.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            var b = new Benchmark1();

            if (true)
            {
                sw.Start();
                b.RunTopaz();
                sw.Stop();
                Console.WriteLine("Topaz: " + sw.ElapsedMilliseconds + " ms");
            }
            else
            {
                sw.Start();
                b.RunJint();
                sw.Stop();
                Console.WriteLine("Jint: " + sw.ElapsedMilliseconds + " ms");
            }
        }
    }
}
