using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleFileBrowser;

public class FragmentsMenuBoxController : MonoBehaviour
{

	public void onAddFragmentClick ()
	{
		FileBrowser.ShowLoadDialog (onSelect, 
			() => {
				//No need to do anything if the file dialog is cancelled
			}
		);
	}

	private void onSelect (string path)
	{
		Mesh holderMesh = ObjImporter.ImportFile (path);
		ObjImporter.AverageVertices (holderMesh);

		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer> ();
		MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
		filter.mesh = holderMesh;
	}
}
