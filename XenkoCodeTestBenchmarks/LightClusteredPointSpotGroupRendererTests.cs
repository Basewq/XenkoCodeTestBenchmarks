using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using Xenko.Core.Collections;
using Xenko.Core.Mathematics;
using Xenko.Rendering;
using Xenko.Rendering.Lights;

namespace XenkoCodeTestBenchmarks
{
    public class LightClusteredPointSpotGroupRendererTests
    {
        private const int N = 1000000;
        private const int LightCount = 4;
        private LightGroupRendererBase.ProcessLightsParameters[] data;
        private RenderLightCollection lightCollection = new RenderLightCollection();
        private readonly List<int> selectedLightIndices = new List<int>(LightCount);

        private const int RenderViewCount = 2;
        private RenderViewInfo[] renderViewInfos;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new LightGroupRendererBase.ProcessLightsParameters[N];
            lightCollection = new RenderLightCollection(LightCount);
            for (int i = 0; i < LightCount; i++)
            {
                lightCollection.Add(new RenderLight());
            }

            for (int i = 0; i < data.Length; i++)
            {
                data[i].LightIndices = new List<int>(LightCount);
                data[i].LightCollection = lightCollection;
            }

            renderViewInfos = new RenderViewInfo[RenderViewCount];
            for (int i = 0; i < renderViewInfos.Length; i++)
            {
                renderViewInfos[i].LightIndices = new FastListStruct<int>();
                renderViewInfos[i].PointLights = new FastListStruct<PointLightData>();
                renderViewInfos[i].SpotLights = new FastListStruct<SpotLightData>();
            }
        }

        [IterationSetup]
        public void IterationSetup()
        {
            selectedLightIndices.Clear();

            for (int i = 0; i < data.Length; i++)
            {
                var lightIndices = data[i].LightIndices;
                lightIndices.Clear();
                for (int lightIndex = 0; lightIndex < LightCount; lightIndex++)
                {
                    lightIndices.Add(lightIndex);
                }
            }
        }

        [Benchmark]
        public int ProcessLights_PopulateLightIndicesByAddAndRemoveAt()
        {
            int sum = 0;
            for (int ii = 0; ii < data.Length; ii++)
            {
                // First, evaluate how many any which light we want to render (store them in selectedLightIndices)
                selectedLightIndices.Clear();

                var parameters = data[ii];
                // ----- Test
                for (int i = 0; i < parameters.LightIndices.Count;)
                {
                    int index = parameters.LightIndices[i];
                    var renderLight = parameters.LightCollection[index];

                    // Check if there might be a renderer that supports shadows instead (in that case skip the light)
                    if (!CanRenderLight(renderLight, parameters)) // If the light projects a texture (we check for this because otherwise this renderer would "steal" the light from the spot light renderer which handle texture projection):    // TODO: Also check for texture projection renderer?
                    {
                        // Skip this light
                        i++;
                    }
                    else
                    {
                        selectedLightIndices.Add(index);
                        parameters.LightIndices.RemoveAt(i);
                    }
                }
                // ----- End Test
                sum += selectedLightIndices.Count;
            }
            return sum;
        }

        [Benchmark]
        public int ProcessLights_PopulateLightIndicesByAddAndRemoveAtReversed()
        {
            int sum = 0;
            for (int ii = 0; ii < data.Length; ii++)
            {
                // First, evaluate how many any which light we want to render (store them in selectedLightIndices)
                selectedLightIndices.Clear();

                var parameters = data[ii];
                // ----- Test
                for (int i = 0; i < parameters.LightIndices.Count; i++)
                {
                    int index = parameters.LightIndices[i];
                    var renderLight = parameters.LightCollection[index];

                    // Check if there might be a renderer that supports shadows instead (in that case skip the light)
                    if (CanRenderLight(renderLight, parameters)) // If the light projects a texture (we check for this because otherwise this renderer would "steal" the light from the spot light renderer which handle texture projection):    // TODO: Also check for texture projection renderer?
                    {
                        selectedLightIndices.Add(index);
                        parameters.LightIndices[i] = -1;
                    }
                }
                for (int i = parameters.LightIndices.Count - 1; i >= 0; i--)
                {
                    if (parameters.LightIndices[i] < 0)
                        parameters.LightIndices.RemoveAt(i);
                }

                // ----- End Test
                sum += selectedLightIndices.Count;
            }
            return sum;
        }

        private static bool CanRenderLight(RenderLight renderLight, LightGroupRendererBase.ProcessLightsParameters parameters)
        {
            // Just to do some minor fake work, but ensure this is true.
            return renderLight.Position == default;
        }

