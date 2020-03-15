using BenchmarkDotNet.Attributes;
using System.Runtime.CompilerServices;
using Xenko.Core.Collections;
using Xenko.Rendering.Lights;

namespace XenkoCodeTestBenchmarks
{
    public class LightDirectionalGroupRendererTest
    {
        private const int N = 1000000;

        private const int TotalLights = 8;
        protected FastListStruct<LightDynamicEntry> lights = new FastListStruct<LightDynamicEntry>(TotalLights);
        protected LightShaderGroupDynamic.LightRange[] lightRanges;
        protected FastListStruct<LightDynamicEntry> currentLights = new FastListStruct<LightDynamicEntry>(8);
        private FastListStruct<DirectionalLightData> lightsData = new FastListStruct<DirectionalLightData>(8);

        [GlobalSetup]
        public void GlobalSetup()
        {
            for (int i = 0; i < TotalLights; i++)
            {
                lights.Add(new LightDynamicEntry(new RenderLight(), shadowMapTexture: null));
            }
            lightRanges = new[]
            {
                new LightShaderGroupDynamic.LightRange(0,4),
                new LightShaderGroupDynamic.LightRange(4,8),
            };
        }

        //[Benchmark]
        //public int ApplyViewParameters_PopulateCurrentLightsOrig()
        //{
        //    int sum = 0;
        //    for (int ii = 0; ii < N; ii++)
        //    {
        //        for (int viewIndex = 0; viewIndex < lightRanges.Length; viewIndex++)
        //        {
        //            // ----- Test
        //            currentLights.Clear();
        //            var lightRange = lightRanges[viewIndex];
        //            for (int i = lightRange.Start; i < lightRange.End; ++i)
        //                currentLights.Add(lights[i]);
        //            // ----- End Test
        //            sum += currentLights.Count;
        //        }
        //    }
        //    return sum;
        //}

        //[Benchmark]
        //public float ApplyViewParameters_PopulateCurrentLightsDirectArray()
        //{
        //    int sum = 0;
        //    for (int ii = 0; ii < N; ii++)
        //    {
        //        for (int viewIndex = 0; viewIndex < lightRanges.Length; viewIndex++)
        //        {
        //            // ----- Test
        //            var lightRange = lightRanges[viewIndex];
        //            PopulateLightsInRange(ref lights, lightRange, ref currentLights);
        //            // ----- End Test
        //            sum += currentLights.Count;
        //        }
        //    }
        //    return sum;
        //}

        [Benchmark]
        public int ApplyViewParameters_PopulateParameterLightsOrig()
        {
            int sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                for (int viewIndex = 0; viewIndex < lightRanges.Length; viewIndex++)
                {
                    var lightRange = lightRanges[viewIndex];
                    PopulateLightsInRange(ref lights, lightRange, ref currentLights);

                    lightsData.Clear();
                    // ----- Test
                    foreach (var lightEntry in currentLights)
                    {
                        var light = lightEntry.Light;
                        lightsData.Add(new DirectionalLightData
                        {
                            DirectionWS = light.Direction,
                            Color = light.Color,
                        });
                    }

                    // ----- End Test
                    sum += lightsData.Count;
                }
            }
            return sum;
        }

        [Benchmark]
        public float ApplyViewParameters_PopulateParameterLightsDirectArray()
        {
            int sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                for (int viewIndex = 0; viewIndex < lightRanges.Length; viewIndex++)
                {
                    var lightRange = lightRanges[viewIndex];
                    PopulateLightsInRange(ref lights, lightRange, ref currentLights);

                    lightsData.Clear();
                    // ----- Test
                    lightsData.EnsureCapacity(currentLights.Count);
                    var lightsDataArray = lightsData.Items;
                    for (int i = 0; i < currentLights.Count; i++)
                    {
                        var light = currentLights[i].Light;
                        lightsDataArray[i] = new DirectionalLightData
                        {
                            DirectionWS = light.Direction,
                            Color = light.Color,
                        };
                    }
                    lightsData.Count = currentLights.Count;
                    // ----- End Test
                    sum += lightsData.Count;
                }
            }
            return sum;
        }

        internal static void PopulateLightsInRange(ref FastListStruct<LightDynamicEntry> allLights, LightShaderGroupDynamic.LightRange lightRange, ref FastListStruct<LightDynamicEntry> lightsInRange)
        {
            int lightsCount = lightRange.End - lightRange.Start;
            lightsInRange.EnsureCapacity(lightsCount);
            var lightsInRangeArray = lightsInRange.Items;
            for (int i = 0; i < lightsCount; i++)
                lightsInRangeArray[i] = allLights[lightRange.Start + i];
            lightsInRange.Count = lightsCount;
        }
    }
}
