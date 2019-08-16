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
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
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
