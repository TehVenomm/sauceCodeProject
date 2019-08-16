using UnityEngine;

public class ItemStorageSell : ItemStorageTop
{
	private enum UI
	{
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		SPR_SCR_BAR,
		SCR_INVENTORY_EQUIP,
		GRD_INVENTORY_EQUIP,
		GRD_INVENTORY_EQUIP_SMALL,
		SPR_EQUIP_SCR_BAR,
		TGL_CHANGE_INVENTORY,
		TGL_ICON_ASC,
		LBL_SORT,
		BTN_SORT,
		SPR_INVALID_SORT,
		LBL_INVALID_SORT,
		BTN_CHANGE,
		SPR_INVALID_CHANGE,
		TGL_TAB0,
		TGL_TAB1,
		TGL_TAB2,
		TGL_TAB3,
		TGL_TAB4,
		TGL_TAB5,
		OBJ_BTN_SELL_MODE,
		OBJ_SELL_MODE_ROOT,
		LBL_MAX_SELECT_NUM,
		LBL_SELECT_NUM,
		LBL_TOTAL,
		LBL_MAX_HAVE_NUM,
		LBL_NOW_HAVE_NUM,
		OBJ_CAPTION_3,
		LBL_CAPTION
	}

	public override void Initialize()
	{
		confirmTo = ItemStorageSellConfirm.GO_BACK.SELL;
		InitializeCaption();
		base.Initialize();
	}

	protected override void ToDetail()
	{
		SaveCurrentScrollPosition();
		int num = (int)GameSection.GetEventData();
		sellItemData.Clear();
		SortCompareData sortCompareData = inventories[(int)tab].datas[num];
		if (!sortCompareData.CanSale())
		{
			if (sortCompareData.IsFavorite())
			{
				GameSection.ChangeEvent("NOT_SELL_FAVORITE");
			}
			else if (sortCompareData.IsHomeEquipping())
			{
				GameSection.ChangeEvent("NOT_SELL_EQUIPPING");
			}
			else if (sortCompareData.IsUniqueEquipping())
			{
				GameSection.ChangeEvent("NOT_SELL_UNIQUE_EQUIPPING");
			}
			else
			{
				GameSection.ChangeEvent("CAN_NOT_SELL");
			}
		}
		else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && sortCompareData.GetTableID() == 10000000)
		{
			GameSection.ChangeEvent("NOT_SELL_DEFAULT_WEAPON");
		}
		else if (tab == TAB_MODE.MATERIAL)
		{
			GameSection.ChangeEvent("SELECT", sortCompareData);
		}
		else
		{
			sellItemData.Add(inventories[(int)tab].datas[num]);
			GameSection.ChangeEvent("EQUIP_SELECT");
			OnQuery_SELL();
		}
	}

	private void OnCloseDialog_ItemStorageSellConfirm()
	{
		if (!isSellMode)
		{
			sellItemData.Clear();
		}
	}

	private new void InitializeCaption()
	{
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_3);
		string text = base.sectionData.GetText("CAPTION");
		SetLabelText(ctrl, UI.LBL_CAPTION, text);
		UITweenCtrl component = ctrl.get_gameObject().GetComponent<UITweenCtrl>();
		if (component != null)
		{
			component.Reset();
			int i = 0;
			for (int num = component.tweens.Length; i < num; i++)
			{
				component.tweens[i].ResetToBeginning();
			}
			component.Play();
		}
	}

	protected void OnQuery_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		if (num >= 0 && IsEnableShowDetailByLongTap())
		{
			SaveCurrentScrollPosition();
			if (tab == TAB_MODE.EQUIP)
			{
				GameSection.ChangeEvent("DETAIL_EQUIP", new object[2]
				{
					ItemDetailEquip.CURRENT_SECTION.SMITH_SELL,
					inventories[(int)tab].datas[num]
				});
			}
			else if (tab == TAB_MODE.SKILL)
			{
				ItemDetailSkillSimpleDialog.InitParam event_data = new ItemDetailSkillSimpleDialog.InitParam(new object[2]
				{
					ItemDetailEquip.CURRENT_SECTION.SMITH_SELL,
					inventories[(int)tab].datas[num]
				}, null);
				GameSection.ChangeEvent("DETAIL_SKILL", event_data);
			}
		}
	}

	private bool IsEnableShowDetailByLongTap()
	{
		return tab == TAB_MODE.EQUIP || tab == TAB_MODE.SKILL;
	}

	protected override bool IsRequiredIconGrayOut(SortCompareData _data)
	{
		if (_data.GetNum() == 0)
		{
			return true;
		}
		if (_data.IsFavorite())
		{
			return true;
		}
		if (_data.IsEquipping() && tab == TAB_MODE.EQUIP)
		{
			return true;
		}
		return false;
	}
}
