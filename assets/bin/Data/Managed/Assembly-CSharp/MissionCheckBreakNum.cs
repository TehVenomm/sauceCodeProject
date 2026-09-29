// Decompiled with JetBrains decompiler
// Type: MissionCheckBreakNum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class MissionCheckBreakNum : MissionCheckBase
{
  public override bool IsMissionClear()
  {
    int num = 0;
    if (MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists != null)
    {
      int index = 0;
      for (int count = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists.Count; index < count; ++index)
        num += MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists[index].Count - 1;
    }
    return num >= this.missionParam;
  }
}
