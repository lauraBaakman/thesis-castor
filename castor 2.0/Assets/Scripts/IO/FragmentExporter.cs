using System;
using UnityEngine;
namespace IO
{
    public delegate void Callback();

    public class FragmentsExporter
    {
        private readonly GameObject FragmentsRoot;

        private readonly CallBack CallBack;

        public FragmentsExporter(GameObject fragmentsRoot, CallBack callback)
        {
            FragmentsRoot = fragmentsRoot;
            CallBack = callback;
        }

        public void Export()
        {
            GetOutputDirectory();
        }

        private void GetOutputDirectory()
        {
            Debug.Log("lets get the output directory.");
        }
    }
}