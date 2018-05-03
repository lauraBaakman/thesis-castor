using System.Collections.Generic;
using UnityEngine;
using System.CodeDom;
using IO;
using System;
using Registration;

public class StatisticsComputer : MonoBehaviour
{
    public Dictionary<string, object> Results { get { return transformComputer.Results; } }

    private bool done;
    public bool Done { get { return done; } }

    private _TransformationComputer transformComputer;

    public void Init()
    {
        done = false;
    }

    public IEnumerator<object> Compute(string objPath)
    {
        transformComputer = new _TransformationComputer(objPath);
        yield return null;

        transformComputer.ReadObjFile();
        yield return null;

        transformComputer.CollectCorrespondences();
        yield return null;

        transformComputer.ComputeTransformationMatrix();
        yield return null;

        transformComputer.ExtractTranslationAndRotation();
        yield return null;

        throw new NotImplementedException("Compare with the expected rotation and translation and store results in Results")
        yield return null;

        done = true;
    }
}

//Shouldn't be public, but wanted to test it
public class _TransformationComputer
{
    private string path;

    private Mesh mesh;
    public Mesh Mesh { get { return mesh; } }

    private Vector3 translation;
    public Vector3 Translation { get { return translation; } }

    private Vector3 rotation;
    public Vector3 Rotation { get { return rotation; } }

    private CorrespondenceCollection correspondences;
    public CorrespondenceCollection Correspondences
    {
        get { return correspondences; }
    }

    private Matrix4x4 transformationMatrix;
    public Matrix4x4 TransformationMatrix { get { return transformationMatrix; } }

    internal Dictionary<string, object> Results;

    public _TransformationComputer(string path)
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
                staticPosition: newPosition,
                modelPosition: oldPosition
            )
        );
    }

    /// <summary>
    /// Find the transformation matrix that transforms the original vertex 
    /// positions, stored in the texture coordinates, to the output vertex 
    /// position, stord in vertex coordinates.
    /// </summary>
    public void ComputeTransformationMatrix()
    {
        HornTransformFinder horn = new HornTransformFinder();
        transformationMatrix = horn.FindTransform(this.correspondences);
    }

    public void ExtractTranslationAndRotation()
    {
        throw new NotImplementedException();
        //Compute translation and rotation
        //Store in Results object that is pubicly accessible.
    }
}
