using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xacor.Graphics.Api;
using Xacor.Graphics.Materials;

namespace Xacor.Graphics.Meshes
{
    public class Mesh : IDisposable
    {
        public IVertexBuffer VertexBuffer { get; }

        public ReadOnlyCollection<IMaterial> Materials { get; }

        public ReadOnlyCollection<MeshPart> MeshParts { get; }
        
        internal Mesh(IGraphicsFactory graphicsDevice, IVertexBuffer vertexBuffer, IList<MeshPart> meshParts, IList<IMaterial> materials)
        {
            Materials = new ReadOnlyCollection<IMaterial>(materials);
            MeshParts = new ReadOnlyCollection<MeshPart>(meshParts);
            VertexBuffer = vertexBuffer;
        }

        public void Dispose()
        {
            VertexBuffer.Dispose();
        }
    }
}