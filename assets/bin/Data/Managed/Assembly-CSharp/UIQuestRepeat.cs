using UnityEngine;

public class UIQuestRepeat : MonoBehaviourSingleton<UIQuestRepeat>
{
	[SerializeField]
	protected UILabel repeatStatus;

	[SerializeField]
	protected UIButton repeatOffBtn;

	public void OnVictory()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		repeatStatus.get_gameObject().SetActive(false);
		repeatOffBtn.get_gameObject().SetActive(false);
	}

	public void InitData()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (PartyManager.IsValidInParty() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
		{
			repeatStatus.get_gameObject().SetActive(MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
			repeatOffBtn.get_gameObject().SetActive(MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
		}
	}

	public void OnEndHunt()
	{
		repeatOffBtn.SetState(UIButtonColor.State.Disabled, true);
		MonoBehaviourSingleton<PartyManager>.I.SendRepeat(false, delegate(bool is_success)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			if (is_success)
			{
				repeatStatus.get_gameObject().SetActive(false);
				repeatOffBtn.get_gameObject().SetActive(false);
				GameSaveData.instance.defaultRepeatPartyOn = false;
			}
			else
			{
				repeatOffBtn.SetState(UIButtonColor.State.Normal, true);
			}
		});
	}
}
