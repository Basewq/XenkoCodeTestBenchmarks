using BenchmarkDotNet.Attributes;
using XenkoCodeTestBenchmarks.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class MatrixTests2
    {
        private const int N = 10000000;

        private MatrixDataTest[] data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new MatrixDataTest[N];
        }

        [IterationSetup]
        public void IterationSetup()
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new MatrixDataTest
                {
                    M11 = 11,
                    M21 = 21,
                    M31 = 31,
                    M41 = 41,

                    M12 = 12,
                    M22 = 22,
                    M32 = 32,
                    M42 = 42,

                    M13 = 13,
                    M23 = 23,
                    M33 = 33,
                    M43 = 43,

                    M14 = 14,
                    M24 = 24,
                    M34 = 34,
                    M44 = 44,
                };
            }
        }

        [Benchmark]
        public float Transpose_SingleTempVariableSwap()
        {
            float sum = 0;
            for (int ii = 0; ii < 3; ii++)
                for (int i = 0; i < data.Length; i++)
                {
                    // ----- Test
                    data[i].TransposeOrig();
                    // ----- End Test
                    sum += data[i].M11;
                }
            return sum;
        }

        [Benchmark]
        public float Transpose_RefSwap()
        {
            float sum = 0;
            for (int ii = 0; ii < 3; ii++)
                for (int i = 0; i < data.Length; i++)
                {
                    // ----- Test
                    data[i].TransposeRefSwap();
                    // ----- End Test
                    sum += data[i].M11;
                }
            return sum;
        }
    }
}
