// Decompiled with JetBrains decompiler
// Type: EnemyColliderSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EnemyColliderSettings : MonoBehaviour
{
  [Tooltip("対象collider")]
  public Collider targetCollider;
  [Tooltip("攻撃ヒット無視フラグ")]
  public bool ignoreHitAttack = true;
}
