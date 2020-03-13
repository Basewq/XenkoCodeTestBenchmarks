using BenchmarkDotNet.Attributes;
using System;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class CanvasTests
    {
        private const int N = 1000000;

        private Vector3[] relativePositions;
        private Vector3[] absolutePositions;
        private bool[] useAbsolutePositions;

        [GlobalSetup]
        public void GlobalSetup()
        {
            relativePositions = new Vector3[N];
            absolutePositions = new Vector3[N];
            useAbsolutePositions = new bool[N];
        }

        [IterationSetup]
        public void IterationSetup()
        {
            for (int i = 0; i < N; i++)
            {
                useAbsolutePositions[i] = (i % 3) == 0;
            }
        }

        [Benchmark]
        public float Canvas_ComputeAbsolutePinPosition_InlineProperty()
        {
            // Refer to Canvas.ComputeAbsolutePinPosition
            float sum = 0;
            var parentSize = Vector3.One;
            for (int i = 0; i < relativePositions.Length - 1; i++)
            {
                var relativePosition = relativePositions[i];
                var absolutePosition = absolutePositions[i];
                var useAbsolutePosition = useAbsolutePositions[i];
                // ----- Test
                for (var dim = 0; dim < 3; ++dim)
                {
                    if (float.IsNaN(absolutePosition[dim]) || !useAbsolutePosition && !float.IsNaN(relativePosition[dim]))
                        absolutePosition[dim] = relativePosition[dim] == 0f ? 0f : relativePosition[dim] * parentSize[dim];
                }
                // ----- End Test
                sum += absolutePosition.X;
            }
            return sum;
        }

        [Benchmark]
        public float Canvas_ComputeAbsolutePinPosition_CacheProperty()
        {
            float sum = 0;
            var parentSize = Vector3.One;
            for (int i = 0; i < relativePositions.Length - 1; i++)
            {
                var relativePosition = relativePositions[i];
                var absolutePosition = absolutePositions[i];
                var useAbsolutePosition = useAbsolutePositions[i];
                // ----- Test
                for (var dim = 0; dim < 3; ++dim)
                {
                    var relPos = relativePosition[dim];
                    if (float.IsNaN(absolutePosition[dim]) || !useAbsolutePosition && !float.IsNaN(relPos))
                        absolutePosition[dim] = relPos == 0f ? 0f : relPos * parentSize[dim];
                }
                // ----- End Test
                sum += absolutePosition.X;
            }
            return sum;
        }

        private struct LightStreakEx
        {
            private const int StreakMaxCount = 8;

            private int tapsPerIteration;
            private int streakCount;
            private int iterationCount;

            private Vector2[] tapOffsetsWeights;

            public void Init()
            {
                TapsPerIteration = 4;
                StreakCount = 4;
                Attenuation = 0.7f;
                IterationCount = 5;
            }

            public int StreakCount
            {
                get { return streakCount; }
                set
                {
                    if (value <= 0) value = 0;
                    if (value > StreakMaxCount) value = StreakMaxCount;
                    streakCount = value;
                }
            }

            public float Attenuation { get; set; }
            public int IterationCount
            {
                get { return iterationCount; }
                set { iterationCount = value; }
            }

            public int TapsPerIteration
            {
                get { return tapsPerIteration; }

                private set { tapsPerIteration = value; tapOffsetsWeights = new Vector2[tapsPerIteration]; }
            }

            public float DrawCore_InlineAttenuation()
            {
                // Refer to LightStreak.DrawCore
                for (int streak = 0; streak < StreakCount; streak++)
                {
                    // Treats one streak

                    // Extends the length recursively
                    for (int level = 0; level < IterationCount; level++)
                    {
                        // Calculates weights and attenuation factors for all the taps
                        float totalWeight = 0;
                        float passLength = (float)Math.Pow(TapsPerIteration, level);
                        for (int i = 0; i < TapsPerIteration; i++)
                        {
                            tapOffsetsWeights[i].X = i * passLength;
                            tapOffsetsWeights[i].Y = (float)Math.Pow(MathUtil.Lerp(0.7f, 1.0f, Attenuation), i * passLength);
                            totalWeight += tapOffsetsWeights[i].Y;
                        }
                        // Normalizes the weights
                        for (int i = 0; i < TapsPerIteration; i++)
                        {
                            tapOffsetsWeights[i].Y /= totalWeight;
                        }
                    }
                }

                return tapOffsetsWeights[0].X;
            }

            public float DrawCore_HoistAttenuation()
            {
                var lerpedAttenuation = MathUtil.Lerp(0.7f, 1.0f, Attenuation);
                for (int streak = 0; streak < StreakCount; streak++)
                {
                    // Treats one streak

                    // Extends the length recursively
                    for (int level = 0; level < IterationCount; level++)
                    {
                        // Calculates weights and attenuation factors for all the taps
                        float totalWeight = 0;
                        float passLength = (float)Math.Pow(TapsPerIteration, level);
                        for (int i = 0; i < TapsPerIteration; i++)
                        {
                            tapOffsetsWeights[i].X = i * passLength;
                            tapOffsetsWeights[i].Y = (float)Math.Pow(lerpedAttenuation, i * passLength);
                            totalWeight += tapOffsetsWeights[i].Y;
                        }
                        // Normalizes the weights
                        for (int i = 0; i < TapsPerIteration; i++)
                        {
                            tapOffsetsWeights[i].Y /= totalWeight;
                        }
                    }
                }

                return tapOffsetsWeights[0].X;
            }
        }
    }
}
