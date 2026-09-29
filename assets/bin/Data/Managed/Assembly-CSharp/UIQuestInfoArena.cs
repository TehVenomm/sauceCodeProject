// Decompiled with JetBrains decompiler
// Type: UIQuestInfoArena
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIQuestInfoArena : MonoBehaviourSingleton<UIQuestInfoArena>
{
  [SerializeField]
  protected UILabel timeArenaText;
  [SerializeField]
  protected UILabel waveMax;
  [SerializeField]
  protected UILabel waveNow;
  private InGameManager m_inGameMgr;
  private InGameProgress m_inGameProgress;
  private int m_wave;
  private bool m_isTimeAttack;

  private void Start()
  {
    if (!this.IsArena())
    {
      ((Component) this).gameObject.SetActive(false);
    }
    else
    {
      ((Component) this).gameObject.SetActive(true);
      this.m_inGameMgr = MonoBehaviourSingleton<InGameManager>.I;
      this.m_inGameProgress = MonoBehaviourSingleton<InGameProgress>.I;
      this.waveMax.text = $"/{this.m_inGameMgr.GetArenaWaveMax()}";
      this.m_wave = this.m_inGameMgr.GetCurrentArenaWaveNum();
      this.SetWaveNow(this.m_wave);
      this.m_isTimeAttack = this.m_inGameMgr.IsArenaTimeAttack();
    }
  }

  private void LateUpdate()
  {
    this.timeArenaText.text = this.m_isTimeAttack ? this.m_inGameProgress.GetArenaElapseTimeToString() : this.m_inGameProgress.GetArenaRemainTimeToString();
    if (this.m_wave == this.m_inGameMgr.GetCurrentArenaWaveNum())
      return;
    this.m_wave = this.m_inGameMgr.GetCurrentArenaWaveNum();
    this.SetWaveNow(this.m_wave);
  }

  private bool IsArena()
  {
    return QuestManager.IsValidInGame() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo();
  }

  public void SetWaveNow(int num) => this.waveNow.text = num.ToString();
}
