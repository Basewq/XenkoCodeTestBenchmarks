using BenchmarkDotNet.Attributes;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class MatrixTests
    {
        private const int N = 1000000;

        private Matrix[] data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new Matrix[N];
        }

        [Benchmark]
        public float Invert_AssignThenInvert()
        {
            float sum = 0;
            for (int i = 0; i < data.Length - 1; i++)
            {
                var matrixInvert = data[i];
                // ----- Test
                //var matrixInvert = matrix;
                matrixInvert.Invert();
                // ----- End Test
                sum += matrixInvert.M11;
            }
            return sum;
        }

        [Benchmark]
        public float Invert_ByValAndReturn()
        {
            float sum = 0;
            for (int i = 0; i < data.Length - 1; i++)
            {
                //var matrix = data[i];
                // ----- Test
                var matrixInvert = Matrix.Invert(data[i]);
                // ----- End Test
                sum += matrixInvert.M11;
            }
            return sum;
        }

        [Benchmark]
        public float Invert_RefAndOut()
        {
            float sum = 0;
            for (int i = 0; i < data.Length - 1; i++)
            {
                //var matrix = data[i];
                // ----- Test
                Matrix.Invert(ref data[i], out var matrixInvert);
                // ----- End Test
                sum += matrixInvert.M11;
            }
            return sum;
        }

        [Benchmark]
        public float Orthogonalize_NoTempVariable()
        {
            float sum = 0;
            for (int i = 0; i < data.Length - 1; i++)
            {
                var result = data[i];
                // ----- Test
                result.Row2 = result.Row2 - (Vector4.Dot(result.Row1, result.Row2) / Vector4.Dot(result.Row1, result.Row1)) * result.Row1;

                result.Row3 = result.Row3 - (Vector4.Dot(result.Row1, result.Row3) / Vector4.Dot(result.Row1, result.Row1)) * result.Row1;
                result.Row3 = result.Row3 - (Vector4.Dot(result.Row2, result.Row3) / Vector4.Dot(result.Row2, result.Row2)) * result.Row2;

                result.Row4 = result.Row4 - (Vector4.Dot(result.Row1, result.Row4) / Vector4.Dot(result.Row1, result.Row1)) * result.Row1;
                result.Row4 = result.Row4 - (Vector4.Dot(result.Row2, result.Row4) / Vector4.Dot(result.Row2, result.Row2)) * result.Row2;
                result.Row4 = result.Row4 - (Vector4.Dot(result.Row3, result.Row4) / Vector4.Dot(result.Row3, result.Row3)) * result.Row3;
                // ----- End Test
                sum += result.M11;
            }
            return sum;
        }

        [Benchmark]
        public float Orthogonalize_UseTempVariable()
        {
            float sum = 0;
            for (int i = 0; i < data.Length - 1; i++)
            {
                var result = data[i];
                // ----- Test
                var row1 = result.Row1;
                var row2 = result.Row2;
                var row3 = result.Row3;
                var row4 = result.Row4;

                row2 = row2 - (Vector4.Dot(row1, row2) / Vector4.Dot(row1, row1)) * row1;

                row3 = row3 - (Vector4.Dot(row1, row3) / Vector4.Dot(row1, row1)) * row1;
                row3 = row3 - (Vector4.Dot(row2, row3) / Vector4.Dot(row2, row2)) * row2;

                row4 = row4 - (Vector4.Dot(row1, row4) / Vector4.Dot(row1, row1)) * row1;
                row4 = row4 - (Vector4.Dot(row2, row4) / Vector4.Dot(row2, row2)) * row2;
                row4 = row4 - (Vector4.Dot(row3, row4) / Vector4.Dot(row3, row3)) * row3;

                result.Row2 = row2;
                result.Row3 = row3;
                result.Row4 = row4;
                // ----- End Test
                sum += result.M11;
            }
            return sum;
        }

    }
}
