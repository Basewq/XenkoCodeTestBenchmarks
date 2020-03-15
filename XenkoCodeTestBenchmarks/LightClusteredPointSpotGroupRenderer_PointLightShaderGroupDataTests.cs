using BenchmarkDotNet.Attributes;
using System;
using System.Runtime.InteropServices;
using Xenko.Core;
using Xenko.Core.Collections;
using Xenko.Core.Mathematics;

namespace XenkoCodeTestBenchmarks
{
    public class LightClusteredPointSpotGroupRenderer_PointLightShaderGroupDataTests
    {
        private const int N = 100000;

        private Int2 maxClusterCount = new Int2(30, 17);
        public int ClusterSlices = 8; // Number of ranges
        private FastListStruct<LightClusterLinkedNode> lightNodes;
        private FastListStruct<Vector3> lightNodes2;

        [GlobalSetup]
        public void GlobalSetup()
        {
            lightNodes = new FastListStruct<LightClusterLinkedNode>(30 * 17 * 8);
            lightNodes2 = new FastListStruct<Vector3>(30 * 17 * 8);
        }

        [Benchmark]
        public float ComputeViewParameter_InitializeLightNodesByAdd()
        {
            float sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                lightNodes.Clear();
                // ----- Test
                // Initialize cluster with no light (-1)
                for (int i = 0; i < maxClusterCount.X * maxClusterCount.Y * ClusterSlices; i++)
                {
                    lightNodes.Add(new LightClusterLinkedNode(InternalLightType.Point, -1, -1));
                }

                // ----- End Test
                sum += lightNodes.Count;
            }
            return sum;
        }

