// Decompiled with JetBrains decompiler
// Type: MissionCheckBadStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class MissionCheckBadStatus : MissionCheckBase
{
  private int badStatusCount;

  public override bool IsMissionClear() => this.badStatusCount <= this.missionParam;

  public override void OnDamage(AttackedHitStatusFix status, Character to_obj)
  {
    if (Object.op_Equality((Object) (to_obj as Self), (Object) null) || (double) status.badStatusTotal.paralyze <= 0.0 && (double) status.badStatusTotal.poison <= 0.0 && (double) status.badStatusTotal.burning <= 0.0 && (double) status.badStatusTotal.speedDown <= 0.0 && (double) status.badStatusTotal.attackSpeedDown <= 0.0 && (double) status.badStatusTotal.freeze <= 0.0 && (double) status.badStatusTotal.lightRing <= 0.0 && (double) status.badStatusTotal.stone <= 0.0)
      return;
    ++this.badStatusCount;
  }
}
