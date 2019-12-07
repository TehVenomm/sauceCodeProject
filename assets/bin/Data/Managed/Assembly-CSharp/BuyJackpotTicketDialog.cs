using UnityEngine;

public class BuyJackpotTicketDialog : GameSection
{
	protected enum UI
	{
		LBL_SELECT_NUM,
		LBL_SELECT_PRICE,
		BTN_SELECT_NUM_MINUS,
		BTN_SELECT_NUM_PLUS,
		SLD_SELECT_NUM,
		SPR_SELECT_FRAME,
		SPR_REACH_LIMIT,
		LBL_REQUEST_LIMIT,
		STR_TITLE_U,
		STR_TITLE_D,
		STR_SELECT_NUM,
		LBL_BUY_BTN,
		LBL_CURRENT_CRYSTAL,
		BTN_BUY,
		SPR_GEM
	}

	private const int MAX_NUMBER_TICKET_PURCHASE = 500;

	private int maxNum;

	private int nowSelected;

	private bool canUpdateUI = true;

	private int jackpotTicketPrice;

	private int numberTicketCanBuy;

	private Transform sprGem;

	private UILabel lblBuy;

	public override bool useOnPressBackKey => true;

	public override void Initialize()
	{
		int crystal = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		jackpotTicketPrice = MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.ticketPrice;
		numberTicketCanBuy = crystal / jackpotTicketPrice;
		maxNum = 500;
		nowSelected = 1;
		SetLabelText(UI.LBL_CURRENT_CRYSTAL, crystal);
		canUpdateUI = false;
		sprGem = GetCtrl(UI.SPR_GEM);
		lblBuy = GetCtrl(UI.LBL_BUY_BTN).GetComponent<UILabel>();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText(UI.STR_TITLE_U, base.sectionData.GetText("STR_TITLE"));
		SetLabelText(UI.STR_TITLE_D, base.sectionData.GetText("STR_TITLE"));
		SetLabelText(UI.STR_SELECT_NUM, base.sectionData.GetText("STR_QUANTITY"));
		SetProgressInt(UI.SLD_SELECT_NUM, nowSelected, 1, maxNum, OnChagenSlider);
	}

	private void OnChagenSlider()
	{
		int progressInt = GetProgressInt(UI.SLD_SELECT_NUM);
		SetLabelText(UI.LBL_SELECT_NUM, string.Format("{0,8:#,0}", progressInt));
		SetLabelText(UI.LBL_BUY_BTN, string.Format(StringTable.Get(STRING_CATEGORY.DRAGON_VAULT, 4u), progressInt * jackpotTicketPrice));
		sprGem.localPosition = new Vector2(lblBuy.printedSize.x / 2f + 15f, 3f);
	}

	private void OnQuery_SELECT_NUM_MINUS()
	{
		SetProgressInt(UI.SLD_SELECT_NUM, GetProgressInt(UI.SLD_SELECT_NUM) - 1);
	}

	private void OnQuery_SELECT_NUM_PLUS()
	{
		SetProgressInt(UI.SLD_SELECT_NUM, GetProgressInt(UI.SLD_SELECT_NUM) + 1);
	}

	protected int GetSliderNum()
	{
		return GetProgressInt(UI.SLD_SELECT_NUM);
	}

	private void OnQuery_BUY_TICKET()
	{
		int sliderNum = GetSliderNum();
		if ((float)(sliderNum * jackpotTicketPrice) > (float)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal)
		{
			DispatchEvent("BUY_GEM", base.sectionData.GetText("STR_COMFIRM_BUY_GEM"));
		}
		else if (sliderNum <= 500)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<FortuneWheelManager>.I.BuyTicket(sliderNum, delegate(bool b)
			{
				GameSection.ResumeEvent(is_resume: true);
				DispatchEvent("JACKPOT_BUY_MESSAGE", base.sectionData.GetText(b ? "STR_BUY_SUCCESS" : "STR_BUY_FAILED"));
				MonoBehaviourSingleton<FortuneWheelManager>.I.RequestUpdateUI();
			});
			SetButtonEnabled(UI.BTN_BUY, is_enabled: false);
		}
	}

	private void OnQuery_CANCEL()
	{
		GameSection.BackSection();
	}

	private void OnQuery_ComfirmBuyGemDialog_YES()
	{
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		int crystal = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		if ((flags & NOTIFY_FLAG.CHANGED_SCENE) != (NOTIFY_FLAG)0L)
		{
			SetLabelText(UI.LBL_CURRENT_CRYSTAL, crystal);
		}
	}

	public override void OnPressBackKey()
	{
		DispatchEvent("[BACK]");
	}
}
