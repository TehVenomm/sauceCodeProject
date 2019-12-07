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
			SetActive(UI.BTN_CENTER, is_visible: false);
			SetActive(UI.BTN_0, is_visible: true);
			SetActive(UI.BTN_1, is_visible: true);
		}
	}

	protected override void DrawIcon()
	{
		base.DrawIcon();
		EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipData.GetTableID());
		lithographArr = new ItemTable.ItemData[1]
		{
			equipItemData.GetRootLithograph()
		};
		int sELL_SELECT_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX;
		SetGrid(UI.GRD_REWARD_ICON, null, sELL_SELECT_MAX, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			if (i < lithographArr.Length)
			{
				ItemTable.ItemData itemData = lithographArr[i];
				ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.ITEM, itemData.id, t, 1, "NONE");
				itemIcon.SetRewardBG(is_visible: true);
				Transform ctrl = GetCtrl(UI.GRD_REWARD_ICON);
				SetMaterialInfo(itemIcon.transform, REWARD_TYPE.ITEM, itemData.id, ctrl);
			}
			else
			{
				SetActive(t, is_visible: false);
			}
		});
		SetActive(UI.STR_NON_REWARD, lithographArr.Length == 0);
	}

	public void OnQuery_YES()
	{
		GameSection.SetEventData(string.Format(StringTable.Get(STRING_CATEGORY.SMITH, 12u), MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SMITH_RESTORE_USE_CRYSTAL));
	}

	private void OnQuery_SmithRevertLithographConfirm_YES()
	{
		if (GameSection.CheckCrystal(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SMITH_RESTORE_USE_CRYSTAL))
		{
			GameSection.SetEventData(StringTable.Format(STRING_CATEGORY.SMITH, 13u, lithographArr[0].name));
			GameSection.StayEvent();
			MonoBehaviourSingleton<SmithManager>.I.SendRevertLithograph(equipData.GetUniqID(), delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success);
			});
		}
	}
}
