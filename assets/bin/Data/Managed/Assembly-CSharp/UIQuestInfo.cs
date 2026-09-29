// Decompiled with JetBrains decompiler
// Type: UIQuestInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIQuestInfo : MonoBehaviourSingleton<UIQuestInfo>
{
  [SerializeField]
  protected UILabel questName;
  [SerializeField]
  protected UILabel timeLabel;

  private void Start()
  {
    if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo() || QuestManager.IsValidInGameWaveMatch() || QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameSeriesArena())
    {
      ((Component) this).gameObject.SetActive(false);
    }
    else
    {
      if (QuestManager.IsValidInGame() && !MonoBehaviourSingleton<InGameManager>.I.IsRush())
      {
        if (Object.op_Inequality((Object) this.questName, (Object) null))
        {
          string currentQuestName = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestName();
          if (!string.IsNullOrEmpty(currentQuestName))
            this.questName.text = currentQuestName;
        }
      }
      else
        ((Component) this).gameObject.SetActive(false);
      if (!MonoBehaviourSingleton<QuestManager>.I.IsExplore() && !MonoBehaviourSingleton<InGameManager>.I.IsRush())
        return;
      ((Component) this).gameObject.SetActive(false);
    }
  }

  private void LateUpdate()
  {
    this.timeLabel.text = MonoBehaviourSingleton<InGameProgress>.I.GetRemainTime();
  }
}
