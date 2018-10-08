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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (!QuestManager.IsValidInGameWaveMatch(false))
		{
			this.get_gameObject().SetActive(false);
		}
		else
		{
			this.get_gameObject().SetActive(true);
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
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		waveText.get_gameObject().SetActive(isShow);
	}
}
