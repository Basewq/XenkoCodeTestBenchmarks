using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using Xenko.Core.Mathematics;
using Xenko.Rendering;

namespace XenkoCodeTestBenchmarks
{
    public class ModelNestedForLoopAccessTests
    {
        private const int N = 100000;

        private ModelComponentExt[] data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new ModelComponentExt[N];

            for (int i = 0; i < data.Length; i++)
            {
                var model = new Model
                {
                    Meshes = new List<Mesh>
                    {
                        new Mesh
                        {
                            Skinning = new MeshSkinningDefinition
                            {
                                Bones = new MeshBoneDefinition[32]
                            }
                        },
                        new Mesh
                        {
                            Skinning = new MeshSkinningDefinition
                            {
                                Bones = new MeshBoneDefinition[32]
                            }
                        }
                    }
                };
                data[i] = new ModelComponentExt(model);
            }
        }

        [Benchmark]
        public float Update_HasNestedReferences()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                var modelComp = data[i];
                // ----- Test
                var value = modelComp.Update_HasNestedReferences();
                // ----- End Test
                sum += value;
            }
            return sum;
        }

        [Benchmark]
        public float Update_NoNestedReferences()
        {
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                var modelComp = data[i];
                // ----- Test
                var value = modelComp.Update_NoNestedReferences();
                // ----- End Test
                sum += value;
            }
            return sum;
        }

        [Benchmark]
        public float Update_HasNestedReferences2()
        {
            // Same as Update_HasNestedReferences, but run again to prevent weird stalling on the first run
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                var modelComp = data[i];
                // ----- Test
                var value = modelComp.Update_HasNestedReferences();
                // ----- End Test
                sum += value;
            }
            return sum;
        }

        [Benchmark]
        public float Update_NoNestedReferences2()
        {
            // Same as Update_NoNestedReferences, but run again to prevent weird stalling on the first run
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                var modelComp = data[i];
                // ----- Test
                var value = modelComp.Update_NoNestedReferences();
                // ----- End Test
                sum += value;
            }
            return sum;
        }

        internal sealed class ModelComponentExt
        {
            private readonly List<MeshInfo> meshInfos = new List<MeshInfo>();
            private Model model;
            private SkeletonUpdater skeleton;
            private bool modelViewHierarchyDirty = true;

            /// <summary>
            /// Per-entity state of each individual mesh of a model.
            /// </summary>
            public class MeshInfo
            {
                /// <summary>
                /// The current blend matrices of a skinned meshes, transforming from mesh space to world space, for each bone.
                /// </summary>
                public Matrix[] BlendMatrices;

                /// <summary>
                /// The meshes current bounding box in world space.
                /// </summary>
                public BoundingBox BoundingBox;

                /// <summary>
                /// The meshes current sphere box in world space.
                /// </summary>
                public BoundingSphere BoundingSphere;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="ModelComponentExt"/> class.
            /// </summary>
            /// <param name="model">The model.</param>
            public ModelComponentExt(Model model)
            {
                Model = model;
                IsShadowCaster = true;
                ModelUpdated();
            }

            public Model Model
            {
                get
                {
                    return model;
                }
                set
                {
                    if (model != value)
                        modelViewHierarchyDirty = true;
                    model = value;
                }
            }

            public bool IsShadowCaster { get; set; }

            public BoundingBox BoundingBox;
            public BoundingSphere BoundingSphere;

            private void ModelUpdated()
            {
                if (model != null)
                {
                    // Create mesh-per-entity state
                    meshInfos.Clear();
                    foreach (var mesh in model.Meshes)
                    {
                        var meshData = new MeshInfo();
                        meshInfos.Add(meshData);

                        if (mesh.Skinning != null)
                            meshData.BlendMatrices = new Matrix[mesh.Skinning.Bones.Length];
                    }

                    if (skeleton != null)
                    {
                        // Reuse previous ModelViewHierarchy
                        skeleton.Initialize(model.Skeleton);
                    }
                    else
                    {
                        skeleton = new SkeletonUpdater(model.Skeleton);
                    }
                }
            }

            public int Update_HasNestedReferences()
            {
                int sum = 0;
                Matrix worldMatrix = Matrix.Identity;

                // Update the bounding sphere / bounding box in world space
                BoundingSphere = BoundingSphere.Empty;
                BoundingBox = BoundingBox.Empty;

                for (int meshIndex = 0; meshIndex < Model.Meshes.Count; meshIndex++)
                {
                    var mesh = Model.Meshes[meshIndex];
                    var meshInfo = meshInfos[meshIndex];
                    meshInfo.BoundingSphere = BoundingSphere.Empty;
                    meshInfo.BoundingBox = BoundingBox.Empty;

                    if (mesh.Skinning != null && skeleton != null)
                    {
                        bool meshHasBoundingBox = false;
                        var bones = mesh.Skinning.Bones;

                        // For skinned meshes, bounding box is union of the bounding boxes of the unskinned mesh, transformed by each affecting bone.
                        for (int boneIndex = 0; boneIndex < bones.Length; boneIndex++)
                        {
                            var nodeIndex = bones[boneIndex].NodeIndex;
                            Matrix.Multiply(ref bones[boneIndex].LinkToMeshMatrix, ref skeleton.NodeTransformations[nodeIndex].WorldMatrix, out meshInfo.BlendMatrices[boneIndex]);

                            BoundingBox skinnedBoundingBox;
                            BoundingBox.Transform(ref mesh.BoundingBox, ref meshInfos[meshIndex].BlendMatrices[boneIndex], out skinnedBoundingBox);
                            BoundingSphere skinnedBoundingSphere;
                            BoundingSphere.Transform(ref mesh.BoundingSphere, ref meshInfos[meshIndex].BlendMatrices[boneIndex], out skinnedBoundingSphere);

                            if (meshHasBoundingBox)
                            {
                                BoundingBox.Merge(ref meshInfo.BoundingBox, ref skinnedBoundingBox, out meshInfo.BoundingBox);
                                BoundingSphere.Merge(ref meshInfo.BoundingSphere, ref skinnedBoundingSphere, out meshInfo.BoundingSphere);
                            }
                            else
                            {
                                meshHasBoundingBox = true;
                                meshInfo.BoundingSphere = skinnedBoundingSphere;
                                meshInfo.BoundingBox = skinnedBoundingBox;
                            }
                            sum++;
                        }
                    }
                    else
                    {
                        // If there is a skeleton, use the corresponding node's transform. Otherwise, fall back to the model transform.
                        var transform = skeleton != null ? skeleton.NodeTransformations[mesh.NodeIndex].WorldMatrix : worldMatrix;
                        BoundingBox.Transform(ref mesh.BoundingBox, ref transform, out meshInfo.BoundingBox);
                        BoundingSphere.Transform(ref mesh.BoundingSphere, ref transform, out meshInfo.BoundingSphere);
                    }
                }

                return sum;
            }

            public int Update_NoNestedReferences()
            {
                int sum = 0;
                Matrix worldMatrix = Matrix.Identity;

                // Update the bounding sphere / bounding box in world space
                BoundingSphere = BoundingSphere.Empty;
                BoundingBox = BoundingBox.Empty;

                for (int meshIndex = 0; meshIndex < Model.Meshes.Count; meshIndex++)
                {
                    var mesh = Model.Meshes[meshIndex];
                    var meshInfo = meshInfos[meshIndex];
                    meshInfo.BoundingSphere = BoundingSphere.Empty;
                    meshInfo.BoundingBox = BoundingBox.Empty;

                    if (mesh.Skinning != null && skeleton != null)
                    {
                        bool meshHasBoundingBox = false;
                        var bones = mesh.Skinning.Bones;

                        // For skinned meshes, bounding box is union of the bounding boxes of the unskinned mesh, transformed by each affecting bone.
                        for (int boneIndex = 0; boneIndex < bones.Length; boneIndex++)
                        {
                            var nodeIndex = bones[boneIndex].NodeIndex;
                            Matrix.Multiply(ref bones[boneIndex].LinkToMeshMatrix, ref skeleton.NodeTransformations[nodeIndex].WorldMatrix, out meshInfo.BlendMatrices[boneIndex]);

                            BoundingBox skinnedBoundingBox;
                            BoundingBox.Transform(ref mesh.BoundingBox, ref meshInfo.BlendMatrices[boneIndex], out skinnedBoundingBox);
                            BoundingSphere skinnedBoundingSphere;
                            BoundingSphere.Transform(ref mesh.BoundingSphere, ref meshInfo.BlendMatrices[boneIndex], out skinnedBoundingSphere);

                            if (meshHasBoundingBox)
                            {
                                BoundingBox.Merge(ref meshInfo.BoundingBox, ref skinnedBoundingBox, out meshInfo.BoundingBox);
                                BoundingSphere.Merge(ref meshInfo.BoundingSphere, ref skinnedBoundingSphere, out meshInfo.BoundingSphere);
                            }
                            else
                            {
                                meshHasBoundingBox = true;
                                meshInfo.BoundingSphere = skinnedBoundingSphere;
                                meshInfo.BoundingBox = skinnedBoundingBox;
                            }
                            sum++;
                        }
                    }
                    else
                    {
                        // If there is a skeleton, use the corresponding node's transform. Otherwise, fall back to the model transform.
                        var transform = skeleton != null ? skeleton.NodeTransformations[mesh.NodeIndex].WorldMatrix : worldMatrix;
                        BoundingBox.Transform(ref mesh.BoundingBox, ref transform, out meshInfo.BoundingBox);
                        BoundingSphere.Transform(ref mesh.BoundingSphere, ref transform, out meshInfo.BoundingSphere);
                    }
                }

                return sum;
            }
        }
    }
}
