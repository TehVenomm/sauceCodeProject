using UnityEngine;

public class UIPhaseNumber : MonoBehaviour
{
	[SerializeField]
	protected UILabel label;

	[SerializeField]
	protected UITweener[] anims;

	public UIPhaseNumber()
		: this()
	{
	}

	protected void Awake()
	{
		label.text = $"Phase {MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex + 1} / {MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestSeriesNum()}";
	}

	private void Update()
	{
		int i = 0;
		for (int num = anims.Length; i < num; i++)
		{
			if (anims[i].get_enabled())
			{
				return;
			}
		}
		Object.Destroy(this.get_gameObject());
	}
}
