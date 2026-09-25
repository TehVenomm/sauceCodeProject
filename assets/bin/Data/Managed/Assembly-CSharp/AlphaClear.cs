// Decompiled with JetBrains decompiler
// Type: AlphaClear
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (Camera))]
public class AlphaClear : MonoBehaviour
{
  [SerializeField]
  private Mesh quad;
  private Matrix4x4 matrix;
  private Material alphaClearMaterial;

  private void Awake()
  {
    this.matrix = Matrix4x4.identity;
    ((Matrix4x4) ref this.matrix).SetTRS(Vector3.zero, Quaternion.AngleAxis(90f, Vector3.right), new Vector3(100f, 100f, 1f));
    this.alphaClearMaterial = new Material(ResourceUtility.FindShader("Custom/AlphaClear"));
  }

  private void OnPostRender()
  {
    this.alphaClearMaterial.SetPass(0);
    Graphics.DrawMeshNow(this.quad, this.matrix, 0);
  }
}
