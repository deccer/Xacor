using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xacor.Graphics.Api;
using Xacor.Graphics.Materials;
using Xacor.Mathematics;

namespace Xacor.Graphics.Meshes
{
    public class MeshFactory : IMeshFactory
    {
        private readonly IGraphicsFactory _graphicsFactory;

        public Mesh CreateMesh()
        {
            throw new NotImplementedException();
        }

        public Mesh CreateUnitCubeMesh()
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

            return new Mesh(_graphicsFactory, cubeVertexBuffer, Enumerable.Empty<MeshPart>().ToArray(), Enumerable.Empty<IMaterial>().ToArray());
        }

        public Mesh CreateUnitSphereMesh()
        {
            throw new NotImplementedException();
        }

        public Mesh CreateUnitCylinderMesh()
        {
            throw new NotImplementedException();
        }

        public MeshFactory(IGraphicsFactory graphicsFactory)
        {
            _graphicsFactory = graphicsFactory;
        }
    }
}
