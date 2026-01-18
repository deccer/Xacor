namespace Xacor.Ecs;

public struct TVertexPositionColor
{
    public float X, Y, Z;
    public float R, G, B, A;

    public TVertexPositionColor(float x, float y, float z, float r, float g, float b, float a)
    {
        X = x; Y = y; Z = z;
        R = r; G = g; B = b; A = a;
    }
}