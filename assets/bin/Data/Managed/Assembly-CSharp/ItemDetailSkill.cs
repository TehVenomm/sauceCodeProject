using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailSkill : SkillInfoBase
{
	protected enum UI
	{
		OBJ_DETAIL_ROOT,
		TEX_MODEL,
		TEX_INNER_MODEL,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		OBJ_LV_EX,
		LBL_LV_EX,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_SELL,
		LBL_DESCRIPTION,
		OBJ_FAVORITE_ROOT,
		TWN_FAVORITE,
		TWN_UNFAVORITE,
		OBJ_SUB_STATUS,
		SPR_SKILL_TYPE_ICON,
		SPR_SKILL_TYPE_ICON_BG,
		SPR_SKILL_TYPE_ICON_RARITY,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_DESCRIPTION,
		STR_TITLE_STATUS,
		STR_TITLE_SELL,
		PRG_EXP_BAR,
		OBJ_NEXT_EXP_ROOT,
		BTN_SELL,
		BTN_CHANGE,
		BTN_GROW
	}

	protected ItemDetailEquip.CURRENT_SECTION callSection;

	protected object itemData;

	protected Transform detailBase;

	private EquipItemInfo equipInfo;

	private int slotIndex;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		callSection = (ItemDetailEquip.CURRENT_SECTION)array[0];
		itemData = array[1];
		if (array.Length > 2)
		{
			equipInfo = (array[2] as EquipItemInfo);
			slotIndex = (int)array[3];
		}
		SortCompareData sortCompareData = itemData as SortCompareData;
		if (sortCompareData != null)
		{
			itemData = (sortCompareData.GetItemData() as SkillItemInfo);
		}
		SkillItemInfo skillItemInfo = itemData as SkillItemInfo;
		if (skillItemInfo != null)
		{
			GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.SKILL_ATTACK, skillItemInfo.uniqueID);
		}
		bool flag = equipInfo != null;
		SetActive((Enum)UI.BTN_CHANGE, flag);
		SetActive((Enum)UI.BTN_GROW, ItemDetailEquip.CanSmithSection(callSection) && skillItemInfo != null && !skillItemInfo.IsLevelMax());
		SetActive((Enum)UI.BTN_SELL, MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene() && !flag);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetupDetailBase();
		SkillItemInfo skillItemInfo = itemData as SkillItemInfo;
		SkillItemTable.SkillItemData skillItemData = itemData as SkillItemTable.SkillItemData;
		if (skillItemInfo != null)
		{
			SkillParam(skillItemInfo);
		}
		else if (skillItemData != null)
		{
			SkillTableParam(skillItemData);
		}
		else
		{
			NotDataEquipParam();
		}
	}

	protected virtual void SetupDetailBase()
	{
		detailBase = SetPrefab(GetCtrl(UI.OBJ_DETAIL_ROOT), "ItemDetailSkillBase");
		SetFontStyle(detailBase, UI.STR_TITLE_ITEM_INFO, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_DESCRIPTION, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_STATUS, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_SELL, 2);
	}

	private void SkillParam(SkillItemInfo item)
	{
		SetActive(detailBase, UI.OBJ_SUB_STATUS, is_visible: true);
		SkillItemTable.SkillItemData tableData = item.tableData;
		SetLabelText(detailBase, UI.LBL_NAME, tableData.name);
		SkillCompareParam(item, GetCompareItem());
		SetLabelText(detailBase, UI.LBL_SELL, item.sellPrice.ToString());
		SetSupportEncoding(UI.LBL_DESCRIPTION, isEnable: true);
		SetLabelText(detailBase, UI.LBL_DESCRIPTION, item.GetExplanationText(isShowExceed: true));
		bool is_visible = (callSection & (ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE | ItemDetailEquip.CURRENT_SECTION.SMITH_SKILL_MATERIAL | ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT | ItemDetailEquip.CURRENT_SECTION.UI_PARTS | ItemDetailEquip.CURRENT_SECTION.EQUIP_LIST)) == ItemDetailEquip.CURRENT_SECTION.NONE;
		SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, is_visible);
		ResetTween(detailBase, UI.TWN_FAVORITE);
		ResetTween(detailBase, UI.TWN_UNFAVORITE);
		SetActive(detailBase, UI.TWN_UNFAVORITE, !item.isFavorite);
		SetActive(detailBase, UI.TWN_FAVORITE, item.isFavorite);
		if (item.IsLevelMax())
		{
			SetProgressInt(detailBase, UI.PRG_EXP_BAR, item.exceedExp, item.exceedExpPrev, item.exceedExpNext);
		}
		else
		{
			SetProgressInt(detailBase, UI.PRG_EXP_BAR, item.exp, item.expPrev, item.expNext);
		}
		SetSkillSlotTypeIcon(detailBase, UI.SPR_SKILL_TYPE_ICON, UI.SPR_SKILL_TYPE_ICON_BG, UI.SPR_SKILL_TYPE_ICON_RARITY, tableData);
		SetRenderSkillItemModel((Enum)UI.TEX_MODEL, tableData.id, rotation: true, light_rotation: false);
		SetRenderSkillItemSymbolModel((Enum)UI.TEX_INNER_MODEL, tableData.id, rotation: true);
	}

	private void SkillTableParam(SkillItemTable.SkillItemData table_data)
	{
		SetActive(detailBase, UI.OBJ_SUB_STATUS, is_visible: true);
		SetLabelText(detailBase, UI.LBL_NAME, table_data.name);
		int level = 1;
		if (callSection == ItemDetailEquip.CURRENT_SECTION.SHOP_TOP)
		{
			level = table_data.GetMaxLv(0);
		}
		SetLabelText(detailBase, UI.LBL_LV_NOW, level.ToString());
		SetLabelText(detailBase, UI.LBL_LV_MAX, table_data.GetMaxLv(0).ToString());
		SetLabelText(detailBase, UI.LBL_ATK, table_data.baseAtk.ToString());
		SetLabelText(detailBase, UI.LBL_DEF, table_data.baseDef.ToString());
		SetLabelText(detailBase, UI.LBL_HP, table_data.baseHp.ToString());
		SetLabelText(detailBase, UI.LBL_SELL, table_data.baseSell.ToString());
		SetLabelText(detailBase, UI.LBL_DESCRIPTION, table_data.GetExplanationText(level));
		SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, is_visible: false);
		SetRenderSkillItemModel((Enum)UI.TEX_MODEL, table_data.id, rotation: true, light_rotation: false);
		SetRenderSkillItemSymbolModel((Enum)UI.TEX_INNER_MODEL, table_data.id, rotation: true);
		SetProgressInt(detailBase, UI.PRG_EXP_BAR, 0);
		SetSkillSlotTypeIcon(detailBase, UI.SPR_SKILL_TYPE_ICON, UI.SPR_SKILL_TYPE_ICON_BG, UI.SPR_SKILL_TYPE_ICON_RARITY, table_data);
	}

	private void NotDataEquipParam()
	{
		SetActive(detailBase, UI.OBJ_SUB_STATUS, is_visible: false);
		SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, is_visible: false);
		SetLabelText(detailBase, UI.LBL_NAME, base.sectionData.GetText("EMPTY"));
		SetProgressInt(detailBase, UI.PRG_EXP_BAR, 0);
		SetSkillSlotTypeIcon(detailBase, UI.SPR_SKILL_TYPE_ICON, UI.SPR_SKILL_TYPE_ICON_BG, UI.SPR_SKILL_TYPE_ICON_RARITY, null);
		SetLabelText(detailBase, UI.LBL_DESCRIPTION, string.Empty);
		ClearRenderModel((Enum)UI.TEX_MODEL);
		ClearRenderModel((Enum)UI.TEX_INNER_MODEL);
		SkillCompareParam(null, GetCompareItem());
	}

	private void SkillCompareParam(SkillItemInfo item, SkillItemInfo compare_item)
	{
		if (item != null)
		{
			SetLabelText(detailBase, UI.LBL_LV_NOW, item.level.ToString());
			SetLabelText(detailBase, UI.LBL_LV_MAX, item.GetMaxLevel().ToString());
			SetLabelText(detailBase, UI.LBL_LV_EX, item.exceedCnt.ToString());
			SetActive(detailBase, UI.OBJ_LV_EX, item.IsExceeded());
			if (compare_item != null)
			{
				SetLabelCompareParam(detailBase, UI.LBL_ATK, item.atk, compare_item.atk);
				SetLabelCompareParam(detailBase, UI.LBL_DEF, item.def, compare_item.def);
				SetLabelCompareParam(detailBase, UI.LBL_HP, item.hp, compare_item.hp);
			}
			else
			{
				SetLabelText(detailBase, UI.LBL_ATK, item.atk.ToString());
				SetLabelText(detailBase, UI.LBL_DEF, item.def.ToString());
				SetLabelText(detailBase, UI.LBL_HP, item.hp.ToString());
			}
		}
		else
		{
			string text = base.sectionData.GetText("NON_DATA");
			SetLabelText(detailBase, UI.LBL_LV_NOW, text);
			SetLabelText(detailBase, UI.LBL_LV_MAX, text);
			if (compare_item != null)
			{
				SetLabelCompareParam(detailBase, UI.LBL_ATK, -compare_item.atk, 0, 0);
				SetLabelCompareParam(detailBase, UI.LBL_DEF, -compare_item.def, 0, 0);
				SetLabelCompareParam(detailBase, UI.LBL_HP, -compare_item.hp, 0, 0);
			}
			else
			{
				SetLabelText(detailBase, UI.LBL_ATK, 0.ToString());
				SetLabelText(detailBase, UI.LBL_DEF, 0.ToString());
				SetLabelText(detailBase, UI.LBL_HP, 0.ToString());
			}
		}
	}

	protected virtual SkillItemInfo GetCompareItem()
	{
		return null;
	}

	protected void OnQuery_SWITCH_FAVORITE()
	{
		SkillItemInfo skillItemInfo = itemData as SkillItemInfo;
		if (skillItemInfo != null)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<StatusManager>.I.SendInventorySkillLock(skillItemInfo.uniqueID, delegate(bool is_success, SkillItemInfo recv_item)
			{
				ItemDetailSkill itemDetailSkill = this;
				if (is_success)
				{
					if (recv_item.isFavorite)
					{
						SetActive(detailBase, UI.TWN_UNFAVORITE, is_visible: false);
						SetActive(detailBase, UI.TWN_FAVORITE, is_visible: true);
						ResetTween(detailBase, UI.TWN_FAVORITE);
						PlayTween(detailBase, UI.TWN_FAVORITE, forward: true, delegate
						{
							GameSection.ChangeStayEvent("FAVORITE");
							GameSection.ResumeEvent(is_success);
						});
					}
					else
					{
						SetActive(detailBase, UI.TWN_FAVORITE, is_visible: false);
						SetActive(detailBase, UI.TWN_UNFAVORITE, is_visible: true);
						ResetTween(detailBase, UI.TWN_UNFAVORITE);
						PlayTween(detailBase, UI.TWN_UNFAVORITE, forward: true, delegate
						{
							GameSection.ChangeStayEvent("RELEASE_FAVORITE");
							GameSection.ResumeEvent(is_success);
						});
					}
					itemData = recv_item;
				}
				else
				{
					GameSection.ResumeEvent(is_success);
				}
			});
		}
	}

	protected void OnQuery_SELL()
	{
		SkillItemInfo skillItemInfo = itemData as SkillItemInfo;
		if (skillItemInfo == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			SellEvent(skillItemInfo);
		}
	}

	private void OnQuery_CHANGE()
	{
		SkillItemInfo skillItemInfo = itemData as SkillItemInfo;
		if (skillItemInfo == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[4]
			{
				callSection,
				skillItemInfo,
				equipInfo,
				slotIndex
			});
		}
	}

	private void OnQuery_GROW()
	{
		SkillItemInfo skillItemInfo = itemData as SkillItemInfo;
		if (skillItemInfo == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				skillItemInfo,
				null
			});
		}
	}

	protected void SellEvent(SkillItemInfo skill)
	{
		SkillItemSortData skillItemSortData = new SkillItemSortData();
		skillItemSortData.SetItem(skill);
		if (!skillItemSortData.CanSale())
		{
			GameSection.ChangeEvent("NOT_SALE_FAVORITE");
			return;
		}
		List<SortCompareData> list = new List<SortCompareData>();
		list.Add(skillItemSortData);
		GameSection.ChangeEvent("SELL", new object[2]
		{
			ItemStorageTop.TAB_MODE.SKILL,
			list
		});
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_SKILL_FAVORITE;
	}
}
