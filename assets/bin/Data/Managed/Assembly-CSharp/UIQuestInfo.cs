using UnityEngine;

public class UIQuestInfo : MonoBehaviourSingleton<UIQuestInfo>
{
	[SerializeField]
	protected UILabel questName;

	[SerializeField]
	protected UILabel timeLabel;

	private void Start()
	{
		if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo() || QuestManager.IsValidInGameWaveMatch(false) || QuestManager.IsValidInGameSeries())
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			if (QuestManager.IsValidInGame() && !MonoBehaviourSingleton<InGameManager>.I.IsRush())
			{
				if ((Object)questName != (Object)null)
				{
					string currentQuestName = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestName();
					if (!string.IsNullOrEmpty(currentQuestName))
					{
						questName.text = currentQuestName;
					}
				}
			}
			else
			{
				base.gameObject.SetActive(false);
			}
			if (MonoBehaviourSingleton<QuestManager>.I.IsExplore() || MonoBehaviourSingleton<InGameManager>.I.IsRush())
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	private void LateUpdate()
	{
		timeLabel.text = MonoBehaviourSingleton<InGameProgress>.I.GetRemainTime();
	}
}
