// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Runtime.InteropServices;

using Xenko.Core;
using Xenko.Graphics;


namespace XenkoCodeTestBenchmarks.Graphics
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct DepthStencilStencilOpDescriptionNew : IEquatable<DepthStencilStencilOpDescriptionNew>
    {
        /// <summary>
        /// Gets or sets the stencil operation to perform if the stencil test fails. The default is StencilOperation.Keep.
        /// </summary>
        public StencilOperation StencilFail { get; set; }

        /// <summary>
        /// Gets or sets the stencil operation to perform if the stencil test passes and the depth-test fails. The default is StencilOperation.Keep.
        /// </summary>
        public StencilOperation StencilDepthBufferFail { get; set; }

        /// <summary>
        /// Gets or sets the stencil operation to perform if the stencil test passes. The default is StencilOperation.Keep.
        /// </summary>
        public StencilOperation StencilPass { get; set; }

        /// <summary>
        /// Gets or sets the comparison function for the stencil test. The default is CompareFunction.Always.
        /// </summary>
        public CompareFunction StencilFunction { get; set; }

        public bool Equals(DepthStencilStencilOpDescriptionNew other)
        {
            return EqualsRef(ref this, ref other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is DepthStencilStencilOpDescriptionNew other && EqualsRef(ref this, ref other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)StencilFail;
                hashCode = (hashCode * 397) ^ (int)StencilDepthBufferFail;
                hashCode = (hashCode * 397) ^ (int)StencilPass;
                hashCode = (hashCode * 397) ^ (int)StencilFunction;
                return hashCode;
            }
        }

        public static bool operator ==(DepthStencilStencilOpDescriptionNew left, DepthStencilStencilOpDescriptionNew right)
        {
            return EqualsRef(ref left, ref right);
        }

        public static bool operator !=(DepthStencilStencilOpDescriptionNew left, DepthStencilStencilOpDescriptionNew right)
        {
            return !EqualsRef(ref left, ref right);
        }

        internal static bool EqualsRef(ref DepthStencilStencilOpDescriptionNew left, ref DepthStencilStencilOpDescriptionNew right)
        {
            return left.StencilFail == right.StencilFail
                && left.StencilDepthBufferFail == right.StencilDepthBufferFail
                && left.StencilPass == right.StencilPass
                && left.StencilFunction == right.StencilFunction;
        }
    }
}
