// Decompiled with JetBrains decompiler
// Type: MissionCheckDownCount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class MissionCheckDownCount : MissionCheckBase
{
  private int downCount;

  public override bool IsMissionClear() => this.downCount >= this.missionParam;

  public override void OnDamage(AttackedHitStatusFix status, Character to_obj)
  {
    if (Object.op_Equality((Object) (to_obj as Enemy), (Object) null) || status.reactionType != 7)
      return;
    ++this.downCount;
  }

  public void SetCount(int count) => this.downCount = count;
}
