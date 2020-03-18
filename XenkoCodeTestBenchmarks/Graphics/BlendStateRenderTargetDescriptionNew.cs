// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using Xenko.Core;
using Xenko.Graphics;


namespace XenkoCodeTestBenchmarks.Graphics
{
    /// <summary>
    /// Describes the blend state for a render target.
    /// </summary>
    [DataContract]
    public struct BlendStateRenderTargetDescriptionNew : IEquatable<BlendStateRenderTargetDescriptionNew>
    {
        /// <summary>
        /// Enable (or disable) blending.
        /// </summary>
        public bool BlendEnable;

        /// <summary>
        /// This <see cref="Blend"/> specifies the first RGB data source and includes an optional pre-blend operation.
        /// </summary>
        public Blend ColorSourceBlend;

        /// <summary>
        /// This <see cref="Blend"/> specifies the second RGB data source and includes an optional pre-blend operation.
        /// </summary>
        public Blend ColorDestinationBlend;

        /// <summary>
        /// This <see cref="BlendFunction"/> defines how to combine the RGB data sources.
        /// </summary>
        public BlendFunction ColorBlendFunction;

        /// <summary>
        /// This <see cref="Blend"/> specifies the first alpha data source and includes an optional pre-blend operation. Blend options that end in _COLOR are not allowed.
        /// </summary>
        public Blend AlphaSourceBlend;

        /// <summary>
        /// This <see cref="Blend"/> specifies the second alpha data source and includes an optional pre-blend operation. Blend options that end in _COLOR are not allowed.
        /// </summary>
        public Blend AlphaDestinationBlend;

        /// <summary>
        /// This <see cref="BlendFunction"/> defines how to combine the alpha data sources.
        /// </summary>
        public BlendFunction AlphaBlendFunction;

        /// <summary>
        /// A write mask.
        /// </summary>
        public ColorWriteChannels ColorWriteChannels;

        public bool Equals(BlendStateRenderTargetDescriptionNew other)
        {
            return EqualsRef(ref this, ref other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is BlendStateRenderTargetDescriptionNew other && EqualsRef(ref this, ref other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = BlendEnable.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)ColorSourceBlend;
                hashCode = (hashCode * 397) ^ (int)ColorDestinationBlend;
                hashCode = (hashCode * 397) ^ (int)ColorBlendFunction;
                hashCode = (hashCode * 397) ^ (int)AlphaSourceBlend;
                hashCode = (hashCode * 397) ^ (int)AlphaDestinationBlend;
                hashCode = (hashCode * 397) ^ (int)AlphaBlendFunction;
                hashCode = (hashCode * 397) ^ (int)ColorWriteChannels;
                return hashCode;
            }
        }

        public static bool operator ==(BlendStateRenderTargetDescriptionNew left, BlendStateRenderTargetDescriptionNew right)
        {
            return EqualsRef(ref left, ref right);
        }

        public static bool operator !=(BlendStateRenderTargetDescriptionNew left, BlendStateRenderTargetDescriptionNew right)
        {
            return !EqualsRef(ref left, ref right);
        }

        internal static bool EqualsRef(ref BlendStateRenderTargetDescriptionNew left, ref BlendStateRenderTargetDescriptionNew right)
        {
            return left.BlendEnable == right.BlendEnable
                && left.ColorSourceBlend == right.ColorSourceBlend
                && left.ColorDestinationBlend == right.ColorDestinationBlend
                && left.ColorBlendFunction == right.ColorBlendFunction
                && left.AlphaSourceBlend == right.AlphaSourceBlend
                && left.AlphaDestinationBlend == right.AlphaDestinationBlend
                && left.AlphaBlendFunction == right.AlphaBlendFunction
                && left.ColorWriteChannels == right.ColorWriteChannels;
        }
    }
}
