using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipMaterialBase : SmithEquipBase
{
	protected enum UI
	{
		BTN_DECISION,
		BTN_INACTIVE,
		LBL_NEXT_BTN,
		LBL_TO_SELECT,
		BTN_TO_SELECT,
		BTN_TO_SELECT_CENTER,
		OBJ_ADD_ABILITY,
		LBL_ADD_ABILITY,
		TEX_MODEL,
		TEX_DETAIL_BASE_MODEL,
		OBJ_DETAIL_ROOT,
		OBJ_DETAIL_BASE_ROOT,
		OBJ_ITEM_INFO_ROOT,
		OBJ_AIM_GROW,
		BTN_AIM_L,
		BTN_AIM_R,
		BTN_AIM_L_INACTIVE,
		BTN_AIM_R_INACTIVE,
		SPR_AIM_L,
		SPR_AIM_R,
		LBL_AIM_LV,
		OBJ_EVOLVE_ROOT,
		LBL_EVO_INDEX,
		LBL_EVO_INDEX_MAX,
		BTN_EVO_L,
		BTN_EVO_R,
		BTN_EVO_L_INACTIVE,
		BTN_EVO_R_INACTIVE,
		SPR_EVO_L,
		SPR_EVO_R,
		BTN_EVO_R2,
		BTN_EVO_L2,
		BTN_EVO_L2_INACTIVE,
		BTN_EVO_R2_INACTIVE,
		SPR_EVO_R2,
		SPR_EVO_L2,
		OBJ_ORDER_L2,
		OBJ_ORDER_R2,
		OBJ_ORDER_NORMAL_CENTER,
		OBJ_ORDER_ATTRIBUTE_CENTER,
		SPR_ORDER_ELEM_CENTER,
		OBJ_ORDER_NORMAL_R,
		OBJ_ORDER_ATTRIBUTE_R,
		SPR_ORDER_ELEM_R,
		OBJ_ORDER_NORMAL_L,
		OBJ_ORDER_ATTRIBUTE_L,
		SPR_ORDER_ELEM_L,
		OBJ_ORDER_CENTER_ANIM_ROOT,
		OBJ_ORDER_L_ANIM_ROOT,
		OBJ_ORDER_R_ANIM_ROOT,
		STR_INACTIVE,
		STR_INACTIVE_REFLECT,
		STR_DECISION,
		STR_DECISION_REFLECT,
		STR_TITLE_MATERIAL,
		STR_TITLE_MONEY,
		STR_TITLE_ATK,
		STR_TITLE_ELEM,
		STR_TITLE_DEF,
		STR_TITLE_ELEM_DEF,
		STR_TITLE_HP,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_ELEM,
		LBL_ELEM_DEF,
		SPR_ELEM,
		SPR_ELEM_DEF,
		LBL_SELL,
		OBJ_SKILL_BUTTON_ROOT,
		BTN_SELL,
		BTN_GROW,
		OBJ_FAVORITE_ROOT,
		SPR_FAVORITE,
		SPR_UNFAVORITE,
		SPR_IS_EVOLVE,
		TWN_FAVORITE,
		TWN_UNFAVORITE,
		OBJ_ATK_ROOT,
		OBJ_DEF_ROOT,
		OBJ_ELEM_ROOT,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_STATUS,
		STR_TITLE_SKILL_SLOT,
		STR_TITLE_ABILITY,
		STR_TITLE_SELL,
		STR_TITLE_ELEMENT,
		TBL_ABILITY,
		STR_NON_ABILITY,
		LBL_ABILITY,
		LBL_ABILITY_NUM,
		BTN_EXCEED,
		SPR_COUNT_0_ON,
		SPR_COUNT_1_ON,
		SPR_COUNT_2_ON,
		SPR_COUNT_3_ON,
		STR_ONLY_EXCEED,
		LBL_AFTER_ATK,
		LBL_AFTER_DEF,
		LBL_AFTER_HP,
		LBL_AFTER_ELEM,
		LBL_AFTER_ELEM_DEF,
		GRD_NEED_MATERIAL,
		LBL_GOLD,
		LBL_CAPTION,
		BTN_GRAPH,
		BTN_LIST,
		SPR_SP_ATTACK_TYPE,
		SPR_ORDER_ACTIONTYPE_CENTER,
		SPR_ORDER_ACTIONTYPE_LEFT,
		SPR_ORDER_ACTIONTYPE_RIGHT,
		BTN_SHADOW_EVOLVE,
		OBJ_ABILITY,
		OBJ_FIXEDABILITY,
		LBL_FIXEDABILITY,
		LBL_FIXEDABILITY_NUM,
		OBJ_ABILITY_ITEM,
		LBL_ABILITY_ITEM,
		OBJ_WEAPON_ROOT,
		OBJ_ARMOR_ROOT,
		LinePartsR01
	}

	private class MaterialSortData
	{
		public int isKey;

		public ItemTable.ItemData table;

		public NeedMaterial needData;

		public MaterialSortData(NeedMaterial need_data, bool is_key = false)
		{
			needData = need_data;
			table = Singleton<ItemTable>.I.GetItemData(need_data.itemID);
			isKey = (is_key ? 1 : 0);
		}
	}

	protected bool isDialogEventYES;

	protected NeedMaterial[] needMaterial;

	protected int[] haveMaterialNum;

	protected int needMoney;

	protected NeedEquip[] needEquip;

	protected int[] haveEquipNum;

	protected ulong[] selectedUniqueIdList;

	protected Transform detailBase;

	protected bool isNotifySelfUpdate;

	public NeedMaterial[] MaterialSort(NeedMaterial[] material_ary)
	{
		if (material_ary == null)
		{
			return null;
		}
		if (material_ary.Length < 1)
		{
			return material_ary;
		}
		MaterialSortData[] array = new MaterialSortData[material_ary.Length];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			array[i] = new MaterialSortData(material_ary[i], material_ary[i].isKey);
		}
		Array.Sort(array, delegate(MaterialSortData l, MaterialSortData r)
		{
			int num3 = r.isKey - l.isKey;
			if (num3 == 0)
			{
				num3 = r.table.rarity - l.table.rarity;
				if (num3 == 0)
				{
					num3 = ((l.table.id != r.table.id) ? ((l.table.id > r.table.id) ? 1 : (-1)) : 0);
				}
			}
			return num3;
		});
		NeedMaterial[] array2 = new NeedMaterial[array.Length];
		int j = 0;
		for (int num2 = array.Length; j < num2; j++)
		{
			array2[j] = array[j].needData;
		}
		return array2;
	}

	public override void Initialize()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		type = EquipDialogType.MATERIAL;
		Transform ctrl = GetCtrl(UI.BTN_GRAPH);
		if (ctrl != null)
		{
			EquipItemTable.EquipItemData equipTableData = GetEquipTableData();
			if (equipTableData != null)
			{
				bool active = equipTableData.damageDistanceId >= 0;
				ctrl.get_gameObject().SetActive(active);
			}
			else
			{
				ctrl.get_gameObject().SetActive(false);
			}
		}
		base.Initialize();
	}

	protected override void OnOpen()
	{
		isNotifySelfUpdate = false;
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		if (smithType == SmithType.GENERATE || smithType == SmithType.SKILL_GROW)
		{
			SetActive((Enum)UI.BTN_EXCEED, false);
			SetActive((Enum)UI.BTN_SHADOW_EVOLVE, false);
		}
		else
		{
			SetActive((Enum)UI.BTN_EXCEED, GetEquipData().tableData.exceedID != 0 && !GetEquipData().tableData.IsShadow());
			SetActive((Enum)UI.BTN_SHADOW_EVOLVE, GetEquipData().tableData.IsShadow());
			int exceed = GetEquipData().exceed;
			SetActive((Enum)UI.SPR_COUNT_0_ON, exceed > 0);
			SetActive((Enum)UI.SPR_COUNT_1_ON, exceed > 1);
			SetActive((Enum)UI.SPR_COUNT_2_ON, exceed > 2);
			SetActive((Enum)UI.SPR_COUNT_3_ON, exceed > 3);
		}
		SetActive((Enum)UI.BTN_LIST, smithType == SmithType.GENERATE);
		SetActive((Enum)UI.OBJ_ITEM_INFO_ROOT, smithType != SmithType.GROW);
		SetActive((Enum)UI.OBJ_AIM_GROW, smithType == SmithType.GROW);
		SetActive((Enum)UI.OBJ_EVOLVE_ROOT, smithType == SmithType.EVOLVE);
		SetLabelText((Enum)UI.STR_DECISION, base.sectionData.GetText("STR_DECISION"));
		SetLabelText((Enum)UI.STR_DECISION_REFLECT, base.sectionData.GetText("STR_DECISION"));
		SetLabelText((Enum)UI.STR_INACTIVE, base.sectionData.GetText("STR_INACTIVE"));
		SetLabelText((Enum)UI.STR_INACTIVE_REFLECT, base.sectionData.GetText("STR_INACTIVE"));
		InitNeedMaterialData();
		if (!string.IsNullOrEmpty(CreateItemDetailPrefabName()))
		{
			detailBase = SetPrefab(GetCtrl(UI.OBJ_DETAIL_ROOT), CreateItemDetailPrefabName(), true);
			if (detailBase != null)
			{
				SetFontStyle(detailBase, UI.STR_TITLE_ITEM_INFO, 2);
				SetFontStyle(detailBase, UI.STR_TITLE_SKILL_SLOT, 2);
				SetFontStyle(detailBase, UI.STR_TITLE_STATUS, 2);
				SetFontStyle(detailBase, UI.STR_TITLE_ABILITY, 2);
				SetFontStyle(detailBase, UI.STR_TITLE_SELL, 2);
				SetFontStyle(detailBase, UI.STR_TITLE_ELEMENT, 2);
				SetFontStyle(detailBase, UI.STR_TITLE_ATK, 2);
				SetFontStyle(detailBase, UI.STR_TITLE_ELEM, 2);
				SetFontStyle(detailBase, UI.STR_TITLE_DEF, 2);
				SetFontStyle(detailBase, UI.STR_TITLE_ELEM_DEF, 2);
				SetFontStyle(detailBase, UI.STR_TITLE_HP, 2);
				SetActive(detailBase, UI.BTN_SELL, false);
				SetActive(detailBase, UI.BTN_GROW, false);
				SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, false);
				SetActive((Enum)UI.OBJ_DETAIL_BASE_ROOT, false);
				SetSprite(detailBase, UI.SPR_SP_ATTACK_TYPE, (!GetEquipTableData().IsWeapon()) ? string.Empty : GetEquipTableData().spAttackType.GetSmallFrameSpriteName());
			}
		}
		else
		{
			SetFontStyle((Enum)UI.STR_TITLE_ITEM_INFO, 2);
			SetFontStyle((Enum)UI.STR_TITLE_SKILL_SLOT, 2);
			SetFontStyle((Enum)UI.STR_TITLE_STATUS, 2);
			SetFontStyle((Enum)UI.STR_TITLE_ABILITY, 2);
			SetFontStyle((Enum)UI.STR_TITLE_SELL, 2);
			SetFontStyle((Enum)UI.STR_TITLE_ELEMENT, 2);
			SetFontStyle((Enum)UI.STR_TITLE_ATK, 2);
			SetFontStyle((Enum)UI.STR_TITLE_ELEM, 2);
			SetFontStyle((Enum)UI.STR_TITLE_DEF, 2);
			SetFontStyle((Enum)UI.STR_TITLE_ELEM_DEF, 2);
			SetFontStyle((Enum)UI.STR_TITLE_HP, 2);
			SetSprite((Enum)UI.SPR_SP_ATTACK_TYPE, (!GetEquipTableData().IsWeapon()) ? string.Empty : GetEquipTableData().spAttackType.GetSmallFrameSpriteName());
		}
		SetFontStyle((Enum)UI.STR_TITLE_MATERIAL, 2);
		SetFontStyle((Enum)UI.STR_TITLE_MONEY, 2);
		bool flag = IsHavingMaterialAndMoney();
		SetActive((Enum)UI.BTN_DECISION, flag);
		SetActive((Enum)UI.BTN_INACTIVE, !flag);
		base.UpdateUI();
	}

	protected virtual string CreateItemDetailPrefabName()
	{
		return string.Empty;
	}

	protected virtual void InitNeedMaterialData()
	{
	}

	protected void CheckNeedMaterialNumFromInventory()
	{
		if (needMaterial != null)
		{
			haveMaterialNum = new int[needMaterial.Length];
			List<uint> list = new List<uint>();
			for (int i = 0; i < needMaterial.Length; i++)
			{
				list.Add(needMaterial[i].itemID);
			}
			LinkedListNode<ItemInfo> node;
			for (node = MonoBehaviourSingleton<InventoryManager>.I.itemInventory.GetFirstNode(); node != null; node = node.Next)
			{
				uint find_id = 0u;
				list.ForEach(delegate(uint id)
				{
					if (find_id == 0 && id == node.Value.tableID)
					{
						int num = 0;
						for (int l = 0; l < needMaterial.Length; l++)
						{
							if (needMaterial[l].itemID == id)
							{
								num = l;
								break;
							}
						}
						haveMaterialNum[num] = node.Value.num;
						find_id = id;
					}
				});
				if (find_id != 0)
				{
					list.Remove(find_id);
				}
			}
		}
		if (needEquip != null)
		{
			needEquip = NeedEquip.DivideNeedEquip(needEquip);
			haveEquipNum = new int[needEquip.Length];
			if (selectedUniqueIdList == null)
			{
				selectedUniqueIdList = new ulong[needEquip.Length];
			}
			List<uint> list2 = new List<uint>();
			for (int j = 0; j < needEquip.Length; j++)
			{
				list2.Add(needEquip[j].equipItemID);
			}
			for (LinkedListNode<EquipItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				for (int k = 0; k < needEquip.Length; k++)
				{
					if (linkedListNode.Value.tableID == needEquip[k].equipItemID)
					{
						haveEquipNum[k]++;
					}
				}
			}
		}
	}

	protected unsafe override void NeededMaterial()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.GRD_NEED_MATERIAL);
		while (ctrl.get_childCount() != 0)
		{
			Transform val = ctrl.GetChild(0);
			val.set_parent(null);
			val.get_gameObject().SetActive(false);
			Object.Destroy(val.get_gameObject());
		}
		int needEquipSize = 0;
		int num = 0;
		if (needEquip != null)
		{
			needEquipSize = needEquip.Length;
		}
		if (needMaterial != null)
		{
			num = needMaterial.Length;
		}
		int needItemSize = needEquipSize + num;
		_003CNeededMaterial_003Ec__AnonStorey34C _003CNeededMaterial_003Ec__AnonStorey34C;
		SetGrid(UI.GRD_NEED_MATERIAL, null, needItemSize, true, new Action<int, Transform, bool>((object)_003CNeededMaterial_003Ec__AnonStorey34C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		SetLabelText((Enum)UI.LBL_GOLD, needMoney.ToString("N0"));
		Color color = Color.get_white();
		if (needMaterial == null && needEquip == null)
		{
			color = Color.get_gray();
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < needMoney)
		{
			color = Color.get_red();
		}
		SetColor((Enum)UI.LBL_GOLD, color);
	}

	protected bool IsHavingMaterialAndMoney()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money >= needMoney && MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(needMaterial) && MonoBehaviourSingleton<InventoryManager>.I.IsHaveingEquip(needEquip))
		{
			if (needEquip == null)
			{
				return true;
			}
			if (MonoBehaviourSingleton<InventoryManager>.I.IsSetEquipMaterial(selectedUniqueIdList))
			{
				return true;
			}
		}
		return false;
	}

	protected void OnQuery_ABILITY()
	{
		int num = (int)GameSection.GetEventData();
		EquipItemAbility equipItemAbility = null;
		EquipItemInfo equipData = GetEquipData();
		if (equipData != null)
		{
			equipItemAbility = new EquipItemAbility(equipData.ability[num].id, -1);
		}
		else
		{
			EquipItemTable.EquipItemData equipTableData = GetEquipTableData();
			if (smithType == SmithType.EVOLVE)
			{
				SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
				equipItemAbility = new EquipItemAbility(smithData.selectEquipData.ability[num].id, -1);
			}
			else
			{
				equipItemAbility = new EquipItemAbility((uint)equipTableData.fixedAbility[num].id, -1);
			}
		}
		if (equipItemAbility == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(equipItemAbility);
		}
	}

	protected virtual void OnQuery_BTN_SHADOW_EVOLVE()
	{
	}

	protected override void EquipImg()
	{
		SetRenderEquipModel((Enum)UI.TEX_DETAIL_BASE_MODEL, GetEquipTableData().id, -1, -1, 1f);
	}

	protected virtual string GetEquipItemName()
	{
		return GetEquipTableData().name;
	}

	protected virtual void OnQuery_START()
	{
		SmithManager.ERR_SMITH_SEND eRR_SMITH_SEND = MonoBehaviourSingleton<SmithManager>.I.CheckGrowEquipItem(GetEquipData());
		if (eRR_SMITH_SEND != 0)
		{
			GameSection.ChangeEvent(eRR_SMITH_SEND.ToString(), null);
		}
		else
		{
			isDialogEventYES = false;
			GameSection.SetEventData(new object[1]
			{
				GetEquipItemName()
			});
		}
	}

	protected virtual void OnQuery_SKILL_ICON_BUTTON()
	{
		GameSection.SetEventData(new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.SMITH_GROW,
			GetEquipData()
		});
	}

	protected void OnQuery_MATERIAL()
	{
		int num = (int)GameSection.GetEventData();
		uint itemID = needMaterial[num].itemID;
		ItemSortData itemSortData = new ItemSortData();
		ItemInfo itemInfo = new ItemInfo();
		itemInfo.uniqueID = 0uL;
		itemInfo.tableID = itemID;
		itemInfo.tableData = Singleton<ItemTable>.I.GetItemData(itemInfo.tableID);
		itemInfo.num = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(itemID);
		itemSortData.SetItem(itemInfo);
		GameSection.SetEventData(new object[2]
		{
			itemSortData,
			needMaterial[num].num
		});
	}

	protected void OnQuery_EQUIP()
	{
		int num = (int)GameSection.GetEventData();
		uint equipItemID = needEquip[num].equipItemID;
		int needLv = needEquip[num].needLv;
		GameSection.SetEventData(new object[4]
		{
			equipItemID,
			needLv,
			selectedUniqueIdList,
			num
		});
	}

	protected void OnQueryConfirmYES()
	{
		Send();
	}

	protected void OnQuery_DISTANCE_GRAPH()
	{
		int damageDistanceId = GetEquipTableData().damageDistanceId;
		GameSection.SetEventData(new object[1]
		{
			damageDistanceId
		});
	}

	protected virtual void Send()
	{
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		if (isNotifySelfUpdate)
		{
			return (NOTIFY_FLAG)0L;
		}
		return NOTIFY_FLAG.UPDATE_ITEM_INVENTORY;
	}
}
