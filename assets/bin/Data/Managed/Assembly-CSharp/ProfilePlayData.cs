// Decompiled with JetBrains decompiler
// Type: ProfilePlayData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ProfilePlayData : GameSection
{
  private const int NUM_BASE = 1000;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "PlayDataTable";
    }
  }

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    base.UpdateUI();
    PlayDataTable.PlayData[] nameList = Singleton<PlayDataTable>.I.GetSortedPlayData(MonoBehaviourSingleton<AchievementManager>.I.GetAchievementCounterList().ToArray());
    this.SetGrid((Enum) ProfilePlayData.UI.GRD_LIST, "ProfilePlaydataListItem", nameList.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      PlayDataTable.PlayData playData = nameList[i];
      this.SetLabelText(t, (Enum) ProfilePlayData.UI.LBL_NAME, playData.name);
      this.SetLabelText(t, (Enum) ProfilePlayData.UI.LBL_NUM, string.Format(playData.format, (object) playData.count));
    }));
  }

  private enum UI
  {
    GRD_LIST,
    LBL_NAME,
    LBL_NUM,
  }
}
