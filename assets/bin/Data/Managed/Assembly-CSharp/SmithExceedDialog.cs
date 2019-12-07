using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SmithExceedDialog : GameSection
{
	private enum UI
	{
		SPR_COUNT_0_ON,
		SPR_COUNT_1_ON,
		SPR_COUNT_2_ON,
		SPR_COUNT_3_ON,
		LBL_EXCEED_0,
		LBL_EXCEED_1,
		LBL_EXCEED_2,
		LBL_EXCEED_3,
		SPR_EXCEED_0_ON,
		SPR_EXCEED_1_ON,
		SPR_EXCEED_2_ON,
		SPR_EXCEED_3_ON,
		LBL_USE_MATERIAL_NAME,
		SCR_LAPIS_ROOT,
		GRD_LAPIS,
		OBJ_VALID_EXCEED_ROOT,
		OBJ_INVALID_EXCEED_ROOT,
		LBL_MAX_COUNT,
		OBJ_MATERIAL_ICON_ROOT,
		SPR_EXCEED_BTN_BG,
		SPR_EXCEED_GRAYOUT,
		OBJ_SELECT,
		BTN_AIM_R,
		BTN_AIM_L,
		BTN_AIM_R_INACTIVE,
		BTN_AIM_L_INACTIVE,
		LBL_SELECT_NOW,
		LBL_SELECT_MAX,
		OBJ_EXCEED_LIMITED,
		LBL_LIMITED
	}

	protected int exceedCount;

	protected EquipItemTable.EquipItemData itemTable;

	private EquipItemExceedTable.EquipItemExceedData exceedData;

	private SmithManager.SmithGrowData smithData;

	private int selectIndex;

	private Color paramColor = Color.white;

	private int selectPageIndex;

	private int maxPageIndex;

	private const int MAX_SHOW_EXCEED_NUM = 3;

	private int need_select_index;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "EquipItemExceedTable";
			yield return "LimitedEquipItemExceedTable";
		}
	}

	protected virtual bool IsValidExceedSection()
	{
		return true;
	}

	protected virtual void SetupExceedData()
	{
		exceedCount = smithData.selectEquipData.exceed;
		itemTable = smithData.selectEquipData.tableData;
	}

	public override void Initialize()
	{
		smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		SetupExceedData();
		exceedData = Singleton<EquipItemExceedTable>.I.GetEquipItemExceedDataIncludeLimited(itemTable);
		selectPageIndex = 0;
		int num = Mathf.CeilToInt((float)exceedData.exceed.Length / 3f);
		maxPageIndex = num - 1;
		SetLabelText(UI.LBL_SELECT_MAX, num.ToString());
		SetLabelText(UI.LBL_SELECT_NOW, (selectPageIndex + 1).ToString());
		if (num == 0)
		{
			SetActive(UI.OBJ_SELECT, is_visible: false);
		}
		else if (num <= 1)
		{
			SetActive(UI.BTN_AIM_R, is_visible: false);
			SetActive(UI.BTN_AIM_L, is_visible: false);
		}
		else
		{
			SetActive(UI.BTN_AIM_R_INACTIVE, is_visible: false);
			SetActive(UI.BTN_AIM_L_INACTIVE, is_visible: false);
		}
		UILabel component = GetComponent<UILabel>(UI.LBL_EXCEED_0);
		if (component != null)
		{
			paramColor = component.color;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		SetActive(UI.SPR_COUNT_0_ON, exceedCount > 0);
		SetActive(UI.SPR_COUNT_1_ON, exceedCount > 1);
		SetActive(UI.SPR_COUNT_2_ON, exceedCount > 2);
		SetActive(UI.SPR_COUNT_3_ON, exceedCount > 3);
		SetLabelText(UI.LBL_SELECT_NOW, (selectPageIndex + 1).ToString());
		UpdateBonusDetail();
		bool is_only_lapis = true;
		int item_num = exceedData.exceed.Length;
		SetGrid(UI.GRD_LAPIS, "SmithExceedItem", item_num, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			if (exceedCount >= 4)
			{
				SetActive(t, is_visible: false);
			}
			else if (i < selectPageIndex * 3 || i >= (selectPageIndex + 1) * 3)
			{
				SetActive(t, is_visible: false);
			}
			else
			{
				EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem exceedNeedItem = exceedData.exceed[i];
				if (exceedNeedItem == null || exceedNeedItem.itemId == 0 || exceedNeedItem.num[exceedCount] == 0)
				{
					SetActive(t, is_visible: false);
				}
				else
				{
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(exceedNeedItem.itemId);
					if (itemData == null)
					{
						SetActive(t, is_visible: false);
					}
					else
					{
						if (itemData.type != ITEM_TYPE.LAPIS)
						{
							is_only_lapis = false;
						}
						SetActive(t, is_visible: true);
						int haveingItemNum = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(exceedNeedItem.itemId);
						int num = (int)exceedNeedItem.num[exceedCount];
						ItemIcon.GetIconShowData(REWARD_TYPE.ITEM, exceedNeedItem.itemId, out int _, out ITEM_ICON_TYPE icon_type, out RARITY_TYPE? _, out ELEMENT_TYPE _, out ELEMENT_TYPE _, out EQUIPMENT_TYPE? _, out int _, out int _, out GET_TYPE _);
						Transform transform = FindCtrl(t, UI.OBJ_MATERIAL_ICON_ROOT);
						bool flag2 = haveingItemNum >= num;
						ItemIcon itemIcon = ItemIconMaterial.CreateMaterialIcon(icon_type, itemData, transform, haveingItemNum, num, flag2 ? "NEXT" : "NEED", i);
						SetMaterialInfo(itemIcon._transform, REWARD_TYPE.ITEM, exceedNeedItem.itemId, GetCtrl(UI.SCR_LAPIS_ROOT));
						ItemIconMaterial itemIconMaterial = itemIcon as ItemIconMaterial;
						if (itemIconMaterial != null)
						{
							itemIconMaterial.SetVisibleBG(is_visible: false);
						}
						FindCtrl(transform, UI.SPR_EXCEED_BTN_BG).parent = itemIcon._transform;
						SetActive(t, UI.SPR_EXCEED_GRAYOUT, !flag2);
						if (itemData.endDate != default(DateTime))
						{
							string format = StringTable.Get(STRING_CATEGORY.SHOP, 15u);
							SetLabelText(t, UI.LBL_LIMITED, string.Format(format, itemData.endDate.ToString("yyyy/MM/dd HH:mm")));
						}
						else
						{
							SetActive(t, UI.OBJ_EXCEED_LIMITED, is_visible: false);
						}
						if (!IsValidExceedSection())
						{
							if (itemIcon.GetComponent<UINoAuto>() == null)
							{
								itemIcon.gameObject.AddComponent<UINoAuto>();
							}
							if (itemIcon.GetComponent<UIButtonScale>() == null)
							{
								UIButtonScale uIButtonScale = itemIcon.gameObject.AddComponent<UIButtonScale>();
								uIButtonScale.hover = Vector3.one;
								uIButtonScale.pressed = UIButtonEffect.buttonScale_pressed;
								uIButtonScale.duration = UIButtonEffect.buttonScale_duration;
							}
						}
					}
				}
			}
		});
		bool flag = exceedCount < 4;
		SetActive(UI.OBJ_VALID_EXCEED_ROOT, flag);
		SetActive(UI.OBJ_INVALID_EXCEED_ROOT, !flag);
		int id = is_only_lapis ? 5 : 6;
		string text = StringTable.Get(STRING_CATEGORY.ITEM_DETAIL, (uint)id);
		SetLabelText(UI.LBL_USE_MATERIAL_NAME, text);
	}

	protected void UpdateBonusDetail(bool changeNextColor = true)
	{
		SetActive(UI.SPR_EXCEED_0_ON, exceedCount > 0);
		SetActive(UI.SPR_EXCEED_1_ON, exceedCount > 1);
		SetActive(UI.SPR_EXCEED_2_ON, exceedCount > 2);
		SetActive(UI.SPR_EXCEED_3_ON, exceedCount > 3);
		EquipItemTable.EquipItemData equipItemData = itemTable;
		Color color = (changeNextColor && exceedCount == 0) ? Color.yellow : paramColor;
		SetLabelText(UI.LBL_EXCEED_0, equipItemData.GetExceedParamName(1));
		SetColor(UI.LBL_EXCEED_0, color);
		color = ((changeNextColor && exceedCount == 1) ? Color.yellow : paramColor);
		SetLabelText(UI.LBL_EXCEED_1, equipItemData.GetExceedParamName(2));
		SetColor(UI.LBL_EXCEED_1, color);
		color = ((changeNextColor && exceedCount == 2) ? Color.yellow : paramColor);
		SetLabelText(UI.LBL_EXCEED_2, equipItemData.GetExceedParamName(3));
		SetColor(UI.LBL_EXCEED_2, color);
		color = ((changeNextColor && exceedCount == 3) ? Color.yellow : paramColor);
		SetLabelText(UI.LBL_EXCEED_3, equipItemData.GetExceedParamName(4));
		SetColor(UI.LBL_EXCEED_3, color);
	}

	private void OnQuery_NEXT()
	{
		if (smithData == null)
		{
			GameSection.StopEvent();
			return;
		}
		selectIndex = (int)GameSection.GetEventData();
		int exceed = smithData.selectEquipData.exceed;
		int needNum = (int)exceedData.exceed[selectIndex].getNeedNum(exceed + 1);
		ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(exceedData.exceed[selectIndex].itemId);
		GameSection.SetEventData(new object[3]
		{
			itemData.name,
			needNum,
			smithData.selectEquipData.tableData.name
		});
	}

	private void OnQuery_NEED()
	{
		if (smithData == null)
		{
			GameSection.StopEvent();
			return;
		}
		need_select_index = (int)GameSection.GetEventData();
		int num = (int)GameSection.GetEventData();
		int exceed = smithData.selectEquipData.exceed;
		int needNum = (int)exceedData.exceed[num].getNeedNum(exceed + 1);
		ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(exceedData.exceed[num].itemId);
		uint id = 7u;
		if (itemData.type == ITEM_TYPE.LAPIS)
		{
			id = (Singleton<EquipItemExceedTable>.I.IsFreeLapis(itemData.rarity, itemData.id, itemData.eventId) ? 3u : 4u);
			if (Singleton<LimitedEquipItemExceedTable>.I.IsLimitedLapis(itemData.id))
			{
				id = 8u;
			}
		}
		string text = string.Format(StringTable.Get(STRING_CATEGORY.ITEM_DETAIL, id), itemData.rarity.ToString());
		GameSection.SetEventData(new object[4]
		{
			itemData.name,
			needNum,
			smithData.selectEquipData.tableData.name,
			text
		});
	}

	private void OnQuery_SmithExceedNeedMessage_YES()
	{
		MonoBehaviourSingleton<TradingPostManager>.I.SetTradingPostFindData((int)exceedData.exceed[need_select_index].itemId);
	}

	private void OnQuery_SmithExceedConfirm_YES()
	{
		if (smithData == null)
		{
			GameSection.StopEvent();
			return;
		}
		EquipItemInfo selectEquipData = smithData.selectEquipData;
		if (selectEquipData == null)
		{
			GameSection.StopEvent();
			return;
		}
		SmithManager.ResultData result_data = new SmithManager.ResultData();
		result_data.beforeRarity = (int)selectEquipData.tableData.rarity;
		result_data.beforeLevel = selectEquipData.level;
		result_data.beforeMaxLevel = selectEquipData.tableData.maxLv;
		result_data.beforeExceedCnt = selectEquipData.exceed;
		result_data.beforeAtk = selectEquipData.atk;
		result_data.beforeDef = selectEquipData.def;
		result_data.beforeHp = selectEquipData.hp;
		result_data.beforeElemAtk = selectEquipData.elemAtk;
		result_data.beforeElemDef = selectEquipData.elemDef;
		result_data.isExceed = true;
		EquipItemExceedTable.EquipItemExceedData equipItemExceedDataIncludeLimited = Singleton<EquipItemExceedTable>.I.GetEquipItemExceedDataIncludeLimited(selectEquipData.tableData);
		if (equipItemExceedDataIncludeLimited == null || equipItemExceedDataIncludeLimited.exceed.Length - 1 < selectIndex)
		{
			GameSection.StopEvent();
			return;
		}
		uint itemId = equipItemExceedDataIncludeLimited.exceed[selectIndex].itemId;
		GameSection.StayEvent();
		MonoBehaviourSingleton<SmithManager>.I.SendExceedEquipItem(selectEquipData.uniqueID, itemId, delegate(Error err, EquipItemInfo exceed_equip_item)
		{
			bool num = err == Error.None;
			GameSection.ResumeEvent(num);
			if (num)
			{
				result_data.itemData = exceed_equip_item;
				GameSection.SetEventData(result_data);
			}
		});
	}

	private void OnQuery_SmithExceedConfirm_NO()
	{
	}

	private void OnQuery_AIM_R()
	{
		if (selectPageIndex >= maxPageIndex)
		{
			selectPageIndex = 0;
		}
		else
		{
			selectPageIndex++;
		}
		RefreshUI();
	}

	private void OnQuery_AIM_L()
	{
		if (selectPageIndex <= 0)
		{
			selectPageIndex = maxPageIndex;
		}
		else
		{
			selectPageIndex--;
		}
		RefreshUI();
	}
}
