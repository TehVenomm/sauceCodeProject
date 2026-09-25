// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.Quads
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

internal class Quads
{
  private static Mesh[] meshes;
  private static int currentQuads;

  private static bool HasMeshes()
  {
    if (Quads.meshes == null)
      return false;
    foreach (Object mesh in Quads.meshes)
    {
      if (Object.op_Equality((Object) null, mesh))
        return false;
    }
    return true;
  }

  public static void Cleanup()
  {
    if (Quads.meshes == null)
      return;
    for (int index = 0; index < Quads.meshes.Length; ++index)
    {
      if (Object.op_Inequality((Object) null, (Object) Quads.meshes[index]))
      {
        Object.DestroyImmediate((Object) Quads.meshes[index]);
        Quads.meshes[index] = (Mesh) null;
      }
    }
    Quads.meshes = (Mesh[]) null;
  }

  public static Mesh[] GetMeshes(int totalWidth, int totalHeight)
  {
    if (Quads.HasMeshes() && Quads.currentQuads == totalWidth * totalHeight)
      return Quads.meshes;
    int num1 = 10833;
    int num2 = totalWidth * totalHeight;
    Quads.currentQuads = num2;
    Quads.meshes = new Mesh[Mathf.CeilToInt((float) (1.0 * (double) num2 / (1.0 * (double) num1)))];
    int index = 0;
    for (int triOffset = 0; triOffset < num2; triOffset += num1)
    {
      int triCount = Mathf.FloorToInt((float) Mathf.Clamp(num2 - triOffset, 0, num1));
      Quads.meshes[index] = Quads.GetMesh(triCount, triOffset, totalWidth, totalHeight);
      ++index;
    }
    return Quads.meshes;
  }

  private static Mesh GetMesh(int triCount, int triOffset, int totalWidth, int totalHeight)
  {
    Mesh mesh = new Mesh();
    ((Object) mesh).hideFlags = (HideFlags) 52;
    Vector3[] vector3Array = new Vector3[triCount * 4];
    Vector2[] vector2Array1 = new Vector2[triCount * 4];
    Vector2[] vector2Array2 = new Vector2[triCount * 4];
    int[] numArray = new int[triCount * 6];
    for (int index1 = 0; index1 < triCount; ++index1)
    {
      int index2 = index1 * 4;
      int index3 = index1 * 6;
      int num1 = triOffset + index1;
      float num2 = Mathf.Floor((float) (num1 % totalWidth)) / (float) totalWidth;
      float num3 = Mathf.Floor((float) (num1 / totalWidth)) / (float) totalHeight;
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector((float) ((double) num2 * 2.0 - 1.0), (float) ((double) num3 * 2.0 - 1.0), 1f);
      vector3Array[index2] = vector3;
      vector3Array[index2 + 1] = vector3;
      vector3Array[index2 + 2] = vector3;
      vector3Array[index2 + 3] = vector3;
      vector2Array1[index2] = new Vector2(0.0f, 0.0f);
      vector2Array1[index2 + 1] = new Vector2(1f, 0.0f);
      vector2Array1[index2 + 2] = new Vector2(0.0f, 1f);
      vector2Array1[index2 + 3] = new Vector2(1f, 1f);
      vector2Array2[index2] = new Vector2(num2, num3);
      vector2Array2[index2 + 1] = new Vector2(num2, num3);
      vector2Array2[index2 + 2] = new Vector2(num2, num3);
      vector2Array2[index2 + 3] = new Vector2(num2, num3);
      numArray[index3] = index2;
      numArray[index3 + 1] = index2 + 1;
      numArray[index3 + 2] = index2 + 2;
      numArray[index3 + 3] = index2 + 1;
      numArray[index3 + 4] = index2 + 2;
      numArray[index3 + 5] = index2 + 3;
    }
    mesh.vertices = vector3Array;
    mesh.triangles = numArray;
    mesh.uv = vector2Array1;
    mesh.uv2 = vector2Array2;
    return mesh;
  }
}
