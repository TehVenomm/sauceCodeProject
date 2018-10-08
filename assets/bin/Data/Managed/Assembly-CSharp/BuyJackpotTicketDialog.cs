using System;
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
		SetLabelText((Enum)UI.STR_TITLE_U, base.sectionData.GetText("STR_TITLE"));
		SetLabelText((Enum)UI.STR_TITLE_D, base.sectionData.GetText("STR_TITLE"));
		SetLabelText((Enum)UI.STR_SELECT_NUM, base.sectionData.GetText("STR_QUANTITY"));
		SetProgressInt((Enum)UI.SLD_SELECT_NUM, nowSelected, 1, maxNum, (EventDelegate.Callback)OnChagenSlider);
	}

	private void OnChagenSlider()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		int progressInt = GetProgressInt((Enum)UI.SLD_SELECT_NUM);
		SetLabelText((Enum)UI.LBL_SELECT_NUM, string.Format("{0,8:#,0}", progressInt));
		SetLabelText((Enum)UI.LBL_BUY_BTN, string.Format(StringTable.Get(STRING_CATEGORY.DRAGON_VAULT, 4u), progressInt * jackpotTicketPrice));
		Transform obj = sprGem;
		Vector2 printedSize = lblBuy.printedSize;
		obj.set_localPosition(Vector2.op_Implicit(new Vector2(printedSize.x / 2f + 15f, 3f)));
	}

	private void OnQuery_SELECT_NUM_MINUS()
	{
		SetProgressInt((Enum)UI.SLD_SELECT_NUM, GetProgressInt((Enum)UI.SLD_SELECT_NUM) - 1, -1, -1, (EventDelegate.Callback)null);
	}

	private void OnQuery_SELECT_NUM_PLUS()
	{
		SetProgressInt((Enum)UI.SLD_SELECT_NUM, GetProgressInt((Enum)UI.SLD_SELECT_NUM) + 1, -1, -1, (EventDelegate.Callback)null);
	}

	protected int GetSliderNum()
	{
		return GetProgressInt((Enum)UI.SLD_SELECT_NUM);
	}

	private void OnQuery_BUY_TICKET()
	{
		int sliderNum = GetSliderNum();
		float num = (float)(sliderNum * jackpotTicketPrice);
		if (num > (float)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal)
		{
			DispatchEvent("BUY_GEM", base.sectionData.GetText("STR_COMFIRM_BUY_GEM"));
		}
		else if (sliderNum <= 500)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<FortuneWheelManager>.I.BuyTicket(sliderNum, delegate(bool b)
			{
				GameSection.ResumeEvent(true, null);
				DispatchEvent("JACKPOT_BUY_MESSAGE", base.sectionData.GetText((!b) ? "STR_BUY_FAILED" : "STR_BUY_SUCCESS"));
				MonoBehaviourSingleton<FortuneWheelManager>.I.RequestUpdateUI();
			});
			SetButtonEnabled((Enum)UI.BTN_BUY, false);
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
}
