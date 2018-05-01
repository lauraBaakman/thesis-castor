using System.Collections.Generic;
using UnityEngine;
using System.CodeDom;

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

        internal Dictionary<string, object> Results;

        internal _StatisticsComputer(string path)
        {
            this.done = false;
            this.Results = new Dictionary<string, object>();

            Debug.Log("Constructor");
        }

        internal void ReadObjFile()
        {
            Debug.Log("ReadObjFile");
        }

        internal void CollectTrueCorrespondences()
        {
            Debug.Log("CollectTrueCorrespondences");
        }

        internal void ComputeTransformationMatrix()
        {
            Debug.Log("ComputeTransformationMatrix");
        }

        internal void ExtractTranslationAndRotation()
        {
            Debug.Log("ExtractTranslationAndRotation");

            done = true;
        }
    }
}