        private static readonly Predicate<int> RemoveMarkedIndices = x => x == -1;
        [Benchmark]
        public float ProcessLights_PopulateLightIndicesByMarkAndRemovePredicate()
        {
            int sum = 0;
            for (int ii = 0; ii < data.Length; ii++)
            {
                // First, evaluate how many any which light we want to render (store them in selectedLightIndices)
                selectedLightIndices.Clear();

                var parameters = data[ii];
                // ----- Test
                for (int i = 0; i < parameters.LightIndices.Count; i++)
                {
                    int index = parameters.LightIndices[i];
                    var renderLight = parameters.LightCollection[index];

                    // Check if there might be a renderer that supports shadows instead (in that case skip the light)
                    if (CanRenderLight(renderLight, parameters)) // If the light projects a texture (we check for this because otherwise this renderer would "steal" the light from the spot light renderer which handle texture projection):    // TODO: Also check for texture projection renderer?
                    {
                        selectedLightIndices.Add(index);
                        parameters.LightIndices[i] = -1;
                    }
                }
                parameters.LightIndices.RemoveAll(RemoveMarkedIndices);

                // ----- End Test
                sum += selectedLightIndices.Count;
            }
            return sum;
        }

        [Benchmark]
        public float ComputeViewsParameter_CopyRenderViewInfo()
        {
            int sum = 0;
            for (int i = 0; i < N; i++)
            {
                var maxLightIndicesCount = 0;
                var maxPointLightsCount = 0;
                var maxSpotLightsCount = 0;

                for (int viewIndex = 0; viewIndex < renderViewInfos.Length; viewIndex++)
                {
                    // ----- Test
                    // First, evaluate how many any which light we want to render (store them in selectedLightIndices)
                    selectedLightIndices.Clear();

                    var renderViewInfo = renderViewInfos[viewIndex];

                    // Update sizes
                    maxLightIndicesCount = Math.Max(maxLightIndicesCount, renderViewInfo.LightIndices.Count);
                    maxPointLightsCount = Math.Max(maxPointLightsCount, renderViewInfo.PointLights.Count);
                    maxSpotLightsCount = Math.Max(maxSpotLightsCount, renderViewInfo.SpotLights.Count);

                    // ----- End Test
                    sum += selectedLightIndices.Count;
                }
            }
            return sum;
        }

        [Benchmark]
        public float ComputeViewsParameter_RenderViewInfoIndexMultipleTimes()
        {
            int sum = 0;
            for (int i = 0; i < N; i++)
            {
                var maxLightIndicesCount = 0;
                var maxPointLightsCount = 0;
                var maxSpotLightsCount = 0;

                for (int viewIndex = 0; viewIndex < renderViewInfos.Length; viewIndex++)
                {
                    // ----- Test
                    // First, evaluate how many any which light we want to render (store them in selectedLightIndices)
                    selectedLightIndices.Clear();

                    // Update sizes
                    maxLightIndicesCount = Math.Max(maxLightIndicesCount, renderViewInfos[viewIndex].LightIndices.Count);
                    maxPointLightsCount = Math.Max(maxPointLightsCount, renderViewInfos[viewIndex].PointLights.Count);
                    maxSpotLightsCount = Math.Max(maxSpotLightsCount, renderViewInfos[viewIndex].SpotLights.Count);

                    // ----- End Test
                    sum += selectedLightIndices.Count;
                }
            }
            return sum;
        }

        [Benchmark]
        public float ComputeViewsParameter_RenderViewInfoRef()
        {
            int sum = 0;
            for (int i = 0; i < N; i++)
            {
                var maxLightIndicesCount = 0;
                var maxPointLightsCount = 0;
                var maxSpotLightsCount = 0;

                for (int viewIndex = 0; viewIndex < renderViewInfos.Length; viewIndex++)
                {
                    // ----- Test
                    // First, evaluate how many any which light we want to render (store them in selectedLightIndices)
                    selectedLightIndices.Clear();

                    ref var renderViewInfo = ref renderViewInfos[viewIndex];

                    // Update sizes
                    maxLightIndicesCount = Math.Max(maxLightIndicesCount, renderViewInfo.LightIndices.Count);
                    maxPointLightsCount = Math.Max(maxPointLightsCount, renderViewInfo.PointLights.Count);
                    maxSpotLightsCount = Math.Max(maxSpotLightsCount, renderViewInfo.SpotLights.Count);

                    // ----- End Test
                    sum += selectedLightIndices.Count;
                }
            }
            return sum;
        }

        private struct RenderViewInfo
        {
            public RenderView RenderView;

            public float ClusterDepthScale;
            public float ClusterDepthBias;

            public FastListStruct<PointLightData> PointLights;
            public FastListStruct<SpotLightData> SpotLights;
            public FastListStruct<int> LightIndices;
            public Int2[] LightClusters;
            public Int2 ClusterCount;
        }
    }
}
