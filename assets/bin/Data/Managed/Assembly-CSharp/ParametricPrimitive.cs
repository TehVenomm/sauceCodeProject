using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
public abstract class ParametricPrimitive
{
	public enum eAlign
	{
		alignX,
		alignY,
		alignZ
	}

	public bool isStatic = true;

	public int _subdivisionsHeight = 1;

	public int _subdivisionsWidth = 1;

	public eAlign _align = eAlign.alignY;

	public bool _invert;

	public bool _invertNormal;

	protected int subdivisionsHeight = 1;

	protected int subdivisionsWidth = 1;

	protected eAlign align = eAlign.alignY;

	protected bool invert;

	protected bool invertNormal;

	protected Vector3 normal;

	protected List<Vector3> newVertices;

	protected List<Vector3> newNormals;

	protected List<Vector2> newUV;

	protected List<int> newTriangles;

	protected MeshFilter meshFilter;

	protected Mesh mesh;

	protected ParametricPrimitive()
		: this()
	{
	}

	protected void Awake()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		meshFilter = this.GetComponent<MeshFilter>();
		mesh = new Mesh();
		newVertices = new List<Vector3>();
		newUV = new List<Vector2>();
		newNormals = new List<Vector3>();
		newTriangles = new List<int>();
		ShowMesh();
	}

	protected virtual string getName()
	{
		return "ParametricPrimitive";
	}

	public virtual void Reset()
	{
		isStatic = true;
		_subdivisionsHeight = 1;
		_subdivisionsWidth = 1;
		_align = eAlign.alignY;
		_invert = false;
		_invertNormal = false;
	}

	public virtual void ShowMesh()
	{
	}
}
