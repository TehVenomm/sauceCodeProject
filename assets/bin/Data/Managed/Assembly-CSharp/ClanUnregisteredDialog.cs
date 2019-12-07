using UnityEngine;

public class ClanUnregisteredDialog : ClanUnregisteredDialogBase
{
	private enum UI
	{
		BTN_SCOUTED
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		int clanInviteNum = MonoBehaviourSingleton<UserInfoManager>.I.clanInviteNum;
		SetBadge(UI.BTN_SCOUTED, clanInviteNum, SpriteAlignment.TopRight, -15, -10);
	}
}
