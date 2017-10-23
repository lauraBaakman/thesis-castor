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
		Debug.Log ("Load: " + path);
	}
}
