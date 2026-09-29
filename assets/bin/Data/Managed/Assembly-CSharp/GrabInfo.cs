// Decompiled with JetBrains decompiler
// Type: GrabInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class GrabInfo
{
  [Tooltip("有効かどうか")]
  public bool enable;
  [Tooltip("掴んでいる秒数")]
  public float duration;
  [Tooltip("離し攻撃のId")]
  public int releaseAttackId;
  [Tooltip("掴み中につける骨")]
  public string parentNode;
  [Tooltip("掴み中のカメラの注視点")]
  public string cameraLookAt;
  [Tooltip("掴み中のカメラの方向")]
  public Vector3 toCameraDir;
  [Tooltip("掴み中のカメラまでの距離")]
  public float cameraDistance;
  [Tooltip("掴み中のカメラ移動速度")]
  public float smoothMaxSpeed;
  [Tooltip("弱点攻撃で解放するかどうか")]
  public bool releaseByWeakHit;
  [Tooltip("武器弱点攻撃で解放するかどうか")]
  public bool releaseByWeaponWeakHit;
  [Tooltip("掴み中のドレイン情報ID")]
  public int drainAttackId;

  public void Copy(GrabInfo src)
  {
    this.enable = src.enable;
    this.duration = src.duration;
    this.releaseAttackId = src.releaseAttackId;
    this.parentNode = src.parentNode;
    this.cameraLookAt = src.cameraLookAt;
    this.toCameraDir = src.toCameraDir;
    this.cameraDistance = src.cameraDistance;
    this.smoothMaxSpeed = src.smoothMaxSpeed;
    this.releaseByWeakHit = src.releaseByWeakHit;
    this.releaseByWeaponWeakHit = src.releaseByWeaponWeakHit;
    this.drainAttackId = src.drainAttackId;
  }
}
