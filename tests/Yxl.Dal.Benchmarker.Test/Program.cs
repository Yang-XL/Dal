using BenchmarkDotNet.Running;
using System;
using Yxl.Dal.Benchmarker.Test;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Being");
            var summary = BenchmarkRunner.Run<SqlWhereBuilderTest>();
            Console.WriteLine("Press Enter Exit");
            Console.ReadLine();
        }
    }
}