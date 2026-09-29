// Decompiled with JetBrains decompiler
// Type: UIQuestInfoExplore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIQuestInfoExplore : MonoBehaviourSingleton<UIQuestInfoExplore>
{
  [SerializeField]
  protected GameObject timeExplore;
  [SerializeField]
  protected GameObject timeInBattle;
  [SerializeField]
  protected UILabel timeExploreText;
  [SerializeField]
  protected UILabel timeInBattleText;
  private bool prevExploreBossBattle;

  private void Start()
  {
    ((Component) this).gameObject.SetActive(this.IsEnable());
    this.timeExplore.SetActive(!this.IsBoss());
    this.timeInBattle.SetActive(this.IsBoss());
    this.prevExploreBossBattle = this.IsBoss();
  }

  private void LateUpdate()
  {
    if (this.prevExploreBossBattle != this.IsBoss())
    {
      this.timeExplore.SetActive(!this.IsBoss());
      this.timeInBattle.SetActive(this.IsBoss());
    }
    string remainTime = MonoBehaviourSingleton<InGameProgress>.I.GetRemainTime();
    this.timeInBattleText.text = remainTime;
    this.timeExploreText.text = remainTime;
    this.prevExploreBossBattle = this.IsBoss();
  }

  private bool IsEnable()
  {
    return QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsExplore();
  }

  private bool IsBoss() => MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap();
}
