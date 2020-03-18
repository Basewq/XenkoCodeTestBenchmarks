using BenchmarkDotNet.Attributes;
using Xenko.Core.Mathematics;
using XenkoCodeTestBenchmarks.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class BoundingFrustumTests
    {
        private const int N = 10000000;

        private Matrix[] matrices;
        private BoundingFrustumOrig[] dataOrig;
        private BoundingFrustumAssignThenNormalize[] dataAssignThenNorm;
        private BoundingFrustumNormalizeImmediate[] dataNormImm;

        [GlobalSetup]
        public void GlobalSetup()
        {
            matrices = new Matrix[N];
            dataOrig = new BoundingFrustumOrig[N];
            dataAssignThenNorm = new BoundingFrustumAssignThenNormalize[N];
            dataNormImm = new BoundingFrustumNormalizeImmediate[N];
        }

        [Benchmark]
        public float BoundingFrustum_ConstructorOriginal()
        {
            float sum = 0;
            for (int i = 0; i < dataOrig.Length; i++)
            {
                // ----- Test
                dataOrig[i] = new BoundingFrustumOrig(ref matrices[i]);
                // ----- End Test
                sum += dataOrig[i].NearPlane.Normal.X;
            }
            return sum;
        }

        [Benchmark]
        public float BoundingFrustum_ConstructorAssignThenNormalize()
        {
            float sum = 0;
            for (int i = 0; i < dataAssignThenNorm.Length; i++)
            {
                // ----- Test
                dataAssignThenNorm[i] = new BoundingFrustumAssignThenNormalize(ref matrices[i]);
                // ----- End Test
                sum += dataAssignThenNorm[i].NearPlane.Normal.X;
            }
            return sum;
        }

        [Benchmark]
        public float BoundingFrustum_ConstructorNormalizeImmediate()
        {
            float sum = 0;
            for (int i = 0; i < dataNormImm.Length; i++)
            {
                // ----- Test
                dataNormImm[i] = new BoundingFrustumNormalizeImmediate(ref matrices[i]);
                // ----- End Test
                sum += dataNormImm[i].NearPlane.Normal.X;
            }
            return sum;
        }
    }
}
