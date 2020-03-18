using BenchmarkDotNet.Attributes;
using Xenko.Core;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class ColorConstructorTests
    {
        private const int N = 10000000;

        private ColorExt[] data;
        private ColorExt2[] data2;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new ColorExt[N];
            data2 = new ColorExt2[N];
        }

        [Benchmark]
        public float Color_EmptyConstructor()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new ColorExt();
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color_EmptyConstructor2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = new ColorExt2();
                // ----- End Test
                sum += data2[i].R;
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
        public float Color_Default2()
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
        public float Color_ConstructorArgOneByte()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new ColorExt(1);
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color_ConstructorArgOneByte2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = new ColorExt2(1);
                // ----- End Test
                sum += data2[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color_ConstructorArgOneFloat()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new ColorExt(1f);
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color_ConstructorArgOneFloat2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = new ColorExt2(1f);
                // ----- End Test
                sum += data2[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color_ConstructorArgThree()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // ----- Test
                data[i] = new ColorExt(1, 1, 1);
                // ----- End Test
                sum += data[i].R;
            }
            return sum;
        }

        [Benchmark]
        public float Color_ConstructorArgThree2()
        {
            float sum = 0;
            for (int i = 0; i < data2.Length; i++)
            {
                // ----- Test
                data2[i] = new ColorExt2(1, 1, 1);
                // ----- End Test
                sum += data2[i].R;
            }
            return sum;
        }

        private struct ColorExt
        {
            /// <summary>
            /// The red component of the color.
            /// </summary>
            [DataMember(0)]
            public byte R;

            /// <summary>
            /// The green component of the color.
            /// </summary>
            [DataMember(1)]
            public byte G;

            /// <summary>
            /// The blue component of the color.
            /// </summary>
            [DataMember(2)]
            public byte B;

            /// <summary>
            /// The alpha component of the color.
            /// </summary>
            [DataMember(3)]
            public byte A;

            /// <summary>
            /// Initializes a new instance of the <see cref="Color"/> struct.
            /// </summary>
            /// <param name="value">The value that will be assigned to all components.</param>
            public ColorExt(byte value)
            {
                A = R = G = B = value;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Color"/> struct.
            /// </summary>
            /// <param name="value">The value that will be assigned to all components.</param>
            public ColorExt(float value)
            {
                A = R = G = B = ToByte(value);
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Color"/> struct.
            /// </summary>
            /// <param name="red">The red component of the color.</param>
            /// <param name="green">The green component of the color.</param>
            /// <param name="blue">The blue component of the color.</param>
            /// <param name="alpha">The alpha component of the color.</param>
            public ColorExt(byte red, byte green, byte blue, byte alpha)
            {
                R = red;
                G = green;
                B = blue;
                A = alpha;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Color"/> struct.  Alpha is set to 255.
            /// </summary>
            /// <param name="red">The red component of the color.</param>
            /// <param name="green">The green component of the color.</param>
            /// <param name="blue">The blue component of the color.</param>
            public ColorExt(byte red, byte green, byte blue)
            {
                R = red;
                G = green;
                B = blue;
                A = 255;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Color"/> struct.
            /// </summary>
            /// <param name="red">The red component of the color.</param>
            /// <param name="green">The green component of the color.</param>
            /// <param name="blue">The blue component of the color.</param>
            /// <param name="alpha">The alpha component of the color.</param>
            public ColorExt(float red, float green, float blue, float alpha)
            {
                R = ToByte(red);
                G = ToByte(green);
                B = ToByte(blue);
                A = ToByte(alpha);
            }

            private static byte ToByte(float component)
            {
                var value = (int)(component * 255.0f);
                return (byte)(value < 0 ? 0 : value > 255 ? 255 : value);
            }
        }

        private struct ColorExt2
        {
            /// <summary>
            /// The red component of the color.
            /// </summary>
            [DataMember(0)]
            public byte R;

            /// <summary>
            /// The green component of the color.
            /// </summary>
            [DataMember(1)]
            public byte G;

            /// <summary>
            /// The blue component of the color.
            /// </summary>
            [DataMember(2)]
            public byte B;

            /// <summary>
            /// The alpha component of the color.
            /// </summary>
            [DataMember(3)]
            public byte A;

            /// <summary>
            /// Initializes a new instance of the <see cref="Color"/> struct.
            /// </summary>
            /// <param name="value">The value that will be assigned to all components.</param>
            public ColorExt2(byte value)
            {
                R = value;
                G = value;
                B = value;
                A = value;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Color"/> struct.
            /// </summary>
            /// <param name="value">The value that will be assigned to all components.</param>
            public ColorExt2(float value)
                : this(ToByte(value))
            {
                //var byteValue = ToByte(value);
                //R = byteValue;
                //G = byteValue;
                //B = byteValue;
                //A = byteValue;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Color"/> struct.
            /// </summary>
            /// <param name="red">The red component of the color.</param>
            /// <param name="green">The green component of the color.</param>
            /// <param name="blue">The blue component of the color.</param>
            /// <param name="alpha">The alpha component of the color.</param>
            public ColorExt2(byte red, byte green, byte blue, byte alpha)
            {
                R = red;
                G = green;
                B = blue;
                A = alpha;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Color"/> struct.  Alpha is set to 255.
            /// </summary>
            /// <param name="red">The red component of the color.</param>
            /// <param name="green">The green component of the color.</param>
            /// <param name="blue">The blue component of the color.</param>
            public ColorExt2(byte red, byte green, byte blue)
            {
                R = red;
                G = green;
                B = blue;
                A = 255;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Color"/> struct.
            /// </summary>
            /// <param name="red">The red component of the color.</param>
            /// <param name="green">The green component of the color.</param>
            /// <param name="blue">The blue component of the color.</param>
            /// <param name="alpha">The alpha component of the color.</param>
            public ColorExt2(float red, float green, float blue, float alpha)
            {
                R = ToByte(red);
                G = ToByte(green);
                B = ToByte(blue);
                A = ToByte(alpha);
            }

            private static byte ToByte(float component)
            {
                var value = (int)(component * 255.0f);
                return (byte)(value < 0 ? 0 : value > 255 ? 255 : value);
            }
        }
    }
}
