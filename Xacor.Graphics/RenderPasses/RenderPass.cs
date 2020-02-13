using System;
using System.Collections.Generic;
using System.Text;

namespace Xacor.Graphics.RenderPasses
{
    public interface IRenderPass
    {
        void Apply();
    }

    public class RenderPass : IRenderPass
    {
        public void Apply()
        {
            throw new NotImplementedException();
        }
    }

    public class GeometryPass : IRenderPass
    {
        public void Apply()
        {
            throw new NotImplementedException();
        }
    }
}
