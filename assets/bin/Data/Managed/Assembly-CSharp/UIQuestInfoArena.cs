using UnityEngine;

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
		if (!IsArena())
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		base.gameObject.SetActive(value: true);
		m_inGameMgr = MonoBehaviourSingleton<InGameManager>.I;
		m_inGameProgress = MonoBehaviourSingleton<InGameProgress>.I;
		waveMax.text = $"/{m_inGameMgr.GetArenaWaveMax()}";
		m_wave = m_inGameMgr.GetCurrentArenaWaveNum();
		SetWaveNow(m_wave);
		m_isTimeAttack = m_inGameMgr.IsArenaTimeAttack();
	}

	private void LateUpdate()
	{
		string text = m_isTimeAttack ? m_inGameProgress.GetArenaElapseTimeToString() : m_inGameProgress.GetArenaRemainTimeToString();
		timeArenaText.text = text;
		if (m_wave != m_inGameMgr.GetCurrentArenaWaveNum())
		{
			m_wave = m_inGameMgr.GetCurrentArenaWaveNum();
			SetWaveNow(m_wave);
		}
	}

	private bool IsArena()
	{
		if (QuestManager.IsValidInGame())
		{
			return MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo();
		}
		return false;
	}

	public void SetWaveNow(int num)
	{
		waveNow.text = num.ToString();
	}
}
