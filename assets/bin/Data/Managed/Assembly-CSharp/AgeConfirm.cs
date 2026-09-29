// Decompiled with JetBrains decompiler
// Type: AgeConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class AgeConfirm : GameSection
{
  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    this.SetInput((Enum) AgeConfirm.UI.IPT_AD, "", 4);
    this.SetInput((Enum) AgeConfirm.UI.IPT_MONTH, "", 2);
  }

  private void OnQuery_OK()
  {
    string inputValue1 = this.GetInputValue((Enum) AgeConfirm.UI.IPT_AD);
    string inputValue2 = this.GetInputValue((Enum) AgeConfirm.UI.IPT_MONTH);
    int result1;
    int result2;
    if (inputValue1.Length == 4 && inputValue2.Length >= 1 && int.TryParse(inputValue1, out result1) && int.TryParse(inputValue2, out result2) && result1 > 1800 && result2 >= 1 && result2 <= 12)
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<UserInfoManager>.I.SendBirthday(result1, result2, 1, (Action<bool>) (is_success =>
      {
        if (!is_success)
          GameSection.ChangeStayEvent("ERROR");
        GameSection.ResumeEvent(true);
      }));
    }
    else
      GameSection.ChangeEvent("ERROR");
  }

  private void OnQuery_SECTION_BACK()
  {
    List<GameSectionHistory.HistoryData> historyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList();
    if (historyList.Count <= 1)
      return;
    int count = historyList.Count;
    GameSection.ChangeEvent("WARNING_" + historyList[count - 2].sceneName.ToUpper());
  }

  private enum UI
  {
    IPT_AD,
    IPT_MONTH,
  }
}
