using System;
using System.IO;
using UnityEngine;
using Utils;
using Fragment;

namespace IO
{
	public class FragmentsImporter
	{
		public delegate void CallBack(IO.ReadResult result);

		public GameObject FragmentsRoot;

		private readonly CallBack callBack;

		private readonly FragmentImporter fragmentImporter;

		public FragmentsImporter(GameObject fragmentParent, CallBack callBack, bool randomizeTransform = false, bool copyVerticesToTexture = false)
		{
			FragmentsRoot = fragmentParent;
			this.callBack = callBack;

			this.fragmentImporter = new FragmentImporter(
				FragmentsRoot, callBack,
				randomizeTransform, copyVerticesToTexture
			);
		}

		public void Import()
		{
			GetFragmentFiles();
		}

		public void Import(string file)
		{
			ImportFragmentFile(file);
		}

		private void GetFragmentFiles()
		{
			SimpleFileBrowser.FileBrowser.ShowLoadDialog(
				onSuccess: ImportFragmentFile,
				onCancel: () => { },
				initialPath: Application.isEditor ? "/Users/laura/Repositories/thesis-experiment/simulated/" : null
			);
		}

		private void ImportFragmentFile(string path)
		{
			fragmentImporter.Import(path, prefabPath: "ExperimentFragment");
		}
	}

	public class FragmentImporter
	{
		private static string DefaultPrefabPath = "Fragment";
		private readonly GameObject Parent;

		private readonly bool RandomizeTransform;
		private readonly bool CopyVerticesToTexture;

		private FragmentsImporter.CallBack CallBack;

		internal FragmentImporter(GameObject parent, FragmentsImporter.CallBack callBack, bool randomizeTransform, bool copyVerticesToTexture)
		{
			Parent = parent;
			CallBack = callBack;

			this.RandomizeTransform = randomizeTransform;
			this.CopyVerticesToTexture = copyVerticesToTexture;
		}

		public FragmentImporter(GameObject parent, FragmentsImporter.CallBack callBack)
			: this(parent, callBack, false, false)
		{ }

		public GameObject Import(string path, string prefabPath = null)
		{
			prefabPath = prefabPath ?? DefaultPrefabPath;

			Ticker.Receiver.Instance.SendMessage(
				methodName: "OnMessage",
				value: new Ticker.Message.InfoMessage(
					string.Format("Reading the file {0}.", path)
				),
				options: SendMessageOptions.RequireReceiver
			);

			ReadResult result = IO.ObjFile.Read(path);
			GameObject fragment = null;

			if (result.Succeeded)
			{
				if (CopyVerticesToTexture) Utils.MeshUtils.CopyVerticesToUV2AndUV3(result.Mesh);

				string name = ExtractObjectName(path);
				fragment = AddFragmentToScene(name, result.Mesh, prefabPath);

				if (result.HasPivot) fragment.GetComponentInChildren<PivotController>().SetPivot(result.Pivot);
			}
			CallBack(result);
			return fragment;
		}

		private string ExtractObjectName(string path)
		{
			return Path.GetFileNameWithoutExtension(path);
		}

		private GameObject AddFragmentToScene(string name, Mesh mesh, string prefabPath)
		{
			GameObject fragment = UnityEngine.Object.Instantiate(
				original: Resources.Load(prefabPath),
				parent: Parent.transform
			) as GameObject;

			fragment.name = name;

			SetDoubleConnectedEdgeList(fragment, mesh);
			SetMesh(fragment, mesh);
			SetMaterial(fragment);


			if (RandomizeTransform) RandomizeTheTransform(fragment);

			return fragment;
		}

		private void SetDoubleConnectedEdgeList(GameObject fragment, Mesh mesh)
		{
			Fragment.DoubleConnectedEdgeListStorage DCELStorage = fragment.GetComponent<Fragment.DoubleConnectedEdgeListStorage>();
			DCELStorage.DCEL = DoubleConnectedEdgeList.DCEL.FromMesh(mesh);
		}

		private void SetMesh(GameObject fragment, Mesh mesh)
		{
			MeshFilter filter = fragment.GetComponent<MeshFilter>();
			filter.mesh = mesh;

			MeshCollider collider = fragment.GetComponent<MeshCollider>();
			collider.sharedMesh = mesh;
		}

		private void RandomizeTheTransform(GameObject fragment)
		{
			fragment.AddComponent<Fragment.RandomTransformer>();
		}

		private void SetMaterial(GameObject fragment)
		{
			MeshRenderer renderer = fragment.GetComponent<MeshRenderer>();
			Material material = renderer.material;
			NamedColor namedColor = ColorGenerator.Instance.GetNextColor();

			RealExperimentLogger.Instance.Log(
				string.Format(
					"Using the color {0} for {1}",
					namedColor.name, fragment.name
				)
			);

			material.color = namedColor.color;
			renderer.material = material;
			fragment.GetComponent<MaterialController>().Init();
		}
	}

}
