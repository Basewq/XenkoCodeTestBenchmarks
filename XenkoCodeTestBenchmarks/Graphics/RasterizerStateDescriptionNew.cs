// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Runtime.InteropServices;

using Xenko.Core;
using Xenko.Graphics;


namespace XenkoCodeTestBenchmarks.Graphics
{
    /// <summary>
    /// Describes a rasterizer state.
    /// </summary>
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct RasterizerStateDescriptionNew : IEquatable<RasterizerStateDescriptionNew>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RasterizerStateDescriptionNew"/> class.
        /// </summary>
        /// <param name="cullMode">The cull mode.</param>
        public RasterizerStateDescriptionNew(CullMode cullMode) : this()
        {
            SetDefault();
            CullMode = cullMode;
        }

        /// <summary>
        /// Determines the fill mode to use when rendering (see <see cref="FillMode"/>).
        /// </summary>
        public FillMode FillMode;

        /// <summary>
        /// Indicates triangles facing the specified direction are not drawn (see <see cref="CullMode"/>).
        /// </summary>
        public CullMode CullMode;

        /// <summary>
        /// Determines if a triangle is front- or back-facing. If this parameter is true, then a triangle will be considered front-facing if its vertices are counter-clockwise on the render target and considered back-facing if they are clockwise. If this parameter is false then the opposite is true.
        /// </summary>
        public bool FrontFaceCounterClockwise;

        /// <summary>
        /// Depth value added to a given pixel.
        /// </summary>
        public int DepthBias;

        /// <summary>
        /// Gets or sets the depth bias for polygons, which is the amount of bias to apply to the depth of a primitive to alleviate depth testing problems for primitives of similar depth. The default value is 0.
        /// </summary>
        public float DepthBiasClamp;

        /// <summary>
        /// Scalar on a given pixel's slope.
        /// </summary>
        public float SlopeScaleDepthBias;

        /// <summary>
        /// Enable clipping based on distance.
        /// </summary>
        public bool DepthClipEnable;

        /// <summary>
        /// Enable scissor-rectangle culling. All pixels ouside an active scissor rectangle are culled.
        /// </summary>
        public bool ScissorTestEnable;

        /// <summary>
        /// Multisample level.
        /// </summary>
        public MultisampleCount MultisampleCount;

        /// <summary>
        /// Enable line antialiasing; only applies if doing line drawing and MultisampleEnable is false.
        /// </summary>
        public bool MultisampleAntiAliasLine;

        /// <summary>
        /// Sets default values for this instance.
        /// </summary>
        public void SetDefault()
        {
            CullMode = CullMode.Back;
            FillMode = FillMode.Solid;
            DepthClipEnable = true;
            FrontFaceCounterClockwise = false;
            ScissorTestEnable = false;
            MultisampleCount = MultisampleCount.None;
            MultisampleAntiAliasLine = false;
            DepthBias = 0;
            DepthBiasClamp = 0f;
            SlopeScaleDepthBias = 0f;
        }

        /// <summary>
        /// Gets default values for this instance.
        /// </summary>
        public static RasterizerStateDescriptionNew Default
        {
            get
            {
                var desc = new RasterizerStateDescriptionNew();
                desc.SetDefault();
                return desc;
            }
        }

        public bool Equals(RasterizerStateDescriptionNew other)
        {
            return EqualsRef(ref this, ref other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is RasterizerStateDescriptionNew other && EqualsRef(ref this, ref other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)FillMode;
                hashCode = (hashCode * 397) ^ (int)CullMode;
                hashCode = (hashCode * 397) ^ FrontFaceCounterClockwise.GetHashCode();
                hashCode = (hashCode * 397) ^ DepthBias;
                hashCode = (hashCode * 397) ^ DepthBiasClamp.GetHashCode();
                hashCode = (hashCode * 397) ^ SlopeScaleDepthBias.GetHashCode();
                hashCode = (hashCode * 397) ^ DepthClipEnable.GetHashCode();
                hashCode = (hashCode * 397) ^ ScissorTestEnable.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)MultisampleCount;
                hashCode = (hashCode * 397) ^ MultisampleAntiAliasLine.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(RasterizerStateDescriptionNew left, RasterizerStateDescriptionNew right)
        {
            return EqualsRef(ref left, ref right);
        }

        public static bool operator !=(RasterizerStateDescriptionNew left, RasterizerStateDescriptionNew right)
        {
            return !EqualsRef(ref left, ref right);
        }

        internal static bool EqualsRef(ref RasterizerStateDescriptionNew left, ref RasterizerStateDescriptionNew right)
        {
            return left.FillMode == right.FillMode
                && left.CullMode == right.CullMode
                && left.FrontFaceCounterClockwise == right.FrontFaceCounterClockwise
                && left.DepthBias == right.DepthBias
                && left.DepthBiasClamp.Equals(right.DepthBiasClamp)
                && left.SlopeScaleDepthBias.Equals(right.SlopeScaleDepthBias)
                && left.DepthClipEnable == right.DepthClipEnable
                && left.ScissorTestEnable == right.ScissorTestEnable
                && left.MultisampleCount == right.MultisampleCount
                && left.MultisampleAntiAliasLine == right.MultisampleAntiAliasLine;
        }
    }
}
