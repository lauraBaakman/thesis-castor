using System.Collections.Generic;
using UnityEngine;
using System.CodeDom;
using IO;
using System;
using Registration;

public class StatisticsComputer : MonoBehaviour
{
    public Dictionary<string, object> Results { get { return computer.Results; } }

    public bool Done { get { return computer.Done; } }

    private _StatisticsComputer computer;

    public void Init()
    { }

    public IEnumerator<object> Compute(string objPath)
    {
        computer = new _StatisticsComputer(objPath);
        yield return null;

        computer.ReadObjFile();
        yield return null;

        computer.CollectCorrespondences();
        yield return null;

        computer.ComputeTransformationMatrix();
        yield return null;

        computer.ExtractTranslationAndRotation();
        yield return null;
    }
}

//Shouldn't be public, but wanted to test it
public class _StatisticsComputer
{
    private bool done;
    public bool Done { get { return done; } }

    private string path;

    private Mesh mesh;
    public Mesh Mesh { get { return mesh; } }

    private CorrespondenceCollection correspondences;
    public CorrespondenceCollection Correspondences
    {
        get { return correspondences; }
    }

    internal Dictionary<string, object> Results;

    public _StatisticsComputer(string path)
    {
        this.done = false;
        this.Results = new Dictionary<string, object>();

        correspondences = new CorrespondenceCollection();

        this.path = path;
    }

    public void ReadObjFile()
    {
        ReadResult result = ObjFile.Read(path);
        if (result.Failed)
            throw new InvalidObjFileException(
                string.Format(
                    "Encountered the error {0} while reading the obj file {1}.",
                    result.Message, this.path
                )
            );

        this.mesh = result.Mesh;
    }

    public void CollectCorrespondences()
    {
        int vertexCount = mesh.vertexCount;

        for (int i = 0; i < vertexCount; i++)
            AddCorrespondence(i);
    }

    private void AddCorrespondence(int idx)
    {
        Vector3 newPosition = mesh.vertices[idx];

        Vector3 oldPosition = new Vector3(
            x: mesh.uv2[idx].x,
            y: mesh.uv2[idx].y,
            z: mesh.uv3[idx].x
        );

        this.correspondences.Add(
            new Correspondence(
                staticPosition: oldPosition,
                modelPosition: newPosition
            )
        );
    }

    public void ComputeTransformationMatrix()
    {
        throw new NotImplementedException();
    }

    public void ExtractTranslationAndRotation()
    {
        throw new NotImplementedException();
        done = true;
    }
}
