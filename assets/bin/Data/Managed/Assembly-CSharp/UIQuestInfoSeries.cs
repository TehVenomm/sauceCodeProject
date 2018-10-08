using UnityEngine;

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
		m_inGameProgress = MonoBehaviourSingleton<InGameProgress>.I;
		m_questMgr = MonoBehaviourSingleton<QuestManager>.I;
		if (!m_questMgr.IsCurrentQuestTypeSeries())
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			base.gameObject.SetActive(true);
			seriesMax.text = $"/{m_questMgr.GetCurrentQuestSeriesNum()}";
			m_series = (int)(m_questMgr.currentQuestSeriesIndex + 1);
			SetSeriesNow(m_series);
		}
	}

	private void LateUpdate()
	{
		timeSeriesText.text = m_inGameProgress.GetRemainTime();
		if (m_series != (int)(m_questMgr.currentQuestSeriesIndex + 1))
		{
			m_series = (int)(m_questMgr.currentQuestSeriesIndex + 1);
			SetSeriesNow(m_series);
		}
	}

	public void SetSeriesNow(int num)
	{
		seriesNow.text = num.ToString();
	}
}
