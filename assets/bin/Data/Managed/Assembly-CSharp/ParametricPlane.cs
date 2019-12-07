using System;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
[AddComponentMenu("Primitives/Plane")]
public class ParametricPlane : ParametricPrimitive
{
	public float _height = 1f;

	public float _width = 1f;

	protected float height = 1f;

	protected float width = 1f;

	protected float demiHeight;

	protected float demiWidth;

	protected Vector3 topLeft;

	protected Vector3 topRight;

	protected Vector3 bottomLeft;

	protected Vector3 bottomRight;

	public void CreateMesh()
	{
		if (subdivisionsHeight != _subdivisionsHeight || subdivisionsWidth != _subdivisionsWidth || align != _align || invert != _invert || invertNormal != _invertNormal || width != _width || height != _height)
		{
			subdivisionsHeight = _subdivisionsHeight;
			subdivisionsWidth = _subdivisionsWidth;
			align = _align;
			invert = _invert;
			invertNormal = _invertNormal;
			width = _width;
			height = _height;
			ShowMesh();
		}
	}

	protected void Update()
	{
		CreateMesh();
	}

	public override void Reset()
	{
		base.Reset();
		_height = 1f;
		_width = 1f;
	}

	public override void ShowMesh()
	{
		if (subdivisionsWidth < 1)
		{
			subdivisionsWidth = 1;
		}
		if (subdivisionsHeight < 1)
		{
			subdivisionsHeight = 1;
		}
		if (height < 0f)
		{
			height = 0f;
		}
		if (width < 0f)
		{
			width = 0f;
		}
		demiWidth = width / 2f;
		demiHeight = height / 2f;
		switch (align)
		{
		default:
			topLeft = ((!invert) ? new Vector3(0f - demiWidth, 0f, demiHeight) : new Vector3(demiWidth, 0f, demiHeight));
			topRight = ((!invert) ? new Vector3(demiWidth, 0f, demiHeight) : new Vector3(0f - demiWidth, 0f, demiHeight));
			bottomLeft = ((!invert) ? new Vector3(0f - demiWidth, 0f, 0f - demiHeight) : new Vector3(demiWidth, 0f, 0f - demiHeight));
			bottomRight = ((!invert) ? new Vector3(demiWidth, 0f, 0f - demiHeight) : new Vector3(0f - demiWidth, 0f, 0f - demiHeight));
			break;
		case eAlign.alignX:
			topLeft = ((!invert) ? new Vector3(0f, demiHeight, 0f - demiWidth) : new Vector3(0f, demiHeight, demiWidth));
			topRight = ((!invert) ? new Vector3(0f, demiHeight, demiWidth) : new Vector3(0f, demiHeight, 0f - demiWidth));
			bottomLeft = ((!invert) ? new Vector3(0f, 0f - demiHeight, 0f - demiWidth) : new Vector3(0f, 0f - demiHeight, demiWidth));
			bottomRight = ((!invert) ? new Vector3(0f, 0f - demiHeight, demiWidth) : new Vector3(0f, 0f - demiHeight, 0f - demiWidth));
			break;
		case eAlign.alignZ:
			topLeft = ((!invert) ? new Vector3(demiWidth, demiHeight, 0f) : new Vector3(0f - demiWidth, demiHeight, 0f));
			topRight = ((!invert) ? new Vector3(0f - demiWidth, demiHeight, 0f) : new Vector3(demiWidth, demiHeight, 0f));
			bottomLeft = ((!invert) ? new Vector3(demiWidth, 0f - demiHeight, 0f) : new Vector3(0f - demiWidth, 0f - demiHeight, 0f));
			bottomRight = ((!invert) ? new Vector3(0f - demiWidth, 0f - demiHeight, 0f) : new Vector3(demiWidth, 0f - demiHeight, 0f));
			break;
		}
		normal = Vector3.Cross(Vector3.Normalize(topLeft - bottomLeft), Vector3.Normalize(bottomRight - bottomLeft));
		normal *= (invertNormal ? (-1f) : 1f);
		newVertices.Clear();
		newTriangles.Clear();
		newUV.Clear();
		newNormals.Clear();
		mesh.Clear();
		float num = width / (float)subdivisionsWidth;
		float num2 = height / (float)subdivisionsHeight;
		Vector3 a = Vector3.Normalize(bottomLeft - topLeft);
		Vector3 a2 = Vector3.Normalize(topRight - topLeft);
		for (int i = 0; i <= subdivisionsHeight; i++)
		{
			for (int j = 0; j <= subdivisionsWidth; j++)
			{
				newVertices.Add(topLeft + (float)j * num * a2 + (float)i * num2 * a);
				newUV.Add(new Vector2((float)j / (float)subdivisionsWidth, 1f - (float)i / (float)subdivisionsHeight));
				newNormals.Add(normal);
			}
		}
		for (int k = 0; k < subdivisionsHeight; k++)
		{
			for (int l = 0; l < subdivisionsWidth; l++)
			{
				newTriangles.Add(l + (k + 1) * (subdivisionsWidth + 1));
				if (!invertNormal)
				{
					newTriangles.Add(l + k * (subdivisionsWidth + 1));
					newTriangles.Add(l + 1 + k * (subdivisionsWidth + 1));
				}
				else
				{
					newTriangles.Add(l + 1 + k * (subdivisionsWidth + 1));
					newTriangles.Add(l + k * (subdivisionsWidth + 1));
				}
				newTriangles.Add(l + (k + 1) * (subdivisionsWidth + 1));
				if (!invertNormal)
				{
					newTriangles.Add(l + 1 + k * (subdivisionsWidth + 1));
					newTriangles.Add(l + 1 + (k + 1) * (subdivisionsWidth + 1));
				}
				else
				{
					newTriangles.Add(l + 1 + (k + 1) * (subdivisionsWidth + 1));
					newTriangles.Add(l + 1 + k * (subdivisionsWidth + 1));
				}
			}
		}
		mesh.vertices = newVertices.ToArray();
		mesh.triangles = newTriangles.ToArray();
		mesh.uv = newUV.ToArray();
		mesh.normals = newNormals.ToArray();
		meshFilter.mesh = mesh;
	}

	protected override string getName()
	{
		return "ParametricPlane";
	}
}
