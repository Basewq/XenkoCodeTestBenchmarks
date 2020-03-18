// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using Xenko.Core;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks.Mathematics
{
    public struct MatrixDataTest
    {
        /// <summary>
        /// The size of the <see cref="Xenko.Core.Mathematics.Matrix"/> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = Utilities.SizeOf<MatrixDataTest>();

        /// <summary>
        /// A <see cref="Xenko.Core.Mathematics.Matrix"/> with all of its components set to zero.
        /// </summary>
        public static readonly MatrixDataTest Zero = new MatrixDataTest();

        /// <summary>
        /// The identity <see cref="Xenko.Core.Mathematics.Matrix"/>.
        /// </summary>
        public static readonly MatrixDataTest Identity = new MatrixDataTest() { M11 = 1.0f, M22 = 1.0f, M33 = 1.0f, M44 = 1.0f };

        /// <summary>
        /// Value at row 1 column 1 of the matrix.
        /// </summary>
        public float M11;

        /// <summary>
        /// Value at row 2 column 1 of the matrix.
        /// </summary>
        public float M21;

        /// <summary>
        /// Value at row 3 column 1 of the matrix.
        /// </summary>
        public float M31;

        /// <summary>
        /// Value at row 4 column 1 of the matrix.
        /// </summary>
        public float M41;

        /// <summary>
        /// Value at row 1 column 2 of the matrix.
        /// </summary>
        public float M12;

        /// <summary>
        /// Value at row 2 column 2 of the matrix.
        /// </summary>
        public float M22;

        /// <summary>
        /// Value at row 3 column 2 of the matrix.
        /// </summary>
        public float M32;

        /// <summary>
        /// Value at row 4 column 2 of the matrix.
        /// </summary>
        public float M42;

        /// <summary>
        /// Value at row 1 column 3 of the matrix.
        /// </summary>
        public float M13;

        /// <summary>
        /// Value at row 2 column 3 of the matrix.
        /// </summary>
        public float M23;

        /// <summary>
        /// Value at row 3 column 3 of the matrix.
        /// </summary>
        public float M33;

        /// <summary>
        /// Value at row 4 column 3 of the matrix.
        /// </summary>
        public float M43;

        /// <summary>
        /// Value at row 1 column 4 of the matrix.
        /// </summary>
        public float M14;

        /// <summary>
        /// Value at row 2 column 4 of the matrix.
        /// </summary>
        public float M24;

        /// <summary>
        /// Value at row 3 column 4 of the matrix.
        /// </summary>
        public float M34;

        /// <summary>
        /// Value at row 4 column 4 of the matrix.
        /// </summary>
        public float M44;

        /// <summary>
        /// Transposes the matrix.
        /// </summary>
        public void TransposeOrig()
        {
            float temp;

            temp = M21; M21 = M12; M12 = temp;
            temp = M31; M31 = M13; M13 = temp;
            temp = M41; M41 = M14; M14 = temp;

            temp = M32; M32 = M23; M23 = temp;
            temp = M42; M42 = M24; M24 = temp;

            temp = M43; M43 = M34; M34 = temp;
        }

        public void TransposeRefSwap()
        {
            Utilities.Swap(ref M12, ref M21);
            Utilities.Swap(ref M13, ref M31);
            Utilities.Swap(ref M14, ref M41);

            Utilities.Swap(ref M23, ref M32);
            Utilities.Swap(ref M24, ref M42);

            Utilities.Swap(ref M34, ref M43);
        }
    }
}
