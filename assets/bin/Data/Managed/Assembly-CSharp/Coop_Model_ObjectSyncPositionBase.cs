// Decompiled with JetBrains decompiler
// Type: Coop_Model_ObjectSyncPositionBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_ObjectSyncPositionBase : Coop_Model_ObjectBase
{
  public Vector3 pos = Vector3.zero;
  public float dir;

  public override Vector3 GetObjectPosition() => this.pos;

  public override bool IsHaveObjectPosition() => true;

  public void SetSyncPosition(StageObject target)
  {
    this.pos = target._position;
    Quaternion rotation = target._rotation;
    this.dir = ((Quaternion) ref rotation).eulerAngles.y;
  }
}
