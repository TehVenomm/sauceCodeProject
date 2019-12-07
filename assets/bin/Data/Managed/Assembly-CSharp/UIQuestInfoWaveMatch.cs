using UnityEngine;

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
			base.gameObject.SetActive(value: false);
			return;
		}
		base.gameObject.SetActive(value: true);
		m_inGameProgress = MonoBehaviourSingleton<InGameProgress>.I;
		isShowFraction = QuestManager.IsValidInGameWaveMatch(isOnlyEvent: true);
		remainingTimeForAlert = MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.remainingTimeForAlert;
		tweenCtrl = timeRoot.GetComponent<UITweenCtrl>();
		if (tweenCtrl != null)
		{
			tweenCtrl.Reset();
		}
		isEnableAlert = (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsWaveStrategyMatch() && (bool)tweenCtrl);
		if (isShowFraction)
		{
			ShowWave(isShow: false);
		}
		else
		{
			SetWaveNow(1, 0, isFinal: false);
		}
	}

	private void LateUpdate()
	{
		timeText.text = MonoBehaviourSingleton<InGameProgress>.I.GetRemainTime();
		if (isEnableAlert)
		{
			if (MonoBehaviourSingleton<InGameProgress>.I.remaindTime <= 0f)
			{
				tweenCtrl.Reset();
			}
			else if (MonoBehaviourSingleton<InGameProgress>.I.remaindTime <= remainingTimeForAlert)
			{
				tweenCtrl.Play();
			}
		}
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
			ShowWave(isShow: true);
		}
	}

	public void ShowWave(bool isShow)
	{
		waveText.gameObject.SetActive(isShow);
	}
}
