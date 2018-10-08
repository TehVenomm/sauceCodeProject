using System;
using System.Collections.Generic;
using UnityEngine;

public class SmithRevertLithographDetail : ItemSellConfirm
{
	public new enum UI
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
		GRD_REWARD_ICON,
		STR_NON_REWARD
	}

	private List<string> uniqs = new List<string>();

	private EquipItemSortData equipData;

	private ItemTable.ItemData[] lithographArr;

	public override string overrideBackKeyEvent => "NO";

	protected override bool isShowIconBG()
	{
		return false;
	}

	public override void Initialize()
	{
		equipData = (GameSection.GetEventData() as EquipItemSortData);
		sellData = new List<SortCompareData>
		{
			equipData
		};
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (sellData != null)
		{
			DrawIcon();
			SetActive((Enum)UI.BTN_CENTER, false);
			SetActive((Enum)UI.BTN_0, true);
			SetActive((Enum)UI.BTN_1, true);
		}
	}

	protected unsafe override void DrawIcon()
	{
		base.DrawIcon();
		EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipData.GetTableID());
		lithographArr = new ItemTable.ItemData[1]
		{
			equipItemData.GetRootLithograph()
		};
		int sELL_SELECT_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX;
		SetGrid(UI.GRD_REWARD_ICON, null, sELL_SELECT_MAX, false, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		SetActive((Enum)UI.STR_NON_REWARD, lithographArr.Length == 0);
	}

	public void OnQuery_YES()
	{
		string eventData = string.Format(StringTable.Get(STRING_CATEGORY.SMITH, 12u), MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SMITH_RESTORE_USE_CRYSTAL);
		GameSection.SetEventData(eventData);
	}

	private void OnQuery_SmithRevertLithographConfirm_YES()
	{
		if (GameSection.CheckCrystal(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SMITH_RESTORE_USE_CRYSTAL, 0, true))
		{
			string eventData = StringTable.Format(STRING_CATEGORY.SMITH, 13u, lithographArr[0].name);
			GameSection.SetEventData(eventData);
			GameSection.StayEvent();
			MonoBehaviourSingleton<SmithManager>.I.SendRevertLithograph(equipData.GetUniqID(), delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success, null);
			});
		}
	}
}