        [Benchmark]
        public float ComputeViewParameter_InitializeLightNodesResizeAndAssignPredefined()
        {
            float sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                lightNodes.Clear();
                // ----- Test
                // Initialize cluster with no light (-1)
                int size = maxClusterCount.X * maxClusterCount.Y * ClusterSlices;
                lightNodes.EnsureCapacity(size);
                lightNodes.Count = size;
                for (int i = 0; i < size; i++)
                {
                    lightNodes.Items[i] = LightClusterLinkedNode.NotInitialized;
                }

                // ----- End Test
                sum += lightNodes.Count;
            }
            return sum;
        }

        [Benchmark]
        public float ComputeViewParameter_InitializeLightNodesResizeAndAssignPredefinedCacheArray()
        {
            float sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                lightNodes.Clear();
                // ----- Test
                // Initialize cluster with no light (-1)
                int size = maxClusterCount.X * maxClusterCount.Y * ClusterSlices;
                lightNodes.EnsureCapacity(size);
                lightNodes.Count = size;
                var items = lightNodes.Items;
                for (int i = 0; i < size; i++)
                {
                    items[i] = LightClusterLinkedNode.NotInitialized;
                }

                // ----- End Test
                sum += lightNodes.Count;
            }
            return sum;
        }

        [Benchmark]
        public float ComputeViewParameter_InitializeLightNodesResizeAndAssignInline()
        {
            float sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                lightNodes.Clear();
                // ----- Test
                // Initialize cluster with no light (-1)
                int size = maxClusterCount.X * maxClusterCount.Y * ClusterSlices;
                lightNodes.EnsureCapacity(size);
                lightNodes.Count = size;
                for (int i = 0; i < size; i++)
                {
                    lightNodes.Items[i] = new LightClusterLinkedNode(InternalLightType.Point, -1, -1);
                }

                // ----- End Test
                sum += lightNodes.Count;
            }
            return sum;
        }

        [Benchmark]
        public float ComputeViewParameter_InitializeLightNodesResizeAndAssignInlineCacheArray()
        {
            float sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                lightNodes.Clear();
                // ----- Test
                // Initialize cluster with no light (-1)
                int size = maxClusterCount.X * maxClusterCount.Y * ClusterSlices;
                lightNodes.EnsureCapacity(size);
                lightNodes.Count = size;
                var items = lightNodes.Items;
                for (int i = 0; i < size; i++)
                {
                    //lightNodes.Items[i] = new LightClusterLinkedNode(InternalLightType.Point, -1, -1);
                    items[i] = new LightClusterLinkedNode(InternalLightType.Point, -1, -1);
                }

                // ----- End Test
                sum += lightNodes.Count;
            }
            return sum;
        }

        //[Benchmark]
        //public float ComputeViewParameter_InitializeLightNodes2ByAdd()
        //{
        //    float sum = 0;
        //    for (int ii = 0; ii < N; ii++)
        //    {
        //        lightNodes2.Clear();
        //        // ----- Test
        //        // Initialize cluster with no light (-1)
        //        for (int i = 0; i < maxClusterCount.X * maxClusterCount.Y * ClusterSlices; i++)
        //        {
        //            lightNodes2.Add(new Vector3(0, -1, -1));
        //        }

        //        // ----- End Test
        //        sum += lightNodes2.Count;
        //    }
        //    return sum;
        //}

        //[Benchmark]
        //public float ComputeViewParameter_InitializeLightNodes2ResizeAndAssignInline()
        //{
        //    float sum = 0;
        //    for (int ii = 0; ii < N; ii++)
        //    {
        //        lightNodes2.Clear();
        //        // ----- Test
        //        // Initialize cluster with no light (-1)
        //        int size = maxClusterCount.X * maxClusterCount.Y * ClusterSlices;
        //        lightNodes2.EnsureCapacity(size);
        //        lightNodes2.Count = size;
        //        var items = lightNodes2.Items;
        //        for (int i = 0; i < size; i++)
        //        {
        //            //lightNodes2.Items[i] = new LightClusterLinkedNode(InternalLightType.Point, -1, -1);
        //            items[i] = new Vector3(0, -1, -1);
        //            sum += items[i].X;
        //        }

        //        // ----- End Test
        //        sum += lightNodes2.Count;
        //    }
        //    return sum;
        //}

        //public static readonly Vector3 NotInitialized = new Vector3(0, -1, -1);
        //[Benchmark]
        //public float ComputeViewParameter_InitializeLightNodes2ResizeAndAssignPredefined()
        //{
        //    float sum = 0;
        //    for (int ii = 0; ii < N; ii++)
        //    {
        //        lightNodes2.Clear();
        //        // ----- Test
        //        // Initialize cluster with no light (-1)
        //        int size = maxClusterCount.X * maxClusterCount.Y * ClusterSlices;
        //        lightNodes2.EnsureCapacity(size);
        //        lightNodes2.Count = size;
        //        var items = lightNodes2.Items;
        //        for (int i = 0; i < size; i++)
        //        {
        //            //items[i] = Vector3.NotInitialized;
        //            items[i] = Vector3.One;
        //            sum += items[i].X;
        //        }

        //        // ----- End Test
        //        sum += lightNodes2.Count;
        //    }
        //    return sum;
        //}

        // Single linked list of lights (stored in an array)
        private struct LightClusterLinkedNode : IEquatable<LightClusterLinkedNode>
        {
            public static readonly LightClusterLinkedNode NotInitialized = new LightClusterLinkedNode(InternalLightType.Point, -1, -1);

            public readonly InternalLightType LightType;
            public readonly int LightIndex;
            public readonly int NextNode;

            public LightClusterLinkedNode(InternalLightType lightType, int lightIndex, int nextNode)
            {
                LightType = lightType;
                LightIndex = lightIndex;
                NextNode = nextNode;
            }

            public bool Equals(LightClusterLinkedNode other)
            {
                return LightType == other.LightType && LightIndex == other.LightIndex && NextNode == other.NextNode;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is LightClusterLinkedNode && Equals((LightClusterLinkedNode)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (int)LightType;
                    hashCode = (hashCode * 397) ^ LightIndex;
                    hashCode = (hashCode * 397) ^ NextNode;
                    return hashCode;
                }
            }
        }

        //[DataContract("float3")]
        //[DataStyle(DataStyle.Compact)]
        //[StructLayout(LayoutKind.Sequential, Pack = 4)]
        //public struct Vector3 : IEquatable<Vector3>
        //{
        //    public static readonly Vector3 NotInitialized = new Vector3(0, -1, -1);

        //    public /*readonly*/ float LightType;
        //    public /*readonly*/ float LightIndex;
        //    public /*readonly*/ float NextNode;

        //    public Vector3(float lightType, float lightIndex, float nextNode)
        //    {
        //        LightType = lightType;
        //        LightIndex = lightIndex;
        //        NextNode = nextNode;
        //    }

        //    public bool Equals(Vector3 other)
        //    {
        //        return LightType == other.LightType && LightIndex == other.LightIndex && NextNode == other.NextNode;
        //    }

        //    public override bool Equals(object obj)
        //    {
        //        if (ReferenceEquals(null, obj)) return false;
        //        return obj is Vector3 && Equals((Vector3)obj);
        //    }

        //    public override int GetHashCode()
        //    {
        //        unchecked
        //        {
        //            var hashCode = (int)LightType;
        //            //hashCode = (hashCode * 397) ^ LightIndex;
        //            //hashCode = (hashCode * 397) ^ NextNode;
        //            return hashCode;
        //        }
        //    }
        //}

        private enum InternalLightType
        {
            Point,
            Spot,
        }
    }
}
