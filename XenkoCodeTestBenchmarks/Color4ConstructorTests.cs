using BenchmarkDotNet.Attributes;
using Xenko.Core;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class Color4ConstructorTests
    {
        private const int N = 10000000;

        private Color4Ext[] data;
        private Color4Ext2[] data2;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new Color4Ext[N];
            data2 = new Color4Ext2[N];
        }

        [Benchmark]
        public float Color4_EmptyConstructor()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Color4Ext();
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color4_EmptyConstructor2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = new Color4Ext2();
                // ----- End Test
                sum += data2[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color4_Default()
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
        public float Color4_Default2()
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
        public float Color4_ConstructorArgOne()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Color4Ext(1f);
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color4_ConstructorArgOne2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = new Color4Ext2(1f);
                // ----- End Test
                sum += data2[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color4_ConstructorArgThree()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new Color4Ext(1f, 1f, 1f);
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color4_ConstructorArgThree2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = new Color4Ext2(1f, 1f, 1f);
                // ----- End Test
                sum += data2[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color4_White()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = Color4Ext.White;
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color4_White2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = Color4Ext2.White;
                // ----- End Test
                sum += data2[i].R;
            }
            return sum;
        }

        private struct Color4Ext
        {
            /// <summary>
            /// The White color (1, 1, 1).
            /// </summary>
            public static readonly Color4Ext White = new Color4Ext(1.0f);

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
            /// The alpha component of the color.
            /// </summary>
            [DataMember(3)]
            public float A;

            /// <summary>
            /// Initializes a new instance of the <see cref="Color4"/> struct.
            /// </summary>
            /// <param name="value">The value that will be assigned to all components.</param>
            public Color4Ext(float value)
            {
                A = R = G = B = value;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Color4"/> struct.
            /// </summary>
            /// <param name="red">The red component of the color.</param>
            /// <param name="green">The green component of the color.</param>
            /// <param name="blue">The blue component of the color.</param>
            /// <param name="alpha">The alpha component of the color.</param>
            public Color4Ext(float red, float green, float blue, float alpha = 1f)
            {
                R = red;
                G = green;
                B = blue;
                A = alpha;
            }
        }

        private struct Color4Ext2
        {
            /// <summary>
            /// The White color (1, 1, 1).
            /// </summary>
            public static readonly Color4Ext2 White = new Color4Ext2(1.0f);

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
            /// The alpha component of the color.
            /// </summary>
            [DataMember(3)]
            public float A;

            /// <summary>
            /// Initializes a new instance of the <see cref="Color4"/> struct.
            /// </summary>
            /// <param name="value">The value that will be assigned to all components.</param>
            public Color4Ext2(float value)
            {
                R = value;
                G = value;
                B = value;
                A = value;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Color4"/> struct.
            /// </summary>
            /// <param name="red">The red component of the color.</param>
            /// <param name="green">The green component of the color.</param>
            /// <param name="blue">The blue component of the color.</param>
            /// <param name="alpha">The alpha component of the color.</param>
            public Color4Ext2(float red, float green, float blue, float alpha = 1f)
            {
                R = red;
                G = green;
                B = blue;
                A = alpha;
            }
        }
    }
}