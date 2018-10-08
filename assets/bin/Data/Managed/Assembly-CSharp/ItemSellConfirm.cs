using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSellConfirm : GameSection
{
	public enum UI
	{
		STR_INCLUDE_RARE,
		STR_MAIN_TEXT,
		STR_TITLE_R,
		GRD_ICON,
		LBL_TOTAL,
		OBJ_GOLD,
		BTN_0,
		BTN_1,
		BTN_CENTER,
		SCR_ICON,
		GRD_REWARD_ICON
	}

	protected List<SortCompareData> sellData;

	protected bool isRareConfirm
	{
		get;
		set;
	}

	protected bool isEquipConfirm
	{
		get;
		set;
	}

	protected bool isExceedConfirm
	{
		get;
		set;
	}

	protected bool isExceedEquipmentConfirm
	{
		get;
		set;
	}

	protected bool isHideMainText
	{
		get;
		set;
	}

	protected bool isButtonSingle
	{
		get;
		set;
	}

	protected virtual bool isShowIconBG()
	{
		return true;
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (sellData != null)
		{
			SetLabelText((Enum)UI.STR_TITLE_R, base.sectionData.GetText("STR_TITLE"));
			SetActive((Enum)UI.STR_INCLUDE_RARE, isRareConfirm);
			SetActive((Enum)UI.STR_MAIN_TEXT, !isHideMainText);
			DrawIcon();
			SortCompareData[] array = sellData.ToArray();
			int num = GetSellGold();
			if (num == 0)
			{
				int i = 0;
				for (int num2 = array.Length; i < num2; i++)
				{
					num += array[i].GetSalePrice();
				}
			}
			SetActive((Enum)UI.OBJ_GOLD, num != 0);
			SetLabelText((Enum)UI.LBL_TOTAL, num.ToString());
			if (isButtonSingle)
			{
				SetActive((Enum)UI.BTN_CENTER, true);
				SetActive((Enum)UI.BTN_0, false);
				SetActive((Enum)UI.BTN_1, false);
			}
			else
			{
				SetActive((Enum)UI.BTN_CENTER, false);
				SetActive((Enum)UI.BTN_0, true);
				SetActive((Enum)UI.BTN_1, true);
			}
		}
	}

	protected virtual int GetSellGold()
	{
		return 0;
	}

	protected unsafe virtual void DrawIcon()
	{
		SortCompareData[] sell_data_ary = sellData.ToArray();
		int sELL_SELECT_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX;
		_003CDrawIcon_003Ec__AnonStorey3D3 _003CDrawIcon_003Ec__AnonStorey3D;
		SetGrid(UI.GRD_ICON, null, sELL_SELECT_MAX, false, new Action<int, Transform, bool>((object)_003CDrawIcon_003Ec__AnonStorey3D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected virtual int GetTargetIconNum(SortCompareData[] sell_data_ary, int i)
	{
		SortCompareData sortCompareData = sell_data_ary[i];
		return sortCompareData.GetNum();
	}
}
