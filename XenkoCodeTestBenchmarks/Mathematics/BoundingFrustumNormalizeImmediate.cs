// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks.Mathematics
{
    /// <summary>
    /// A bounding frustum.
    /// </summary>
    public struct BoundingFrustumNormalizeImmediate
    {
        /// <summary>
        /// The left plane of this frustum.
        /// </summary>
        public Plane LeftPlane;

        /// <summary>
        /// The right plane of this frustum.
        /// </summary>
        public Plane RightPlane;

        /// <summary>
        /// The top  plane of this frustum.
        /// </summary>
        public Plane TopPlane;

        /// <summary>
        /// The bottom plane of this frustum.
        /// </summary>
        public Plane BottomPlane;

        /// <summary>
        /// The near plane of this frustum.
        /// </summary>
        public Plane NearPlane;

        /// <summary>
        /// The far plane of this frustum.
        /// </summary>
        public Plane FarPlane;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingFrustumNormalizeImmediate"/> struct from a matrix view-projection.
        /// </summary>
        /// <param name="matrix">The matrix view projection.</param>
        public BoundingFrustumNormalizeImmediate(ref Matrix matrix)
        {
            // Left
            PlaneExt.Normalize(
                matrix.M14 + matrix.M11,
                matrix.M24 + matrix.M21,
                matrix.M34 + matrix.M31,
                matrix.M44 + matrix.M41,
                out LeftPlane);

            // Right
            PlaneExt.Normalize(
                matrix.M14 - matrix.M11,
                matrix.M24 - matrix.M21,
                matrix.M34 - matrix.M31,
                matrix.M44 - matrix.M41,
                out RightPlane);

            // Top
            PlaneExt.Normalize(
                matrix.M14 - matrix.M12,
                matrix.M24 - matrix.M22,
                matrix.M34 - matrix.M32,
                matrix.M44 - matrix.M42,
                out TopPlane);

            // Bottom
            PlaneExt.Normalize(
                matrix.M14 + matrix.M12,
                matrix.M24 + matrix.M22,
                matrix.M34 + matrix.M32,
                matrix.M44 + matrix.M42,
                out BottomPlane);

            // Near
            PlaneExt.Normalize(
                matrix.M13,
                matrix.M23,
                matrix.M33,
                matrix.M43,
                out NearPlane);

            // Far
            PlaneExt.Normalize(
                matrix.M14 - matrix.M13,
                matrix.M24 - matrix.M23,
                matrix.M34 - matrix.M33,
                matrix.M44 - matrix.M43,
                out FarPlane);
        }

        /// <summary>
        /// Check whether this frustum contains the specified <see cref="BoundingBoxExt"/>.
        /// </summary>
        /// <param name="boundingBoxExt">The bounding box.</param>
        /// <returns><c>true</c> if this frustum contains the specified bounding box.</returns>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public bool Contains(ref BoundingBoxExt boundingBoxExt)
        //{
        //    return CollisionHelper.FrustumContainsBox(ref this, ref boundingBoxExt);
        //}
    }

    public class PlaneExt
    {
        public static void Normalize(float normalX, float normalY, float normalZ, float planeD, out Plane plane)
        {
            float magnitude = 1.0f / (float)(Math.Sqrt((normalX * normalX) + (normalY * normalY) + (normalZ * normalZ)));
            plane = new Plane(normalX * magnitude, normalY * magnitude, normalZ * magnitude, planeD * magnitude);
        }
    }
}
