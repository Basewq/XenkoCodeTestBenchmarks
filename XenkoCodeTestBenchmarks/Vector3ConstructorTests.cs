using BenchmarkDotNet.Attributes;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class Vector3ConstructorTests
    {
        private const int N = 10000000;

        private Vector3[] data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new Vector3[N];
        }

        [Benchmark]
        public float Vector3_EmptyConstructor()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Vector3();
                // ----- End Test
                sum += data[i].X;
            }
            return sum;
        }

        [Benchmark]
        public float Vector3_Default()
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
        public float Vector3_ConstructorArgOne()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Vector3(1f);
                // ----- End Test
                sum += data[i].X;
            }
            return sum;
        }

        [Benchmark]
        public float Vector3_ConstructorArgThree()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Vector3(1f, 1f, 1f);
                // ----- End Test
                sum += data[i].X;
            }
            return sum;
        }

        [Benchmark]
        public float Vector3_Vector3One()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = Vector3.One;
                // ----- End Test
                sum += data[i].X;
            }
            return sum;
        }
    }
}