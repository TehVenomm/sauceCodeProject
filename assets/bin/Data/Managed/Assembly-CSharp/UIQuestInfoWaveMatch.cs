using UnityEngine;

public class UIQuestInfoWaveMatch : MonoBehaviourSingleton<UIQuestInfoWaveMatch>
{
	[SerializeField]
	protected UILabel timeText;

	[SerializeField]
	protected UILabel waveText;

	private InGameProgress m_inGameProgress;

	private int waveNum;

	private bool isShowFraction;

	private void Start()
	{
		if (!QuestManager.IsValidInGameWaveMatch(false))
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			base.gameObject.SetActive(true);
			m_inGameProgress = MonoBehaviourSingleton<InGameProgress>.I;
			isShowFraction = QuestManager.IsValidInGameWaveMatch(true);
			if (isShowFraction)
			{
				ShowWave(false);
			}
			else
			{
				SetWaveNow(1, 0, false);
			}
		}
	}

	private void LateUpdate()
	{
		timeText.text = MonoBehaviourSingleton<InGameProgress>.I.GetRemainTime();
	}

	public void SetWaveNow(int num, int finalNum, bool isFinal)
	{
		if (waveNum != num)
		{
			waveNum = num;
			if (isFinal)
			{
				waveText.text = "Final Wave";
			}
			else if (isShowFraction && finalNum != 0)
			{
				waveText.text = $"Wave {waveNum:D2}/{finalNum:D2}";
			}
			else
			{
				waveText.text = $"Wave {waveNum:D2}";
			}
			ShowWave(true);
		}
	}

	public void ShowWave(bool isShow)
	{
		waveText.gameObject.SetActive(isShow);
	}
}
