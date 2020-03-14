using BenchmarkDotNet.Attributes;
using System;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class Color3CopyTests
    {
        private const int N = 1000000;

        private Color3[] data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new Color3[N];
        }

        [Benchmark]
        public float Color_EmptyConstructor()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Color3();
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color_Default()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = default;
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color_ConstructorArgZero()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Color3(0f);
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color_Black()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = ColorConst.Black;
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color_ConstructorArgOne()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Color3(1f);
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color_White()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = ColorConst.White;
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }
    }

    static class ColorConst
    {
        internal static readonly Color3 Black = new Color3(0f);
        internal static readonly Color3 White = new Color3(1.0f);
    }
}
