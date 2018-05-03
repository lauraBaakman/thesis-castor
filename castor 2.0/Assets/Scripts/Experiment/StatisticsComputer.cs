using System.Collections.Generic;
using UnityEngine;
using IO;
using System;
using Registration;

public class StatisticsComputer : MonoBehaviour
{
    public Dictionary<string, object> Results { get { return transformComputer.Run.ToDictionary(); } }

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

        throw new NotImplementedException("Compare with the expected rotation and translation and store results in Results");
        yield return null;

        done = true;
    }

    public class Run
    {
        /// <summary>
        /// The rotation used to bring the modelpoints to the static points.
        /// </summary>
        internal Quaternion appliedRotation;
        public Quaternion AppliedRotation { get { return appliedRotation; } }

        public Vector3 ZXYEuler { get { return appliedRotation.eulerAngles; } }

        /// <summary>
        /// The translation used to bring the modelpoints to the static points.
        /// </summary>
        internal Vector3 appliedTranslation;
        public Vector3 AppliedTranslation { get { return appliedTranslation; } }

        public Run()
        {
            this.appliedRotation = Quaternion.identity;
            this.appliedTranslation = new Vector3(0, 0, 0);
        }

        public Run(Quaternion appliedRotation, Vector3 appliedTranslation)
        {
            this.appliedRotation = appliedRotation;
            this.appliedTranslation = appliedTranslation;
        }

        internal Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("applied translation x", appliedTranslation.x);
            dict.Add("applied translation y", appliedTranslation.y);
            dict.Add("applied translation z", appliedTranslation.z);
            dict.Add("applied rotation quaterion x", appliedRotation.x);
            dict.Add("applied rotation quaterion y", appliedRotation.y);
            dict.Add("applied rotation quaterion z", appliedRotation.z);
            dict.Add("applied rotation quaterion w", appliedRotation.w);
            dict.Add("applied rotation zxy euler x", ZXYEuler.x);
            dict.Add("applied rotation zxy euler y", ZXYEuler.y);
            dict.Add("applied rotation zxy euler z", ZXYEuler.z);

            return dict;
        }
    }
}

//Shouldn't be public, but wanted to test it
public class _TransformationComputer
{
    private string path;

    private Mesh mesh;
    public Mesh Mesh { get { return mesh; } }

    private CorrespondenceCollection correspondences;
    public CorrespondenceCollection Correspondences { get { return correspondences; } }

    private StatisticsComputer.Run run;
    public StatisticsComputer.Run Run { get { return run; } }

    private Matrix4x4 transformationMatrix;
    public Matrix4x4 TransformationMatrix { get { return transformationMatrix; } }

    public _TransformationComputer(string path)
    {
        correspondences = new CorrespondenceCollection();

        this.run = new StatisticsComputer.Run();

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
        run.appliedTranslation = ExtractTranslation();
        run.appliedRotation = ExtractRotation();
    }

    private Vector3 ExtractTranslation()
    {
        return transformationMatrix.ExtractTranslation();
    }

    private Quaternion ExtractRotation()
    {
        return transformationMatrix.ExtractRotation();
    }
}
