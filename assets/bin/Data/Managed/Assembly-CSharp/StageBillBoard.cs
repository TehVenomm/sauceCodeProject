// Decompiled with JetBrains decompiler
// Type: StageBillBoard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class StageBillBoard : MonoBehaviour
{
  private Vector3 cameraPos = Vector3.zero;
  private Transform _transform;

  private void Start() => this._transform = ((Component) this).transform;

  private void Update()
  {
    this.cameraPos = ((Component) Camera.main).transform.position;
    this.cameraPos.y = this._transform.position.y;
    ((Component) this).transform.LookAt(this.cameraPos);
  }
}
