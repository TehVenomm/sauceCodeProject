// Decompiled with JetBrains decompiler
// Type: UIBreakableSphere
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIBreakableSphere : MonoBehaviour
{
  [SerializeField]
  private float _breakRate;
  private Material _mat;
  [SerializeField]
  private Mesh _sourceMesh;
  private Mesh newMesh;

  public float breakRate
  {
    get => this._breakRate;
    set
    {
      this._breakRate = value;
      this.mat.SetFloat("_BreakRate", this._breakRate);
    }
  }

  private Material mat
  {
    get
    {
      if (Object.op_Equality((Object) this._mat, (Object) null))
      {
        MeshRenderer component = ((Component) this).GetComponent<MeshRenderer>();
        this._mat = new Material(((Renderer) component).sharedMaterial);
        ((Renderer) component).material = this._mat;
      }
      return this._mat;
    }
  }

  private void OnDestroy()
  {
    Object.Destroy((Object) this._mat);
    Object.Destroy((Object) this.newMesh);
  }

  private void Awake()
  {
    this.breakRate = 1f;
    MeshFilter component = ((Component) this).GetComponent<MeshFilter>();
    int[] indices = this._sourceMesh.GetIndices(0);
    Mesh mesh = new Mesh();
    Vector3[] vector3Array = new Vector3[indices.Length];
    Vector2[] vector2Array1 = new Vector2[indices.Length];
    Vector2[] vector2Array2 = new Vector2[indices.Length];
    Color[] colorArray = new Color[indices.Length];
    int[] numArray = new int[indices.Length];
    for (int index1 = 0; index1 < indices.Length; index1 += 3)
    {
      int index2 = indices[index1];
      int index3 = indices[index1 + 1];
      int index4 = indices[index1 + 2];
      Vector3 vertex1 = this._sourceMesh.vertices[index2];
      Vector3 vertex2 = this._sourceMesh.vertices[index3];
      Vector3 vertex3 = this._sourceMesh.vertices[index4];
      Vector3 vector3 = Vector3.op_Division(Vector3.op_Addition(Vector3.op_Addition(vertex1, vertex2), vertex3), 3f);
      numArray[index1] = index1;
      numArray[index1 + 1] = index1 + 1;
      numArray[index1 + 2] = index1 + 2;
      vector3Array[index1] = vertex1;
      vector3Array[index1 + 1] = vertex2;
      vector3Array[index1 + 2] = vertex3;
      vector2Array1[index1] = new Vector2(vector3.x, vector3.y);
      vector2Array1[index1 + 1] = new Vector2(vector3.x, vector3.y);
      vector2Array1[index1 + 2] = new Vector2(vector3.x, vector3.y);
      vector2Array2[index1] = new Vector2(vector3.z, 0.0f);
      vector2Array2[index1 + 1] = new Vector2(vector3.z, 0.0f);
      vector2Array2[index1 + 2] = new Vector2(vector3.z, 0.0f);
      colorArray[index1] = new Color(this._sourceMesh.colors[index2].a, this._sourceMesh.uv[index2].x, this._sourceMesh.uv[index2].y, 0.0f);
      colorArray[index1 + 1] = new Color(this._sourceMesh.colors[index2].a, this._sourceMesh.uv[index3].x, this._sourceMesh.uv[index3].y, 0.0f);
      colorArray[index1 + 2] = new Color(this._sourceMesh.colors[index2].a, this._sourceMesh.uv[index4].x, this._sourceMesh.uv[index4].y, 0.0f);
    }
    mesh.vertices = vector3Array;
    mesh.uv = vector2Array1;
    mesh.uv2 = vector2Array2;
    mesh.colors = colorArray;
    mesh.SetIndices(numArray, (MeshTopology) 0, 0);
    component.mesh = mesh;
    this.newMesh = mesh;
  }
}
