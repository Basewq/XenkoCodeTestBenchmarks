// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using System.Runtime.InteropServices;

using Xenko.Core;
using Xenko.Core.Extensions;
using Xenko.Core.Mathematics;
using Xenko.Graphics;

namespace XenkoCodeTestBenchmarks.Graphics
{
    /// <summary>
    /// Describes a blend state.
    /// </summary>
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct BlendStateDescriptionOrig : IEquatable<BlendStateDescriptionOrig>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlendStateDescriptionOrig"/> class.
        /// </summary>
        /// <param name="sourceBlend">The source blend.</param>
        /// <param name="destinationBlend">The destination blend.</param>
        public BlendStateDescriptionOrig(Blend sourceBlend, Blend destinationBlend) : this()
        {
            SetDefaults();
            RenderTarget0.BlendEnable = true;
            RenderTarget0.ColorSourceBlend = sourceBlend;
            RenderTarget0.ColorDestinationBlend = destinationBlend;
            RenderTarget0.AlphaSourceBlend = sourceBlend;
            RenderTarget0.AlphaDestinationBlend = destinationBlend;
        }

        /// <summary>
        /// Setup this blend description with defaults value.
        /// </summary>
        public unsafe void SetDefaults()
        {
            AlphaToCoverageEnable = false;
            IndependentBlendEnable = false;

            fixed (BlendStateRenderTargetDescriptionOrig* renderTargets = &RenderTarget0)
            {
                for (int i = 0; i < 8; i++)
                {
                    ref var renderTarget = ref renderTargets[i];
                    renderTarget.BlendEnable = false;
                    renderTarget.ColorSourceBlend = Blend.One;
                    renderTarget.ColorDestinationBlend = Blend.Zero;
                    renderTarget.ColorBlendFunction = BlendFunction.Add;

                    renderTarget.AlphaSourceBlend = Blend.One;
                    renderTarget.AlphaDestinationBlend = Blend.Zero;
                    renderTarget.AlphaBlendFunction = BlendFunction.Add;

                    renderTarget.ColorWriteChannels = ColorWriteChannels.All;
                }
            }
        }

        /// <summary>
        /// Gets default values for this instance.
        /// </summary>
        public static BlendStateDescriptionOrig Default
        {
            get
            {
                var desc = new BlendStateDescriptionOrig();
                desc.SetDefaults();
                return desc;
            }
        }

        /// <summary>
        /// Determines whether or not to use alpha-to-coverage as a multisampling technique when setting a pixel to a rendertarget.
        /// </summary>
        public bool AlphaToCoverageEnable;

        /// <summary>
        /// Set to true to enable independent blending in simultaneous render targets.  If set to false, only the RenderTarget[0] members are used. RenderTarget[1..7] are ignored.
        /// </summary>
        public bool IndependentBlendEnable;

        /// <summary>
        /// An array of render-target-blend descriptions (see <see cref="BlendStateRenderTargetDescriptionOrig"/>); these correspond to the eight rendertargets  that can be set to the output-merger stage at one time.
        /// </summary>
        public BlendStateRenderTargetDescriptionOrig RenderTarget0;
        public BlendStateRenderTargetDescriptionOrig RenderTarget1;
        public BlendStateRenderTargetDescriptionOrig RenderTarget2;
        public BlendStateRenderTargetDescriptionOrig RenderTarget3;
        public BlendStateRenderTargetDescriptionOrig RenderTarget4;
        public BlendStateRenderTargetDescriptionOrig RenderTarget5;
        public BlendStateRenderTargetDescriptionOrig RenderTarget6;
        public BlendStateRenderTargetDescriptionOrig RenderTarget7;

        /// <inheritdoc/>
        public bool Equals(BlendStateDescriptionOrig other)
        {
            if (AlphaToCoverageEnable != other.AlphaToCoverageEnable
                || IndependentBlendEnable != other.IndependentBlendEnable)
                return false;

            if (RenderTarget0 != other.RenderTarget0
                || RenderTarget1 != other.RenderTarget1
                || RenderTarget2 != other.RenderTarget2
                || RenderTarget3 != other.RenderTarget3
                || RenderTarget4 != other.RenderTarget4
                || RenderTarget5 != other.RenderTarget5
                || RenderTarget6 != other.RenderTarget6
                || RenderTarget7 != other.RenderTarget7)
                return false;

            return true;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is BlendStateDescriptionOrig && Equals((BlendStateDescriptionOrig)obj);
        }

        public static bool operator ==(BlendStateDescriptionOrig left, BlendStateDescriptionOrig right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BlendStateDescriptionOrig left, BlendStateDescriptionOrig right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = AlphaToCoverageEnable.GetHashCode();
                hashCode = (hashCode * 397) ^ IndependentBlendEnable.GetHashCode();
                hashCode = (hashCode * 397) ^ RenderTarget0.GetHashCode();
                hashCode = (hashCode * 397) ^ RenderTarget1.GetHashCode();
                hashCode = (hashCode * 397) ^ RenderTarget2.GetHashCode();
                hashCode = (hashCode * 397) ^ RenderTarget3.GetHashCode();
                hashCode = (hashCode * 397) ^ RenderTarget4.GetHashCode();
                hashCode = (hashCode * 397) ^ RenderTarget5.GetHashCode();
                hashCode = (hashCode * 397) ^ RenderTarget6.GetHashCode();
                hashCode = (hashCode * 397) ^ RenderTarget7.GetHashCode();
                return hashCode;
            }
        }
    }
}
