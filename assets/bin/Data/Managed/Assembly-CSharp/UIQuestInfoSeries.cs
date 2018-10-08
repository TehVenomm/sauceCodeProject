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
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		m_inGameProgress = MonoBehaviourSingleton<InGameProgress>.I;
		m_questMgr = MonoBehaviourSingleton<QuestManager>.I;
		if (!m_questMgr.IsCurrentQuestTypeSeries())
		{
			this.get_gameObject().SetActive(false);
		}
		else
		{
			this.get_gameObject().SetActive(true);
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
