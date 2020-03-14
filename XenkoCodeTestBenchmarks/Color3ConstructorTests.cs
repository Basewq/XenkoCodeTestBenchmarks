using BenchmarkDotNet.Attributes;
using Xenko.Core;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class Color3ConstructorTests
    {
        private const int N = 10000000;

        private Color3Ext[] data;
        private Color3Ext2[] data2;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new Color3Ext[N];
            data2 = new Color3Ext2[N];
        }

        [Benchmark]
        public float Color3_EmptyConstructor()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Color3Ext();
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color3_EmptyConstructor2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = new Color3Ext2();
                // ----- End Test
                sum += data2[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color3_Default()
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
        public float Color3_Default2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = default;
                // ----- End Test
                sum += data2[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color3_ConstructorArgOne()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Color3Ext(1f);
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color3_ConstructorArgOne2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = new Color3Ext2(1f);
                // ----- End Test
                sum += data2[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color3_ConstructorArgThree()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Color3Ext(1f, 1f, 1f);
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color3_ConstructorArgThree2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = new Color3Ext2(1f, 1f, 1f);
                // ----- End Test
                sum += data2[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color3_White()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = Color3Ext.White;
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color3_White2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = Color3Ext2.White;
                // ----- End Test
                sum += data2[i].R;
            }
            return sum;
        }

        private struct Color3Ext
        {
            private const string ToStringFormat = "R:{0} G:{1} B:{2}";

            /// <summary>
            /// The White color (1, 1, 1).
            /// </summary>
            public static readonly Color3Ext White = new Color3Ext(1.0f);

            /// <summary>
            /// The red component of the color.
            /// </summary>
            [DataMember(0)]
            public float R;

            /// <summary>
            /// The green component of the color.
            /// </summary>
            [DataMember(1)]
            public float G;

            /// <summary>
            /// The blue component of the color.
            /// </summary>
            [DataMember(2)]
            public float B;

            /// <summary>
            /// Initializes a new instance of the <see cref="Color3"/> struct.
            /// </summary>
            /// <param name="value">The value that will be assigned to all components.</param>
            public Color3Ext(float value)
            {
                R = G = B = value;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Color3"/> struct.
            /// </summary>
            /// <param name="red">The red component of the color.</param>
            /// <param name="green">The green component of the color.</param>
            /// <param name="blue">The blue component of the color.</param>
            public Color3Ext(float red, float green, float blue)
            {
                R = red;
                G = green;
                B = blue;
            }
        }

        private struct Color3Ext2
        {
            private const string ToStringFormat = "R:{0} G:{1} B:{2}";

            /// <summary>
            /// The White color (1, 1, 1).
            /// </summary>
            public static readonly Color3Ext2 White = new Color3Ext2(1.0f);

            /// <summary>
            /// The red component of the color.
            /// </summary>
            [DataMember(0)]
            public float R;

            /// <summary>
            /// The green component of the color.
            /// </summary>
            [DataMember(1)]
            public float G;

            /// <summary>
            /// The blue component of the color.
            /// </summary>
            [DataMember(2)]
            public float B;

            /// <summary>
            /// Initializes a new instance of the <see cref="Color3"/> struct.
            /// </summary>
            /// <param name="value">The value that will be assigned to all components.</param>
            public Color3Ext2(float value)
            {
                R = value;
                G = value;
                B = value;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Color3"/> struct.
            /// </summary>
            /// <param name="red">The red component of the color.</param>
            /// <param name="green">The green component of the color.</param>
            /// <param name="blue">The blue component of the color.</param>
            public Color3Ext2(float red, float green, float blue)
            {
                R = red;
                G = green;
                B = blue;
            }
        }
    }
}