// Decompiled with JetBrains decompiler
// Type: MissionCheckDead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class MissionCheckDead : MissionCheckBase
{
  private bool isDead;

  public override bool IsMissionClear() => !this.isDead;

  public override void OnDamage(AttackedHitStatusFix status, Character to_obj)
  {
    if (status.reactionType != 8)
      return;
    switch (this.missionRequire)
    {
      case MISSION_REQUIRE.SELF:
        if (Object.op_Equality((Object) (to_obj as Self), (Object) null))
          break;
        this.isDead = true;
        break;
      case MISSION_REQUIRE.ALL:
        if (Object.op_Equality((Object) (to_obj as Player), (Object) null) || MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList.Contains((StageObject) to_obj))
          break;
        this.isDead = true;
        break;
    }
  }
}
