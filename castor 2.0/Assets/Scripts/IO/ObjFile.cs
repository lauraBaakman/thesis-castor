using UnityEngine;

public static class ObjFile
{
    public static Mesh Read(string path)
    {
        ObjFileReader.ImportFile(path);
        return ObjFileReader.ImportFile(path);
    }

    public static bool Write(Mesh mesh, string path)
    {
        Debug.Log("Temporarily returning true, without writing the mesh to file");
        return true;
    }
}
