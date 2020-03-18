using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using XenkoCodeTestBenchmarks.Graphics;

namespace XenkoCodeTestBenchmarks
{
    public class PipelineStateDirect3DTests
    {
        private const int N = 1000000;

        private Dictionary<BlendStateDescriptionOrig, object> BlendStateCacheOrig;
        private Dictionary<RasterizerStateDescriptionOrig, object> RasterizerStateCacheOrig;
        private Dictionary<DepthStencilStateDescriptionOrig, object> DepthStencilStateCacheOrig;

        private Dictionary<BlendStateDescriptionNew, object> BlendStateCacheNew;
        private Dictionary<RasterizerStateDescriptionNew, object> RasterizerStateCacheNew;
        private Dictionary<DepthStencilStateDescriptionNew, object> DepthStencilStateCacheNew;


        private BlendStateDescriptionOrig BlendStateOrig = new BlendStateDescriptionOrig(Xenko.Graphics.Blend.One, Xenko.Graphics.Blend.One);
        private RasterizerStateDescriptionOrig RasterizerStateOrig = new RasterizerStateDescriptionOrig(Xenko.Graphics.CullMode.Back);
        private DepthStencilStateDescriptionOrig DepthStencilStateOrig = new DepthStencilStateDescriptionOrig(false, false);

        private BlendStateDescriptionNew BlendStateNew = new BlendStateDescriptionNew(Xenko.Graphics.Blend.One, Xenko.Graphics.Blend.One);
        private RasterizerStateDescriptionNew RasterizerStateNew = new RasterizerStateDescriptionNew(Xenko.Graphics.CullMode.Back);
        private DepthStencilStateDescriptionNew DepthStencilStateNew = new DepthStencilStateDescriptionNew(false, false);

        [GlobalSetup]
        public void GlobalSetup()
        {
            BlendStateCacheOrig = new Dictionary<BlendStateDescriptionOrig, object>();
            RasterizerStateCacheOrig = new Dictionary<RasterizerStateDescriptionOrig, object>();
            DepthStencilStateCacheOrig = new Dictionary<DepthStencilStateDescriptionOrig, object>();

            BlendStateCacheNew = new Dictionary<BlendStateDescriptionNew, object>();
            RasterizerStateCacheNew = new Dictionary<RasterizerStateDescriptionNew, object>();
            DepthStencilStateCacheNew = new Dictionary<DepthStencilStateDescriptionNew, object>();

            var obj = new object();
            for (int i = 0; i < 4; i++)
            {
                BlendStateOrig.RenderTarget0.AlphaBlendFunction = (Xenko.Graphics.BlendFunction)i;
                RasterizerStateOrig.DepthBias = i;
                DepthStencilStateOrig.FrontFace.StencilDepthBufferFail = (Xenko.Graphics.StencilOperation)i;

                BlendStateNew.RenderTarget0.AlphaBlendFunction = (Xenko.Graphics.BlendFunction)i;
                RasterizerStateNew.DepthBias = i;
                DepthStencilStateNew.FrontFace.StencilDepthBufferFail = (Xenko.Graphics.StencilOperation)i;

                BlendStateCacheOrig.Add(BlendStateOrig, obj);
                RasterizerStateCacheOrig.Add(RasterizerStateOrig, obj);
                DepthStencilStateCacheOrig.Add(DepthStencilStateOrig, obj);

                BlendStateCacheNew.Add(BlendStateNew, obj);
                RasterizerStateCacheNew.Add(RasterizerStateNew, obj);
                DepthStencilStateCacheNew.Add(DepthStencilStateNew, obj);
            }
        }

        [Benchmark]
        public int GraphicsCacheInstantiate_TryGetValueBlendStateOrig()
        {
            int sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                for (int i = 0; i < 4; i++)
                {
                    BlendStateOrig.RenderTarget0.AlphaBlendFunction = (Xenko.Graphics.BlendFunction)i;
                    // ----- Test
                    sum += BlendStateCacheOrig.TryGetValue(BlendStateOrig, out _) ? 1 : 0;
                    // ----- End Test
                }
            }
            return sum;
        }

        [Benchmark]
        public int GraphicsCacheInstantiate_TryGetValueBlendStateNew()
        {
            int sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                for (int i = 0; i < 4; i++)
                {
                    BlendStateNew.RenderTarget0.AlphaBlendFunction = (Xenko.Graphics.BlendFunction)i;
                    // ----- Test
                    sum += BlendStateCacheNew.TryGetValue(BlendStateNew, out _) ? 1 : 0;
                    // ----- End Test
                }
            }
            return sum;
        }

        [Benchmark]
        public int GraphicsCacheInstantiate_TryGetValueRasterizerStateOrig()
        {
            int sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                for (int i = 0; i < 4; i++)
                {
                    RasterizerStateOrig.FillMode = (Xenko.Graphics.FillMode)i;
                    // ----- Test
                    sum += RasterizerStateCacheOrig.TryGetValue(RasterizerStateOrig, out _) ? 1 : 0;
                    // ----- End Test
                }
            }
            return sum;
        }

        [Benchmark]
        public int GraphicsCacheInstantiate_TryGetValueRasterizerStateNew()
        {
            int sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                for (int i = 0; i < 4; i++)
                {
                    RasterizerStateNew.FillMode = (Xenko.Graphics.FillMode)i;
                    // ----- Test
                    sum += RasterizerStateCacheNew.TryGetValue(RasterizerStateNew, out _) ? 1 : 0;
                    // ----- End Test
                }
            }
            return sum;
        }

        [Benchmark]
        public int GraphicsCacheInstantiate_TryGetValueDepthStencilStateOrig()
        {
            int sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                for (int i = 0; i < 4; i++)
                {
                    DepthStencilStateOrig.FrontFace.StencilFail = (Xenko.Graphics.StencilOperation)i;
                    // ----- Test
                    sum += DepthStencilStateCacheOrig.TryGetValue(DepthStencilStateOrig, out _) ? 1 : 0;
                    // ----- End Test
                }
            }
            return sum;
        }

        [Benchmark]
        public int GraphicsCacheInstantiate_TryGetValueDepthStencilStateNew()
        {
            int sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                for (int i = 0; i < 4; i++)
                {
                    DepthStencilStateNew.FrontFace.StencilFail = (Xenko.Graphics.StencilOperation)i;
                    // ----- Test
                    sum += DepthStencilStateCacheNew.TryGetValue(DepthStencilStateNew, out _) ? 1 : 0;
                    // ----- End Test
                }
            }
            return sum;
        }
    }
}
