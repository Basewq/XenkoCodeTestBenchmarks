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
    public struct BlendStateDescriptionNew : IEquatable<BlendStateDescriptionNew>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlendStateDescriptionNew"/> class.
        /// </summary>
        /// <param name="sourceBlend">The source blend.</param>
        /// <param name="destinationBlend">The destination blend.</param>
        public BlendStateDescriptionNew(Blend sourceBlend, Blend destinationBlend) : this()
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

            fixed (BlendStateRenderTargetDescriptionNew* renderTargets = &RenderTarget0)
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
        public static BlendStateDescriptionNew Default
        {
            get
            {
                var desc = new BlendStateDescriptionNew();
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
        /// An array of render-target-blend descriptions (see <see cref="BlendStateRenderTargetDescriptionNew"/>); these correspond to the eight rendertargets  that can be set to the output-merger stage at one time.
        /// </summary>
        public BlendStateRenderTargetDescriptionNew RenderTarget0;
        public BlendStateRenderTargetDescriptionNew RenderTarget1;
        public BlendStateRenderTargetDescriptionNew RenderTarget2;
        public BlendStateRenderTargetDescriptionNew RenderTarget3;
        public BlendStateRenderTargetDescriptionNew RenderTarget4;
        public BlendStateRenderTargetDescriptionNew RenderTarget5;
        public BlendStateRenderTargetDescriptionNew RenderTarget6;
        public BlendStateRenderTargetDescriptionNew RenderTarget7;

        /// <inheritdoc/>
        public bool Equals(BlendStateDescriptionNew other)
        {
            return EqualsRef(ref this, ref other);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is BlendStateDescriptionNew other && EqualsRef(ref this, ref other);
        }

        public static bool operator ==(BlendStateDescriptionNew left, BlendStateDescriptionNew right)
        {
            return EqualsRef(ref left, ref right);
        }

        public static bool operator !=(BlendStateDescriptionNew left, BlendStateDescriptionNew right)
        {
            return !EqualsRef(ref left, ref right);
        }

        internal static bool EqualsRef(ref BlendStateDescriptionNew left, ref BlendStateDescriptionNew right)
        {
            return left.AlphaToCoverageEnable == right.AlphaToCoverageEnable
                && left.IndependentBlendEnable == right.IndependentBlendEnable
             && BlendStateRenderTargetDescriptionNew.EqualsRef(ref left.RenderTarget0, ref right.RenderTarget0)
                && BlendStateRenderTargetDescriptionNew.EqualsRef(ref left.RenderTarget1, ref right.RenderTarget1)
                && BlendStateRenderTargetDescriptionNew.EqualsRef(ref left.RenderTarget2, ref right.RenderTarget2)
                && BlendStateRenderTargetDescriptionNew.EqualsRef(ref left.RenderTarget3, ref right.RenderTarget3)
                && BlendStateRenderTargetDescriptionNew.EqualsRef(ref left.RenderTarget4, ref right.RenderTarget4)
                && BlendStateRenderTargetDescriptionNew.EqualsRef(ref left.RenderTarget5, ref right.RenderTarget5)
                && BlendStateRenderTargetDescriptionNew.EqualsRef(ref left.RenderTarget6, ref right.RenderTarget6)
                && BlendStateRenderTargetDescriptionNew.EqualsRef(ref left.RenderTarget7, ref right.RenderTarget7);
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
