using BenchmarkDotNet.Attributes;
using Xenko.Rendering.Lights;

namespace XenkoCodeTestBenchmarks
{
    public class LightGroupRendererBaseTest
    {
        private const int N = 1000000;

        private LightGroupRendererBase.ProcessLightsParameters[] data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new LightGroupRendererBase.ProcessLightsParameters[N];
        }

        [Benchmark]
        public int ProcessLightsParameters_PassByValue()
        {
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                sum += ReadParam(data[i]);
                // ----- End Test
            }
            return sum;

            int ReadParam(LightGroupRendererBase.ProcessLightsParameters parameters)
            {
                return parameters.ViewIndex;
            }
        }

        [Benchmark]
        public float ProcessLightsParameters_PassByRef()
        {
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                sum += ReadParam(ref data[i]);
                // ----- End Test
            }
            return sum;

            int ReadParam(ref LightGroupRendererBase.ProcessLightsParameters parameters)
            {
                return parameters.ViewIndex;
            }
        }
    }
}
