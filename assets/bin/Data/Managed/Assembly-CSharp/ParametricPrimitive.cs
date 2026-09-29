// Decompiled with JetBrains decompiler
// Type: ParametricPrimitive
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[Serializable]
public abstract class ParametricPrimitive : MonoBehaviour
{
  public bool isStatic = true;
  public int _subdivisionsHeight = 1;
  public int _subdivisionsWidth = 1;
  public ParametricPrimitive.eAlign _align = ParametricPrimitive.eAlign.alignY;
  public bool _invert;
  public bool _invertNormal;
  protected int subdivisionsHeight = 1;
  protected int subdivisionsWidth = 1;
  protected ParametricPrimitive.eAlign align = ParametricPrimitive.eAlign.alignY;
  protected bool invert;
  protected bool invertNormal;
  protected Vector3 normal;
  protected List<Vector3> newVertices;
  protected List<Vector3> newNormals;
  protected List<Vector2> newUV;
  protected List<int> newTriangles;
  protected MeshFilter meshFilter;
  protected Mesh mesh;

  protected void Awake()
  {
    this.meshFilter = ((Component) this).GetComponent<MeshFilter>();
    this.mesh = new Mesh();
    this.newVertices = new List<Vector3>();
    this.newUV = new List<Vector2>();
    this.newNormals = new List<Vector3>();
    this.newTriangles = new List<int>();
    this.ShowMesh();
  }

  protected virtual string getName() => nameof (ParametricPrimitive);

  public virtual void Reset()
  {
    this.isStatic = true;
    this._subdivisionsHeight = 1;
    this._subdivisionsWidth = 1;
    this._align = ParametricPrimitive.eAlign.alignY;
    this._invert = false;
    this._invertNormal = false;
  }

  public virtual void ShowMesh()
  {
  }

  public enum eAlign
  {
    alignX,
    alignY,
    alignZ,
  }
}
