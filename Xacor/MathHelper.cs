using System;
using System.Diagnostics.Contracts;

namespace Xacor
{
    public static class MathHelper
    {
        [Pure]
        public static int Clamp(int n, int min, int max)
        {
            return Math.Max(Math.Min(n, max), min);
        }

        [Pure]
        public static float Clamp(float n, float min, float max)
        {
            return Math.Max(Math.Min(n, max), min);
        }

        [Pure]
        public static float DegreesToRadians(float degrees)
        {
            const float degToRad = (float)Math.PI / 180.0f;
            return degrees * degToRad;
        }

        [Pure]
        public static float RadiansToDegrees(float radians)
        {
            const float radToDeg = 180.0f / (float)Math.PI;
            return radians * radToDeg;
        }
    }
}