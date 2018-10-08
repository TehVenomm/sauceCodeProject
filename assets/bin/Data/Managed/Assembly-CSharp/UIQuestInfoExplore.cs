using UnityEngine;

public class UIQuestInfoExplore : MonoBehaviourSingleton<UIQuestInfoExplore>
{
	[SerializeField]
	protected GameObject timeExplore;

	[SerializeField]
	protected GameObject timeInBattle;

	[SerializeField]
	protected UILabel timeExploreText;

	[SerializeField]
	protected UILabel timeInBattleText;

	private bool prevExploreBossBattle;

	private void Start()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(IsEnable());
		timeExplore.SetActive(!IsBoss());
		timeInBattle.SetActive(IsBoss());
		prevExploreBossBattle = IsBoss();
	}

	private void LateUpdate()
	{
		if (prevExploreBossBattle != IsBoss())
		{
			timeExplore.SetActive(!IsBoss());
			timeInBattle.SetActive(IsBoss());
		}
		string remainTime = MonoBehaviourSingleton<InGameProgress>.I.GetRemainTime();
		timeInBattleText.text = remainTime;
		timeExploreText.text = remainTime;
		prevExploreBossBattle = IsBoss();
	}

	private bool IsEnable()
	{
		return QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.IsExplore();
	}

	private bool IsBoss()
	{
		return MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap();
	}
}
