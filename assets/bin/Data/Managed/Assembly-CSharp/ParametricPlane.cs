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
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
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
			topLeft = (invert ? new Vector3(demiWidth, 0f, demiHeight) : new Vector3(0f - demiWidth, 0f, demiHeight));
			topRight = (invert ? new Vector3(0f - demiWidth, 0f, demiHeight) : new Vector3(demiWidth, 0f, demiHeight));
			bottomLeft = (invert ? new Vector3(demiWidth, 0f, 0f - demiHeight) : new Vector3(0f - demiWidth, 0f, 0f - demiHeight));
			bottomRight = (invert ? new Vector3(0f - demiWidth, 0f, 0f - demiHeight) : new Vector3(demiWidth, 0f, 0f - demiHeight));
			break;
		case eAlign.alignX:
			topLeft = (invert ? new Vector3(0f, demiHeight, demiWidth) : new Vector3(0f, demiHeight, 0f - demiWidth));
			topRight = (invert ? new Vector3(0f, demiHeight, 0f - demiWidth) : new Vector3(0f, demiHeight, demiWidth));
			bottomLeft = (invert ? new Vector3(0f, 0f - demiHeight, demiWidth) : new Vector3(0f, 0f - demiHeight, 0f - demiWidth));
			bottomRight = (invert ? new Vector3(0f, 0f - demiHeight, 0f - demiWidth) : new Vector3(0f, 0f - demiHeight, demiWidth));
			break;
		case eAlign.alignZ:
			topLeft = (invert ? new Vector3(0f - demiWidth, demiHeight, 0f) : new Vector3(demiWidth, demiHeight, 0f));
			topRight = (invert ? new Vector3(demiWidth, demiHeight, 0f) : new Vector3(0f - demiWidth, demiHeight, 0f));
			bottomLeft = (invert ? new Vector3(0f - demiWidth, 0f - demiHeight, 0f) : new Vector3(demiWidth, 0f - demiHeight, 0f));
			bottomRight = (invert ? new Vector3(demiWidth, 0f - demiHeight, 0f) : new Vector3(0f - demiWidth, 0f - demiHeight, 0f));
			break;
		}
		normal = Vector3.Cross(Vector3.Normalize(topLeft - bottomLeft), Vector3.Normalize(bottomRight - bottomLeft));
		normal *= ((!invertNormal) ? 1f : (-1f));
		newVertices.Clear();
		newTriangles.Clear();
		newUV.Clear();
		newNormals.Clear();
		mesh.Clear();
		float num = width / (float)subdivisionsWidth;
		float num2 = height / (float)subdivisionsHeight;
		Vector3 val = Vector3.Normalize(bottomLeft - topLeft);
		Vector3 val2 = Vector3.Normalize(topRight - topLeft);
		for (int i = 0; i <= subdivisionsHeight; i++)
		{
			for (int j = 0; j <= subdivisionsWidth; j++)
			{
				newVertices.Add(topLeft + (float)j * num * val2 + (float)i * num2 * val);
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
		mesh.set_vertices(newVertices.ToArray());
		mesh.set_triangles(newTriangles.ToArray());
		mesh.set_uv(newUV.ToArray());
		mesh.set_normals(newNormals.ToArray());
		meshFilter.set_mesh(mesh);
	}

	protected override string getName()
	{
		return "ParametricPlane";
	}
}
