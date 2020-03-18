using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
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
            var config = DefaultConfig.Instance
                          .With(Job.Default.With(ClrRuntime.Net461))
                          .With(Job.Default.With(CoreRuntime.Core20))
                          //.With(Job.Default.With(CoreRuntime.Core30))
                          //.With(Job.Default.With(MonoRuntime.Default))
                          ;

            var options = new List<(string code, string desc, Func<Summary[]> benchmark)>();
            AddCodedOption("A", "[A]ll", () => BenchmarkRunner.Run(typeof(Program).Assembly, config));
            AddOption("Matrix tests", () => BenchmarkRunner.Run<MatrixTests>(config));
            AddOption("Canvas tests", () => BenchmarkRunner.Run<CanvasTests>(config));

            AddOption("Color3Constructor tests", () => BenchmarkRunner.Run<Color3ConstructorTests>(config));
            AddOption("Color3Copy tests", () => BenchmarkRunner.Run<Color3CopyTests>(config));

            AddOption("Vector3Constructor tests", () => BenchmarkRunner.Run<Vector3ConstructorTests>(config));
            AddOption("Vector4Constructor tests", () => BenchmarkRunner.Run<Vector4ConstructorTests>(config));

            AddOption("LightStreak tests", () => BenchmarkRunner.Run<LightStreakTests>(config));
            AddOption("ModelNestedForLoopAccess tests", () => BenchmarkRunner.Run<ModelNestedForLoopAccessTests>(config));

            AddOption("LightGroupRendererBase tests", () => BenchmarkRunner.Run<LightGroupRendererBaseTest>(config));
            AddOption("LightClusteredPointSpotGroupRenderer tests", () => BenchmarkRunner.Run<LightClusteredPointSpotGroupRendererTests>(config));
            AddOption("Light_PointLightShaderGroupData tests", () => BenchmarkRunner.Run<LightClusteredPointSpotGroupRenderer_PointLightShaderGroupDataTests>(config));
            AddOption("LightDirectionalGroupRenderer tests", () => BenchmarkRunner.Run<LightDirectionalGroupRendererTest>(config));

            AddOption("StreamingManager tests", () => BenchmarkRunner.Run<StreamingManagerTests>(config));
            AddOption("PhysicsDebugShapeMaterial tests", () => BenchmarkRunner.Run<PhysicsDebugShapeMaterialTests>(config));
            AddOption("PipelineStateDirect3D tests", () => BenchmarkRunner.Run<PipelineStateDirect3DTests>(config));

            AddOption("Grid tests", () => BenchmarkRunner.Run<GridTests>(config));

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine($"Select your benchmark:");
                Console.WriteLine(string.Join("\r\n", options.Select(x => x.desc)));

                string userInput = Console.ReadLine();
                var selectedOption = options.Find(x => x.code.Equals(userInput, StringComparison.OrdinalIgnoreCase));
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
