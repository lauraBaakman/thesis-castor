using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace IO
{
	public class FragmentsExporter
	{
		public delegate void CallBack(IO.WriteResult result);

		private readonly GameObject FragmentsRoot;

		private readonly CallBack callback;

		private static string extension = "obj";

		public FragmentsExporter(GameObject fragmentsRoot, CallBack callback)
		{
			FragmentsRoot = fragmentsRoot;
			this.callback = callback;
		}

		public void Export()
		{
			GetOutputDirectory();
		}

		private void GetOutputDirectory()
		{
			SimpleFileBrowser.FileBrowser.ShowSaveDialog(
				onSuccess: ExportFragments,
				onCancel: () => { },
				folderMode: true,
				initialPath: Application.isEditor ? "/Users/laura/Desktop" : null,
				title: "Export",
				saveButtonText: "Export"
			);
		}

		private void ExportFragments(string directory)
		{
			FragmentExporter exporter = new FragmentExporter(callback);

			List<GameObject> exportFragments = GetExportFragments();

			foreach (GameObject fragment in exportFragments)
			{
				exporter.Export(fragment, BuildExportPath(directory, fragment.name));
			}
		}

		private string BuildExportPath(string directory, string fragment_name)
		{
			return Path.Combine(directory, fragment_name + '.' + extension);
		}

		private List<GameObject> GetExportFragments()
		{
			List<GameObject> exportFragments;

			exportFragments = GetSelectedFragments();

			if (exportFragments.IsEmpty()) exportFragments = GetAllFragments(exportFragments);

			return exportFragments;
		}

		private List<GameObject> GetSelectedFragments()
		{
			return new List<GameObject>(RTEditor.EditorObjectSelection.Instance.SelectedGameObjects);
		}

		private List<GameObject> GetAllFragments(List<GameObject> fragments)
		{
			foreach (Transform child in FragmentsRoot.transform)
				//Add only fragments, not the other children of 'Fragments'
				if (child.GetComponent<MeshFilter>() != null) fragments.Add(child.gameObject);

			return fragments;
		}
	}

	public class FragmentExporter
	{
		private static string defaultSuffix = "_transformed";

		private FragmentsExporter.CallBack callback;

		/// <summary>
		/// The suffix to postpend to the name of the fragment.
		/// </summary>
		private string suffix;

		public FragmentExporter(FragmentsExporter.CallBack callback)
			: this(callback, "")
		{ }

		public FragmentExporter(FragmentsExporter.CallBack callback, string suffix = null)
		{
			this.callback = callback;
			this.suffix = suffix ?? defaultSuffix;
		}

		public void Export(GameObject fragment, string path)
		{
			ValidateFragment(fragment);
			WriteResult result = Export(
				mesh: fragment.GetComponent<MeshFilter>().mesh,
				transformation: fragment.transform.localToWorldMatrix,
				path: AddSuffix(path)
			);
			callback(result);
		}

		public string AddSuffix(string path)
		{
			string directory = Path.GetDirectoryName(path);
			string file = Path.GetFileName(path);
			string extension = Path.GetExtension(file);
			string name = Path.GetFileNameWithoutExtension(path);

			string newFile = string.Format("{0}{1}{2}", name, suffix, extension);
			return Path.Combine(directory, newFile);
		}

		private WriteResult Export(Mesh mesh, Matrix4x4 transformation, string path)
		{
			Utils.MeshTransformer meshTransformer = new Utils.MeshTransformer(transformation);
			Mesh transformedMesh = meshTransformer.Transform(mesh);

			return transformedMesh.ToFile(path);
		}

		private void ValidateFragment(GameObject fragment)
		{
			if (fragment.GetComponent<MeshFilter>() == null)
				Debug.LogError("Trying to write a gameobject without MeshFilter to file as a mesh.");
		}
	}
}