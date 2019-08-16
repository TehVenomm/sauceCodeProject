using UnityEngine;

public class UIQuestRepeat : MonoBehaviourSingleton<UIQuestRepeat>
{
	[SerializeField]
	protected UILabel repeatStatus;

	[SerializeField]
	protected UIButton repeatOffBtn;

	public void OnVictory()
	{
		repeatStatus.get_gameObject().SetActive(false);
		repeatOffBtn.get_gameObject().SetActive(false);
	}

	public void InitData()
	{
		if (PartyManager.IsValidInParty() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
		{
			repeatStatus.get_gameObject().SetActive(MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
			repeatOffBtn.get_gameObject().SetActive(MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
		}
	}

	public void OnEndHunt()
	{
		repeatOffBtn.SetState(UIButtonColor.State.Disabled, immediate: true);
		MonoBehaviourSingleton<PartyManager>.I.SendRepeat(isOn: false, delegate(bool is_success)
		{
			if (is_success)
			{
				repeatStatus.get_gameObject().SetActive(false);
				repeatOffBtn.get_gameObject().SetActive(false);
				GameSaveData.instance.defaultRepeatPartyOn = false;
			}
			else
			{
				repeatOffBtn.SetState(UIButtonColor.State.Normal, immediate: true);
			}
		});
	}
}
