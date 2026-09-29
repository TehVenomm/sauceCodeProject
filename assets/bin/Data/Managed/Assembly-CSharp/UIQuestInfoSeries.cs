// Decompiled with JetBrains decompiler
// Type: UIQuestInfoSeries
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIQuestInfoSeries : MonoBehaviourSingleton<UIQuestInfoSeries>
{
  [SerializeField]
  private UILabel timeSeriesText;
  [SerializeField]
  private UILabel seriesMax;
  [SerializeField]
  private UILabel seriesNow;
  private InGameProgress m_inGameProgress;
  private QuestManager m_questMgr;
  private int m_series;

  private void Start()
  {
    this.m_inGameProgress = MonoBehaviourSingleton<InGameProgress>.I;
    this.m_questMgr = MonoBehaviourSingleton<QuestManager>.I;
    if (!this.m_questMgr.IsCurrentQuestTypeSeries() && !this.m_questMgr.IsCurrentQuestTypeSeriesArena())
    {
      ((Component) this).gameObject.SetActive(false);
    }
    else
    {
      ((Component) this).gameObject.SetActive(true);
      this.seriesMax.text = $"/{this.m_questMgr.GetCurrentQuestSeriesNum()}";
      this.m_series = (int) this.m_questMgr.currentQuestSeriesIndex + 1;
      this.SetSeriesNow(this.m_series);
    }
  }

  private void LateUpdate()
  {
    this.timeSeriesText.text = this.m_inGameProgress.GetRemainTime();
    if (this.m_series == (int) this.m_questMgr.currentQuestSeriesIndex + 1)
      return;
    this.m_series = (int) this.m_questMgr.currentQuestSeriesIndex + 1;
    this.SetSeriesNow(this.m_series);
  }

  public void SetSeriesNow(int num) => this.seriesNow.text = num.ToString();
}
