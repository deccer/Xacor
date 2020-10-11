﻿using System;
using System.Runtime.InteropServices;
using Xacor.Mathematics;

namespace Xacor.Graphics.Api
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct VertexPosition  : IEquatable<VertexPosition>
    {
        public readonly Vector3 Position;

        public VertexPosition(Vector3 position)
        {
            Position = position;
        }

        public bool Equals(VertexPosition other)
        {
            return Position.Equals(other.Position);
        }

        public override bool Equals(object obj)
        {
            return obj is VertexPosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}