// Decompiled with JetBrains decompiler
// Type: ParametricPlane
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("Primitives/Plane")]
[Serializable]
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
    if (this.subdivisionsHeight == this._subdivisionsHeight && this.subdivisionsWidth == this._subdivisionsWidth && this.align == this._align && this.invert == this._invert && this.invertNormal == this._invertNormal && (double) this.width == (double) this._width && (double) this.height == (double) this._height)
      return;
    this.subdivisionsHeight = this._subdivisionsHeight;
    this.subdivisionsWidth = this._subdivisionsWidth;
    this.align = this._align;
    this.invert = this._invert;
    this.invertNormal = this._invertNormal;
    this.width = this._width;
    this.height = this._height;
    this.ShowMesh();
  }

  protected void Update() => this.CreateMesh();

  public override void Reset()
  {
    base.Reset();
    this._height = 1f;
    this._width = 1f;
  }

  public override void ShowMesh()
  {
    if (this.subdivisionsWidth < 1)
      this.subdivisionsWidth = 1;
    if (this.subdivisionsHeight < 1)
      this.subdivisionsHeight = 1;
    if ((double) this.height < 0.0)
      this.height = 0.0f;
    if ((double) this.width < 0.0)
      this.width = 0.0f;
    this.demiWidth = this.width / 2f;
    this.demiHeight = this.height / 2f;
    switch (this.align)
    {
      case ParametricPrimitive.eAlign.alignX:
        this.topLeft = !this.invert ? new Vector3(0.0f, this.demiHeight, -this.demiWidth) : new Vector3(0.0f, this.demiHeight, this.demiWidth);
        this.topRight = !this.invert ? new Vector3(0.0f, this.demiHeight, this.demiWidth) : new Vector3(0.0f, this.demiHeight, -this.demiWidth);
        this.bottomLeft = !this.invert ? new Vector3(0.0f, -this.demiHeight, -this.demiWidth) : new Vector3(0.0f, -this.demiHeight, this.demiWidth);
        this.bottomRight = !this.invert ? new Vector3(0.0f, -this.demiHeight, this.demiWidth) : new Vector3(0.0f, -this.demiHeight, -this.demiWidth);
        break;
      case ParametricPrimitive.eAlign.alignZ:
        this.topLeft = !this.invert ? new Vector3(this.demiWidth, this.demiHeight, 0.0f) : new Vector3(-this.demiWidth, this.demiHeight, 0.0f);
        this.topRight = !this.invert ? new Vector3(-this.demiWidth, this.demiHeight, 0.0f) : new Vector3(this.demiWidth, this.demiHeight, 0.0f);
        this.bottomLeft = !this.invert ? new Vector3(this.demiWidth, -this.demiHeight, 0.0f) : new Vector3(-this.demiWidth, -this.demiHeight, 0.0f);
        this.bottomRight = !this.invert ? new Vector3(-this.demiWidth, -this.demiHeight, 0.0f) : new Vector3(this.demiWidth, -this.demiHeight, 0.0f);
        break;
      default:
        this.topLeft = !this.invert ? new Vector3(-this.demiWidth, 0.0f, this.demiHeight) : new Vector3(this.demiWidth, 0.0f, this.demiHeight);
        this.topRight = !this.invert ? new Vector3(this.demiWidth, 0.0f, this.demiHeight) : new Vector3(-this.demiWidth, 0.0f, this.demiHeight);
        this.bottomLeft = !this.invert ? new Vector3(-this.demiWidth, 0.0f, -this.demiHeight) : new Vector3(this.demiWidth, 0.0f, -this.demiHeight);
        this.bottomRight = !this.invert ? new Vector3(this.demiWidth, 0.0f, -this.demiHeight) : new Vector3(-this.demiWidth, 0.0f, -this.demiHeight);
        break;
    }
    this.normal = Vector3.Cross(Vector3.Normalize(Vector3.op_Subtraction(this.topLeft, this.bottomLeft)), Vector3.Normalize(Vector3.op_Subtraction(this.bottomRight, this.bottomLeft)));
    this.normal = Vector3.op_Multiply(this.normal, this.invertNormal ? -1f : 1f);
    this.newVertices.Clear();
    this.newTriangles.Clear();
    this.newUV.Clear();
    this.newNormals.Clear();
    this.mesh.Clear();
    float num1 = this.width / (float) this.subdivisionsWidth;
    float num2 = this.height / (float) this.subdivisionsHeight;
    Vector3 vector3_1 = Vector3.Normalize(Vector3.op_Subtraction(this.bottomLeft, this.topLeft));
    Vector3 vector3_2 = Vector3.Normalize(Vector3.op_Subtraction(this.topRight, this.topLeft));
    for (int index1 = 0; index1 <= this.subdivisionsHeight; ++index1)
    {
      for (int index2 = 0; index2 <= this.subdivisionsWidth; ++index2)
      {
        this.newVertices.Add(Vector3.op_Addition(Vector3.op_Addition(this.topLeft, Vector3.op_Multiply((float) index2 * num1, vector3_2)), Vector3.op_Multiply((float) index1 * num2, vector3_1)));
        this.newUV.Add(new Vector2((float) index2 / (float) this.subdivisionsWidth, (float) (1.0 - (double) index1 / (double) this.subdivisionsHeight)));
        this.newNormals.Add(this.normal);
      }
    }
    for (int index3 = 0; index3 < this.subdivisionsHeight; ++index3)
    {
      for (int index4 = 0; index4 < this.subdivisionsWidth; ++index4)
      {
        this.newTriangles.Add(index4 + (index3 + 1) * (this.subdivisionsWidth + 1));
        if (!this.invertNormal)
        {
          this.newTriangles.Add(index4 + index3 * (this.subdivisionsWidth + 1));
          this.newTriangles.Add(index4 + 1 + index3 * (this.subdivisionsWidth + 1));
        }
        else
        {
          this.newTriangles.Add(index4 + 1 + index3 * (this.subdivisionsWidth + 1));
          this.newTriangles.Add(index4 + index3 * (this.subdivisionsWidth + 1));
        }
        this.newTriangles.Add(index4 + (index3 + 1) * (this.subdivisionsWidth + 1));
        if (!this.invertNormal)
        {
          this.newTriangles.Add(index4 + 1 + index3 * (this.subdivisionsWidth + 1));
          this.newTriangles.Add(index4 + 1 + (index3 + 1) * (this.subdivisionsWidth + 1));
        }
        else
        {
          this.newTriangles.Add(index4 + 1 + (index3 + 1) * (this.subdivisionsWidth + 1));
          this.newTriangles.Add(index4 + 1 + index3 * (this.subdivisionsWidth + 1));
        }
      }
    }
    this.mesh.vertices = this.newVertices.ToArray();
    this.mesh.triangles = this.newTriangles.ToArray();
    this.mesh.uv = this.newUV.ToArray();
    this.mesh.normals = this.newNormals.ToArray();
    this.meshFilter.mesh = this.mesh;
  }

  protected override string getName() => nameof (ParametricPlane);
}
