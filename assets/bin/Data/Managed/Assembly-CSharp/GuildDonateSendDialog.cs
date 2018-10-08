using Network;
using System;

public class GuildDonateSendDialog : GameSection
{
	protected enum UI
	{
		LBL_SELECT_NUM,
		LBL_SELECT_PRICE,
		BTN_SELECT_NUM_MINUS,
		BTN_SELECT_NUM_PLUS,
		SLD_SELECT_NUM,
		SPR_SELECT_FRAME,
		LBL_NUMBER_REQUEST,
		SPR_REACH_LIMIT,
		LBL_REQUEST_LIMIT,
		STR_TITLE_U,
		STR_TITLE_D,
		STR_SELECT_NUM,
		LBL_CAPTION
	}

	private DonateInfo _info;

	private int m_maxNum;

	private int m_nowSelect;

	private bool canUpdateUI = true;

	public override void Initialize()
	{
		_info = (GameSection.GetEventData() as DonateInfo);
		if (_info != null)
		{
			SetActive(base._transform, UI.SPR_SELECT_FRAME, true);
			SetActive(base._transform, UI.SPR_REACH_LIMIT, false);
			SetActive(base._transform, UI.LBL_NUMBER_REQUEST, false);
			canUpdateUI = true;
			int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == _info.itemId, 1, false);
			int num = _info.quantity - _info.itemNum;
			m_maxNum = ((itemNum < num) ? itemNum : num);
			m_nowSelect = m_maxNum;
		}
		else if (MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateCap < MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap)
		{
			m_maxNum = MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap - MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateCap;
			m_nowSelect = 1;
			canUpdateUI = true;
			SetActive(base._transform, UI.SPR_SELECT_FRAME, true);
			SetActive(base._transform, UI.SPR_REACH_LIMIT, false);
			SetActive(base._transform, UI.LBL_NUMBER_REQUEST, true);
			SetSupportEncoding(UI.LBL_NUMBER_REQUEST, true);
			SetLabelText((Enum)UI.LBL_NUMBER_REQUEST, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 32u), MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateCap, MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap));
		}
		else
		{
			SetActive(base._transform, UI.SPR_SELECT_FRAME, false);
			SetActive(base._transform, UI.SPR_REACH_LIMIT, true);
			SetSupportEncoding(UI.LBL_REQUEST_LIMIT, true);
			SetLabelText((Enum)UI.LBL_REQUEST_LIMIT, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 33u), MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap, MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap));
			canUpdateUI = false;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (canUpdateUI)
		{
			string key = "TEXT_SELECT";
			SetLabelText((Enum)UI.LBL_CAPTION, base.sectionData.GetText(key));
			SetLabelText((Enum)UI.STR_TITLE_U, base.sectionData.GetText(key));
			SetLabelText((Enum)UI.STR_TITLE_D, base.sectionData.GetText(key));
			string key2 = "TEXT_SELECT_NUM";
			SetLabelText((Enum)UI.STR_SELECT_NUM, base.sectionData.GetText(key2));
			SetProgressInt((Enum)UI.SLD_SELECT_NUM, m_nowSelect, 0, m_maxNum, (EventDelegate.Callback)OnChagenSlider);
		}
	}

	private void OnChagenSlider()
	{
		int progressInt = GetProgressInt((Enum)UI.SLD_SELECT_NUM);
		SetLabelText((Enum)UI.LBL_SELECT_NUM, string.Format("{0,8:#,0}", progressInt));
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

	private void OnQuery_SELECT()
	{
		if (_info != null)
		{
			int num = GetSliderNum();
			if (num > 0)
			{
				GameSection.StayEvent();
				MonoBehaviourSingleton<GuildManager>.I.SendDonateSend(_info.id, num, delegate
				{
					if (MonoBehaviourSingleton<GuildManager>.I.donateInviteList != null)
					{
						int count = MonoBehaviourSingleton<GuildManager>.I.donateInviteList.Count;
						for (int i = 0; i < count; i++)
						{
							if (MonoBehaviourSingleton<GuildManager>.I.donateInviteList[i].id == _info.id)
							{
								MonoBehaviourSingleton<GuildManager>.I.donateInviteList[i].itemNum += num;
								if (MonoBehaviourSingleton<GuildManager>.I.donateInviteList[i].itemNum >= MonoBehaviourSingleton<GuildManager>.I.donateInviteList[i].quantity)
								{
									MonoBehaviourSingleton<GuildManager>.I.donateInviteList.RemoveAt(i);
								}
								break;
							}
						}
					}
					MonoBehaviourSingleton<GuildManager>.I.SendDonateList(delegate(bool donate_success)
					{
						GameSection.ResumeEvent(donate_success, null);
						GameSection.BackSection();
					});
				});
			}
			else
			{
				GameSection.BackSection();
			}
		}
		else
		{
			GameSection.SetEventData(GetSliderNum().ToString());
			GameSection.BackSection();
		}
	}

	private void OnQuery_CLOSE()
	{
		GameSection.SetEventData("0");
		GameSection.BackSection();
	}
}
