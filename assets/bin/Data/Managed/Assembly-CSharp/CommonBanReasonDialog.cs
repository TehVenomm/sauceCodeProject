using Network;

public class CommonBanReasonDialog : CommonDialog
{
	protected new enum UI
	{
		MESSAGE,
		SPR_BTN_0,
		LBL_BTN_0,
		LBL_BTN_0_R,
		SPR_BTN_1,
		LBL_BTN_1,
		LBL_BTN_1_R,
		SPR_BTN_2,
		LBL_BTN_2,
		LBL_BTN_2_R,
		OBJ_SPACE,
		OBJ_FRAME,
		TBL_BTN,
		BG,
		HEADER,
		CLOSE_BTN,
		LBL_TITLE,
		LBL_TITLE_U,
		LBL_TITLE_D,
		FOOTER,
		IPT_REASON,
		LBL_DEFAULT_INPUT_REASON,
		LBL_USER_KICK,
		LBL_REASON
	}

	private string banReason = string.Empty;

	protected override SoundID.UISE openingSound => SoundID.UISE.DIALOG_IMPTNT;

	public override void Initialize()
	{
		base.Initialize();
		SetInput(base._transform, UI.IPT_REASON, string.Empty, 50, OnChangeGuildName);
	}

	protected override void InitDialog(object data_object)
	{
		base.InitDialog(new Desc(TYPE.DEFAULT, string.Empty));
		FriendCharaInfo friendCharaInfo = data_object as FriendCharaInfo;
		SetLabelText(base._transform, UI.LBL_USER_KICK, $"{friendCharaInfo.name} will be kicked from your clan!");
	}

	protected override string GetTransferUIName()
	{
		return "UI_BanReasonDialog";
	}

	protected void OnChangeGuildName()
	{
		banReason = GetInputValue(base._transform, UI.IPT_REASON);
		MonoBehaviourSingleton<GuildManager>.I.BanReason = banReason;
	}
}
