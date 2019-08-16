using System;

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
		SetBadge((Enum)UI.BTN_SCOUTED, clanInviteNum, 3, -15, -10, is_scale_normalize: false);
	}
}
