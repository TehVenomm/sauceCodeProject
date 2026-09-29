// Decompiled with JetBrains decompiler
// Type: UIQuestInfoWaveMatch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIQuestInfoWaveMatch : MonoBehaviourSingleton<UIQuestInfoWaveMatch>
{
  [SerializeField]
  protected UILabel timeText;
  [SerializeField]
  protected UILabel waveText;
  [SerializeField]
  protected GameObject timeRoot;
  private InGameProgress m_inGameProgress;
  private int waveNum;
  private bool isShowFraction;
  private bool isEnableAlert;
  private float remainingTimeForAlert = 60f;
  private UITweenCtrl tweenCtrl;

  private void Start()
  {
    if (!QuestManager.IsValidInGameWaveMatch())
    {
      ((Component) this).gameObject.SetActive(false);
    }
    else
    {
      ((Component) this).gameObject.SetActive(true);
      this.m_inGameProgress = MonoBehaviourSingleton<InGameProgress>.I;
      this.isShowFraction = QuestManager.IsValidInGameWaveMatch(true);
      this.remainingTimeForAlert = MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.remainingTimeForAlert;
      this.tweenCtrl = this.timeRoot.GetComponent<UITweenCtrl>();
      if (Object.op_Inequality((Object) this.tweenCtrl, (Object) null))
        this.tweenCtrl.Reset();
      this.isEnableAlert = MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsWaveStrategyMatch() && Object.op_Implicit((Object) this.tweenCtrl);
      if (this.isShowFraction)
        this.ShowWave(false);
      else
        this.SetWaveNow(1, 0, false);
    }
  }

  private void LateUpdate()
  {
    this.timeText.text = MonoBehaviourSingleton<InGameProgress>.I.GetRemainTime();
    if (!this.isEnableAlert)
      return;
    if ((double) MonoBehaviourSingleton<InGameProgress>.I.remaindTime <= 0.0)
    {
      this.tweenCtrl.Reset();
    }
    else
    {
      if ((double) MonoBehaviourSingleton<InGameProgress>.I.remaindTime > (double) this.remainingTimeForAlert)
        return;
      this.tweenCtrl.Play();
    }
  }

  public void SetWaveNow(int num, int finalNum, bool isFinal)
  {
    if (this.waveNum == num)
      return;
    this.waveNum = num;
    this.waveText.text = !isFinal ? (!this.isShowFraction || finalNum == 0 ? $"Wave {this.waveNum:D2}" : $"Wave {this.waveNum:D2}/{finalNum:D2}") : "Final Wave";
    this.ShowWave(true);
  }

  public void ShowWave(bool isShow) => ((Component) this.waveText).gameObject.SetActive(isShow);
}
