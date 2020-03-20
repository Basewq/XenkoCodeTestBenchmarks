using BenchmarkDotNet.Attributes;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class VectorLengthTests
    {
        public const float ZeroToleranceSquared = MathUtil.ZeroTolerance * MathUtil.ZeroTolerance;

        private const int N = 1000000;

        private Vector2[] data2;
        private Vector3[] data3;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data2 = new Vector2[N];
            data3 = new Vector3[N];
            for (int i = 0; i < N; i++)
            {
                data2[i] = new Vector2(i / (float)N);
                data3[i] = new Vector3(i / (float)N);
            }
        }

        [Benchmark]
        public float Vector2_LengthVsZeroTolerance()
        {
            float sum = 0;
            for (int ii = 0; ii < 3; ii++)
                for (int i = 0; i < data2.Length; i++)
                {
                    // ----- Test
                    if (data2[i].Length() < MathUtil.ZeroTolerance)
                    {
                        sum += 1;
                    }
                    // ----- End Test
                }
            return sum;
        }

        [Benchmark]
        public float Vector2_LengthSquaredVsZeroToleranceSquared()
        {
            float sum = 0;
            for (int ii = 0; ii < 3; ii++)
                for (int i = 0; i < data2.Length; i++)
                {
                    // ----- Test
                    if (data2[i].LengthSquared() < ZeroToleranceSquared)
                    {
                        sum += 1;
                    }
                    // ----- End Test
                }
            return sum;
        }

        [Benchmark]
        public float Vector3_LengthVsZeroTolerance()
        {
            float sum = 0;
            for (int ii = 0; ii < 3; ii++)
                for (int i = 0; i < data3.Length; i++)
                {
                    // ----- Test
                    if (data3[i].Length() < MathUtil.ZeroTolerance)
                    {
                        sum += 1;
                    }
                    // ----- End Test
                }
            return sum;
        }

        [Benchmark]
        public float Vector3_LengthSquaredVsZeroToleranceSquared()
        {
            float sum = 0;
            for (int ii = 0; ii < 3; ii++)
                for (int i = 0; i < data3.Length; i++)
                {
                    // ----- Test
                    if (data3[i].LengthSquared() < ZeroToleranceSquared)
                    {
                        sum += 1;
                    }
                    // ----- End Test
                }
            return sum;
        }
    }
}
