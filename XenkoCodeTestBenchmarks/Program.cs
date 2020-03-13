using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XenkoCodeTestBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new List<(string code, string desc, Func<Summary[]> benchmark)>();
            AddCodedOption("A", "[A]ll", () => BenchmarkRunner.Run(typeof(Program).Assembly));
            AddOption("Matrix tests", () => BenchmarkRunner.Run<MatrixTests>());
            AddOption("Canvas tests", () => BenchmarkRunner.Run<CanvasTests>());
            AddOption("Color3Copy tests", () => BenchmarkRunner.Run<Color3CopyTests>());
            AddOption("LightStreak tests", () => BenchmarkRunner.Run<LightStreakTests>());
            AddOption("ModelNestedForLoopAccess tests", () => BenchmarkRunner.Run<ModelNestedForLoopAccessTests>());

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine($"Select your benchmark:");
                Console.WriteLine(string.Join("\r\n", options.Select(x => x.desc)));

                string userInput = Console.ReadLine();
                var selectedOption = options.FirstOrDefault(x => x.code.Equals(userInput, StringComparison.OrdinalIgnoreCase));
                if (selectedOption.benchmark == null)
                {
                    Console.WriteLine($"Invalid command.");
                }
                else
                {
                    var summaries = selectedOption.benchmark();
                    Console.WriteLine($"Benchmark result(s) stored at:");
                    Console.WriteLine(string.Join("\r\n", summaries.Select(x => x.LogFilePath)));

                    isRunning = false;
                }
            }

            void AddCodedOption(string code, string desc, Func<Summary[]> benchmark)
            {
                var opt = (code, desc, benchmark);
                options.Add(opt);
            }
            void AddOption(string desc, Func<Summary> benchmark)
            {
                string code = options.Count.ToString();
                Func<Summary[]> bmFunc = () => new Summary[] { benchmark() };
                var opt = (code, $"[{code}] {desc}", bmFunc);
                options.Add(opt);
            }
        }
    }
}
