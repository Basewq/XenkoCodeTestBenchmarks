using BenchmarkDotNet.Attributes;
using Xenko.Core.Mathematics;
using Xenko.Graphics;
using Xenko.Rendering;
using Xenko.Rendering.Materials;
using Xenko.Rendering.Materials.ComputeColors;

namespace XenkoCodeTestBenchmarks
{
    public class PhysicsDebugShapeMaterialTests
    {
        private const int N = 1000000;

        private GraphicsDevice device;
        private Material material;

        [GlobalSetup]
        public void GlobalSetup()
        {
            device = GraphicsDevice.New();
            IComputeColor diffuseMap = new ComputeColor();

            material = Material.New(device, new MaterialDescriptor
            {
                Attributes =
                {
                    Diffuse = new MaterialDiffuseMapFeature(diffuseMap),
                    DiffuseModel = new MaterialDiffuseLambertModelFeature(),
                    Emissive = new MaterialEmissiveMapFeature(new ComputeColor()),
                },
            });
        }

        [Benchmark]
        public Material PhysicsDebugShapeMaterial_SetParameterOrig()
        {
            for (int i = 0; i < N; i++)
            {
                var color = new Color(i);
                float intensity = i;
                // ----- Test
                material.Passes[0].Parameters.Set(MaterialKeys.DiffuseValue, new Color4(color).ToColorSpace(device.ColorSpace));

                material.Passes[0].Parameters.Set(MaterialKeys.EmissiveIntensity, intensity);
                material.Passes[0].Parameters.Set(MaterialKeys.EmissiveValue, new Color4(color).ToColorSpace(device.ColorSpace));
                // ----- End Test
            }
            return material;
        }

        [Benchmark]
        public Material PhysicsDebugShapeMaterial_SetParameterCacheParameterCalcMultiple()
        {
            for (int i = 0; i < N; i++)
            {
                var color = new Color(i);
                float intensity = i;
                // ----- Test
                var parameters = material.Passes[0].Parameters;
                parameters.Set(MaterialKeys.DiffuseValue, new Color4(color).ToColorSpace(device.ColorSpace));

                parameters.Set(MaterialKeys.EmissiveIntensity, intensity);
                parameters.Set(MaterialKeys.EmissiveValue, new Color4(color).ToColorSpace(device.ColorSpace));
                // ----- End Test
            }
            return material;
        }

        [Benchmark]
        public Material PhysicsDebugShapeMaterial_SetParameterCalcOnceAndByVal()
        {
            for (int i = 0; i < N; i++)
            {
                var color = new Color(i);
                float intensity = i;
                // ----- Test
                var materialColor = new Color4(color).ToColorSpace(device.ColorSpace);
                material.Passes[0].Parameters.Set(MaterialKeys.DiffuseValue, materialColor);

                material.Passes[0].Parameters.Set(MaterialKeys.EmissiveIntensity, intensity);
                material.Passes[0].Parameters.Set(MaterialKeys.EmissiveValue, materialColor);
                // ----- End Test
            }
            return material;
        }

        [Benchmark]
        public Material PhysicsDebugShapeMaterial_SetParameterCalcOnceAndRef()
        {
            for (int i = 0; i < N; i++)
            {
                var color = new Color(i);
                float intensity = i;
                // ----- Test
                var materialColor = new Color4(color).ToColorSpace(device.ColorSpace);
                material.Passes[0].Parameters.Set(MaterialKeys.DiffuseValue, ref materialColor);

                material.Passes[0].Parameters.Set(MaterialKeys.EmissiveIntensity, intensity);
                material.Passes[0].Parameters.Set(MaterialKeys.EmissiveValue, ref materialColor);
                // ----- End Test
            }
            return material;
        }

        [Benchmark]
        public Material PhysicsDebugShapeMaterial_SetParameterCacheParameterCalcOnceAndRef()
        {
            for (int i = 0; i < N; i++)
            {
                var color = new Color(i);
                float intensity = i;
                // ----- Test
                var materialColor = new Color4(color).ToColorSpace(device.ColorSpace);
                var parameters = material.Passes[0].Parameters;
                parameters.Set(MaterialKeys.DiffuseValue, ref materialColor);

                parameters.Set(MaterialKeys.EmissiveIntensity, intensity);
                parameters.Set(MaterialKeys.EmissiveValue, ref materialColor);
                // ----- End Test
            }
            return material;
        }
    }
}
