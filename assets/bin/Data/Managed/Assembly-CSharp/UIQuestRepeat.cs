using UnityEngine;

public class UIQuestRepeat : MonoBehaviourSingleton<UIQuestRepeat>
{
	[SerializeField]
	protected UILabel repeatStatus;

	[SerializeField]
	protected UIButton repeatOffBtn;

	public void OnVictory()
	{
		repeatStatus.gameObject.SetActive(false);
		repeatOffBtn.gameObject.SetActive(false);
	}

	public void InitData()
	{
		if (PartyManager.IsValidInParty() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
		{
			repeatStatus.gameObject.SetActive(MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
			repeatOffBtn.gameObject.SetActive(MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
		}
	}

	public void OnEndHunt()
	{
		repeatOffBtn.SetState(UIButtonColor.State.Disabled, true);
		MonoBehaviourSingleton<PartyManager>.I.SendRepeat(false, delegate(bool is_success)
		{
			if (is_success)
			{
				repeatStatus.gameObject.SetActive(false);
				repeatOffBtn.gameObject.SetActive(false);
				GameSaveData.instance.defaultRepeatPartyOn = false;
			}
			else
			{
				repeatOffBtn.SetState(UIButtonColor.State.Normal, true);
			}
		});
	}
}
