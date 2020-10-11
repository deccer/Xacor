using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using Xacor.Graphics.Api;
using Xacor.Graphics.Materials;
using Xacor.Mathematics;

namespace Xacor.Graphics.Meshes
{
    public interface IMeshPart
    {

    }

    public interface IMesh
    {
        ReadOnlyCollection<IMaterial> Materials { get; }

        ReadOnlyCollection<IMeshPart> MeshParts { get; }

        IVertexBuffer VertexBuffer { get; }
    }

    public class Mesh : IMesh
    {
        public IVertexBuffer VertexBuffer { get; }

        public ReadOnlyCollection<IMaterial> Materials { get; }

        public ReadOnlyCollection<IMeshPart> MeshParts { get; }
        
        internal Mesh(IGraphicsFactory graphicsDevice, IVertexBuffer vertexBuffer, IList<IMeshPart> meshParts, IList<IMaterial> materials)
        {
            Materials = new ReadOnlyCollection<IMaterial>(materials);
            MeshParts = new ReadOnlyCollection<IMeshPart>(meshParts);
            VertexBuffer = vertexBuffer;
        }
    }

    public interface IMeshFactory
    {
        IMesh CreateMesh();

        IMesh CreateUnitCubeMesh();

        IMesh CreateUnitSphereMesh();

        IMesh CreateUnitCylinderMesh();
    }

    public class MeshFactory : IMeshFactory
    {
        private readonly IGraphicsFactory _graphicsFactory;

        public IMesh CreateMesh()
        {
            throw new System.NotImplementedException();
        }

        public IMesh CreateUnitCubeMesh()
        {
            var cubeColor = new Vector4(112 / 256f, 53 / 256f, 63 / 256f, 1.0f);
            var cubeVertices = new List<VertexPositionColor>
            {
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Front 
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),

                new VertexPositionColor(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor), // BACK 
                new VertexPositionColor(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),

                new VertexPositionColor(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor), // Top 
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),

                new VertexPositionColor(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Bottom 
                new VertexPositionColor(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),

                new VertexPositionColor(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Left 
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),

                new VertexPositionColor(new Vector3(1.0f, -1.0f, -1.0f), cubeColor), // Right 
                new VertexPositionColor(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, 1.0f), cubeColor)
            };
            var cubeVertexBuffer = _graphicsFactory.CreateVertexBuffer(cubeVertices.ToArray());

            return new Mesh(_graphicsFactory, cubeVertexBuffer, null, null);
        }

        public IMesh CreateUnitSphereMesh()
        {
            throw new System.NotImplementedException();
        }

        public IMesh CreateUnitCylinderMesh()
        {
            throw new System.NotImplementedException();
        }

        public MeshFactory(IGraphicsFactory graphicsFactory)
        {
            _graphicsFactory = graphicsFactory;
        }
    }
}
