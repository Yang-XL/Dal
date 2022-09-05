using BenchmarkDotNet.Running;

namespace Yxl.Dal.Benchmarker.Test // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SqlWhereBuilder BenchmarkRunner");
            BenchmarkRunner.Run<SqlWhereBuilderTest>();


            Console.WriteLine("SqlDeleteBuilder BenchmarkRunner");
            BenchmarkRunner.Run<SqlDeleteBuilderTest>();

            Console.WriteLine("Press Enter Exit");
            Console.ReadLine();
        }
    }
}