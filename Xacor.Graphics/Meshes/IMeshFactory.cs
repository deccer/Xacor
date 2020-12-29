namespace Xacor.Graphics.Meshes
{
    public interface IMeshFactory
    {
        Mesh CreateMesh();

        Mesh CreateUnitCubeMesh();

        Mesh CreateUnitSphereMesh();

        Mesh CreateUnitCylinderMesh();
    }
}