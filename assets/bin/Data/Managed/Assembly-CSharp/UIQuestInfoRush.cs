using UnityEngine;

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
		this.get_gameObject().SetActive(IsEnable());
		if (IsEnable())
		{
			int num = QuestTable.GetSameRushQuestData((uint)MonoBehaviourSingleton<InGameManager>.I.rushId).Count - 1;
			int waveNum = MonoBehaviourSingleton<InGameManager>.I.GetWaveNum(0);
			waveMax.text = "/" + string.Format(StringTable.Get(STRING_CATEGORY.RUSH_WAVE, 10004400u), waveNum + num - 1);
			wave = MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum();
			SetWaveNow(wave);
		}
	}

	private void LateUpdate()
	{
		string remainTime = MonoBehaviourSingleton<InGameProgress>.I.GetRemainTime();
		timeRushText.text = remainTime;
		if (wave != MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum())
		{
			wave = MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum();
			SetWaveNow(wave);
		}
	}

	private bool IsEnable()
	{
		return QuestManager.IsValidInGame() && MonoBehaviourSingleton<InGameManager>.I.IsRush();
	}

	public void SetWaveNow(int num)
	{
		waveNow.text = num.ToString();
	}
}
