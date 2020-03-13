using BenchmarkDotNet.Attributes;
using System;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class LightStreakTests
    {
        private const int N = 1000000;

        private LightStreakEx[] data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new LightStreakEx[N];
        }

        [IterationSetup]
        public void IterationSetup()
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i].Init();
            }
        }

        [Benchmark]
        public float DrawCore_InlineAttenuation()
        {
            float sum = 0;
            for (int i = 0; i < data.Length - 1; i++)
            {
                var lightStreak = data[i];
                // ----- Test
                var value = lightStreak.DrawCore_InlineAttenuation();
                // ----- End Test
                sum += value;
            }
            return sum;
        }

        [Benchmark]
        public float DrawCore_HoistAttenuation()
        {
            float sum = 0;
            for (int i = 0; i < data.Length - 1; i++)
            {
                var lightStreak = data[i];
                // ----- Test
                var value = lightStreak.DrawCore_HoistAttenuation();
                // ----- End Test
                sum += value;
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
