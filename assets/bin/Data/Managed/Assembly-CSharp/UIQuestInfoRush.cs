// Decompiled with JetBrains decompiler
// Type: UIQuestInfoRush
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIQuestInfoRush : MonoBehaviourSingleton<UIQuestInfoRush>
{
  [SerializeField]
  protected UILabel timeRushText;
  [SerializeField]
  protected UILabel waveMax;
  [SerializeField]
  protected UILabel waveNow;
  private int wave;

  private void Start()
  {
    ((Component) this).gameObject.SetActive(this.IsEnable());
    if (!this.IsEnable())
      return;
    int num = QuestTable.GetSameRushQuestData((uint) MonoBehaviourSingleton<InGameManager>.I.rushId).Count - 1;
    int waveNum = MonoBehaviourSingleton<InGameManager>.I.GetWaveNum(0);
    this.waveMax.text = "/" + string.Format(StringTable.Get(STRING_CATEGORY.RUSH_WAVE, 10004400U), (object) (waveNum + num - 1));
    this.wave = MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum();
    this.SetWaveNow(this.wave);
  }

  private void LateUpdate()
  {
    this.timeRushText.text = MonoBehaviourSingleton<InGameProgress>.I.GetRemainTime();
    if (this.wave == MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum())
      return;
    this.wave = MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum();
    this.SetWaveNow(this.wave);
  }

  private bool IsEnable()
  {
    return QuestManager.IsValidInGame() && MonoBehaviourSingleton<InGameManager>.I.IsRush();
  }

  public void SetWaveNow(int num) => this.waveNow.text = num.ToString();
}
