using BenchmarkDotNet.Attributes;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class Vector4ConstructorTests
    {
        private const int N = 10000000;

        private Vector4[] data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new Vector4[N];
        }

        [Benchmark]
        public float Vector4_EmptyConstructor()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Vector4();
                // ----- End Test
                sum += data[i].X;
            }
            return sum;
        }

        [Benchmark]
        public float Vector4_Default()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = default;
                // ----- End Test
                sum += data[i].X;
            }
            return sum;
        }

        [Benchmark]
        public float Vector4_ConstructorArgOne()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Vector4(1f);
                // ----- End Test
                sum += data[i].X;
            }
            return sum;
        }

        [Benchmark]
        public float Vector4_ConstructorArgThree()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Vector4(1f, 1f, 1f, 1f);
                // ----- End Test
                sum += data[i].X;
            }
            return sum;
        }

        [Benchmark]
        public float Vector4_Vector4One()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = Vector4.One;
                // ----- End Test
                sum += data[i].X;
            }
            return sum;
        }
    }
}