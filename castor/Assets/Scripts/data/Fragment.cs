using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Fragment
{
	/// <summary>
	/// Gets or set the mesh.
	/// </summary>
	/// <value>The mesh assocaited with the fragment.</value>
	public Mesh Mesh { get; private set; }

	/// <summary>
	/// Gets or set the name of the fragment.
	/// </summary>
	/// <value>The name of the fragment.</value>
	public string Name { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <Fragment cref="Fragment"/> class.
	/// </summary>
	/// <param name="mesh">the mesh associated with the fragment.</param>
	/// <param name="name">the name of the fragment.</param>
	public Fragment (Mesh mesh, string name)
	{
		this.Mesh = mesh;
		this.Name = name;
	}
}
