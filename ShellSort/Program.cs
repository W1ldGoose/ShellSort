using System;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

namespace ShellSort
{
    public class Program
    {
        

        public static void FillArray(int[] unsortedArray)
        {
            Random rand = new Random();
            for (int i = 0; i < unsortedArray.Length; i++)
            {
                unsortedArray[i] = rand.Next(0, 1000000);
            }
        }
        //public static int

        [MemoryDiagnoser()]
        [Orderer(SummaryOrderPolicy.FastestToSlowest)]
        [RankColumn()]
        [RPlotExporter]
        [SimpleJob(RunStrategy.Throughput)]
        public class BenchmarkTest
        {
            [Params(10000, 1000000, 10000000, 100000000)] public static int elemsCount = 100;
            public static int[] unsortedArray = new int[elemsCount];


            public ShellSort shellSort = new ShellSort();

            [Benchmark]
            public void ParallelTest()
            {
                FillArray(unsortedArray);
                shellSort.ParallelSort(ref unsortedArray);
                // Console.WriteLine("Отсортированный массив: {0}", string.Join(", ", unsortedArray));
            }

            [Benchmark]
            public void SerialTest()
            {
                FillArray(unsortedArray);
                SerialSort.ShellSort(ref unsortedArray);
                // Console.WriteLine("Отсортированный массив: {0}", string.Join(", ", unsortedArray));
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Сортировка Шелла");

            var summary = BenchmarkRunner.Run<BenchmarkTest>();
        }
    }
}