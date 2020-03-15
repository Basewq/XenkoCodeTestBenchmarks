using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace XenkoCodeTestBenchmarks
{
    public class StreamingManagerTests
    {
        private const int N = 500000;
        private const int DefaultResourceObjectCount = 8;
        private List<FakeStreamableResource>[] activeStreamings;

        [GlobalSetup]
        public void GlobalSetup()
        {
            activeStreamings = new List<FakeStreamableResource>[N];
            for (int i = 0; i < N; i++)
            {
                activeStreamings[i] = new List<FakeStreamableResource>(DefaultResourceObjectCount);
            }
        }

        [IterationSetup]
        public void IterationSetup()
        {
            for (int i = 0; i < activeStreamings.Length; i++)
            {
                var list = activeStreamings[i];
                list.Clear();
                for (int j = 0; j < DefaultResourceObjectCount; j++)
                {
                    list.Add(new FakeStreamableResource());
                }
            }
        }

        [Benchmark]
        public float FlushSync_RemoveAtForward()
        {
            float sum = 0;
            for (int ii = 0; ii < activeStreamings.Length; ii++)
            {
                var activeStreaming = activeStreamings[ii];
                // ----- Test
                for (int i = 0; i < activeStreaming.Count; i++)
                {
                    var resource = activeStreaming[i];
                    if (resource.IsTaskActive)
                        continue;

                    resource.FlushSync();
                    activeStreaming.RemoveAt(i);
                    i--;
                }
                // ----- End Test
                sum += activeStreaming.Count;
            }
            return sum;
        }

        [Benchmark]
        public float FlushSync_RemoveAtReverse()
        {
            float sum = 0;
            for (int ii = 0; ii < activeStreamings.Length; ii++)
            {
                var activeStreaming = activeStreamings[ii];
                // ----- Test
                for (int i = activeStreaming.Count - 1; i >= 0; i--)
                {
                    var resource = activeStreaming[i];
                    if (resource.IsTaskActive)
                        continue;

                    resource.FlushSync();
                    activeStreaming.RemoveAt(i);
                }
                // ----- End Test
                sum += activeStreaming.Count;
            }
            return sum;
        }

        private abstract class FakeStreamableResourceBase
        {
            internal virtual bool IsTaskActive => false;

            internal virtual void FlushSync()
            {
            }

            internal int Value = 1;
        }

        private class FakeStreamableResource : FakeStreamableResourceBase
        {
            internal override bool IsTaskActive => false;
        }
    }
}
