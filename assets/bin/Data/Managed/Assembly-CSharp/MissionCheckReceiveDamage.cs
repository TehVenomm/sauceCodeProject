// Decompiled with JetBrains decompiler
// Type: MissionCheckReceiveDamage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class MissionCheckReceiveDamage : MissionCheckBase
{
  private bool isOver;

  public override bool IsMissionClear() => !this.isOver;

  public override void OnDamage(AttackedHitStatusFix status, Character to_obj)
  {
    if (Object.op_Equality((Object) (to_obj as Self), (Object) null) || status.damage < this.missionParam)
      return;
    this.isOver = true;
  }
}
