using System.Collections.Generic;
using UnityEngine;
using System.CodeDom;
using IO;
using System;

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

        computer.CollectTrueCorrespondences();
        yield return null;

        computer.ComputeTransformationMatrix();
        yield return null;

        computer.ExtractTranslationAndRotation();
        yield return null;
    }

    private class _StatisticsComputer
    {
        private bool done;
        public bool Done { get { return done; } }

        private string path;
        private Mesh mesh;

        internal Dictionary<string, object> Results;

        internal _StatisticsComputer(string path)
        {
            this.done = false;
            this.Results = new Dictionary<string, object>();

            this.path = path;
        }

        internal void ReadObjFile()
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

        internal void CollectTrueCorrespondences()
        {
            throw new NotImplementedException();
        }

        internal void ComputeTransformationMatrix()
        {
            throw new NotImplementedException();
        }

        internal void ExtractTranslationAndRotation()
        {
            throw new NotImplementedException();
            done = true;
        }
    }
}
