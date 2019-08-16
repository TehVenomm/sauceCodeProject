using UnityEngine;

public class ItemIconDetailEquipSetupper : ItemIconDetailSetuperBase
{
	public UISprite spRegistedAchievement;

	public UISprite spGrowMax;

	public UISprite spIsValidEvolve;

	public UISprite spEquipIndex;

	public UISprite spEquipFavorite;

	public UISprite spGrayOut;

	public GameObject lvRoot;

	public GameObject strVisualEquip;

	public UILabel lblLv;

	public UISprite spValueType;

	public UILabel lblValue;

	public UISprite spElem;

	public UILabel lblElem;

	public UISprite spSkillBG;

	public UIGrid grdSkillRoot;

	public UILabel lblNonSkillSlot;

	public GameObject objAbilityRoot;

	public UILabel lblNonAbility;

	public UISprite spSellSelectNumber;

	protected UIGrid gridEquipMark;

	private static readonly string SPR_TYPE_ATK = "EquipStatusATK_W";

	private static readonly string SPR_TYPE_DEF = "EquipStatusDEF_W";

	public static readonly string[] SPR_EQUIP_INDEX = new string[4]
	{
		"ItemIconEquipMark01",
		"ItemIconEquipMark02",
		"ItemIconEquipMark03",
		"ItemIconEquipMark"
	};

	protected override UISprite selectSP => spSellSelectNumber;

	public override void Set(object[] data = null)
	{
		base.Set();
		if (data != null)
		{
			SkillSlotUIData[] slot_data = data[1] as SkillSlotUIData[];
			bool is_show_main_status = (bool)data[2];
			ItemIconDetail.ICON_STATUS iconStatusSprite = (ItemIconDetail.ICON_STATUS)data[3];
			int equipping_sp_index = (int)data[4] - 1;
			infoRootAry[1].SetActive(true);
			SetupSelectNumberSprite((int)data[5]);
			SetIconStatusSprite(iconStatusSprite);
			if (gridEquipMark == null)
			{
				gridEquipMark = spEquipIndex.get_gameObject().GetComponentInParent<UIGrid>();
			}
			if (data[0] is EquipItemInfo)
			{
				Set(data[0] as EquipItemInfo, slot_data, is_show_main_status, equipping_sp_index);
			}
			else
			{
				Set(data[0] as EquipItemTable.EquipItemData, slot_data, is_show_main_status);
			}
		}
	}

	protected void Set(EquipItemInfo item, SkillSlotUIData[] slot_data, bool is_show_main_status, int equipping_sp_index)
	{
		SetEquipIndexSprite(equipping_sp_index);
		SetFavorite(item.isFavorite);
		bool flag = item.tableData.IsWeapon();
		if (flag)
		{
			SetElement(item.GetTargetElement(), item.elemAtk, isWeapon: true);
		}
		else
		{
			int num = item.elemDef;
			if (item.tableData.isFormer)
			{
				num = Mathf.FloorToInt((float)num * 0.1f);
			}
			SetElement(item.GetTargetElement(), num, isWeapon: false);
		}
		if (is_show_main_status)
		{
			infoRootAry[2].SetActive(true);
			infoRootAry[3].SetActive(false);
			SetVisibleBG(is_visible: true);
			SetName(item.tableData.name);
			SetLevel(item.level, item.tableData.maxLv, item.tableData.IsVisual());
			SetEquipValue(flag, (!flag) ? item.def : item.atk);
			return;
		}
		infoRootAry[2].SetActive(false);
		infoRootAry[3].SetActive(true);
		SetVisibleBG(is_visible: false);
		SetName(string.Empty);
		int num2 = (slot_data != null && slot_data.Length > 0) ? slot_data.Length : 0;
		bool flag2 = num2 > 0;
		spSkillBG.get_gameObject().SetActive(flag2);
		grdSkillRoot.get_gameObject().SetActive(flag2);
		lblNonSkillSlot.get_gameObject().SetActive(!flag2);
		if (flag2)
		{
			int childCount = grdSkillRoot.get_transform().get_childCount();
			for (int i = 0; i < childCount; i++)
			{
				bool flag3 = false;
				UISprite component = grdSkillRoot.get_transform().GetChild(i).GetComponent<UISprite>();
				if (i < num2 && component != null && slot_data[i] != null && slot_data[i].slotData != null)
				{
					flag3 = true;
				}
				if (flag3)
				{
					bool is_attached = slot_data[i].itemData != null && (slot_data[i].itemData.isAttached || slot_data[i].itemData.isUniqueAttached) && slot_data[i].itemData.tableData.type == slot_data[i].slotData.slotType;
					component.get_gameObject().SetActive(true);
					component.spriteName = UIBehaviour.GetSkillIconSpriteName(slot_data[i].slotData.slotType, is_attached, is_button_icon: false);
				}
				else
				{
					component.get_gameObject().SetActive(false);
					component.spriteName = string.Empty;
				}
			}
		}
		grdSkillRoot.Reposition();
		bool enabled = true;
		EquipItemAbility[] ability = item.ability;
		objAbilityRoot.GetComponentsInChildren<UILabel>(Temporary.uiLabelList);
		int j = 0;
		for (int count = Temporary.uiLabelList.Count; j < count; j++)
		{
			UILabel uILabel = Temporary.uiLabelList[j];
			uILabel.set_enabled(j < ability.Length && ability[j].id != 0 && ability[j].ap > 0);
			if (uILabel.get_enabled())
			{
				uILabel.text = ability[j].GetNameAndAP();
				enabled = false;
			}
		}
		Temporary.uiLabelList.Clear();
		lblNonAbility.set_enabled(enabled);
	}

	protected void Set(EquipItemTable.EquipItemData table, SkillSlotUIData[] slot_data, bool is_show_main_status)
	{
		SetEquipIndexSprite(-1);
		SetFavorite(is_favorite: false);
		bool flag = table.IsWeapon();
		SetElement(table.GetTargetElement(0), (!flag) ? table.baseElemDef : table.baseElemAtk, flag);
		if (is_show_main_status)
		{
			infoRootAry[2].SetActive(true);
			infoRootAry[3].SetActive(false);
			SetVisibleBG(is_visible: true);
			SetName(table.name);
			SetLevel(1, table.maxLv, table.IsVisual());
			SetEquipValue(flag, (!flag) ? table.baseDef : table.baseAtk);
			return;
		}
		infoRootAry[2].SetActive(false);
		infoRootAry[3].SetActive(true);
		SetVisibleBG(is_visible: false);
		SetName(string.Empty);
		int num = (slot_data != null && slot_data.Length > 0) ? slot_data.Length : 0;
		bool flag2 = num > 0;
		spSkillBG.get_gameObject().SetActive(flag2);
		grdSkillRoot.get_gameObject().SetActive(flag2);
		lblNonSkillSlot.get_gameObject().SetActive(!flag2);
		if (flag2)
		{
			int childCount = grdSkillRoot.get_transform().get_childCount();
			for (int i = 0; i < childCount; i++)
			{
				bool flag3 = false;
				UISprite component = grdSkillRoot.get_transform().GetChild(i).GetComponent<UISprite>();
				if (i < num && component != null && slot_data[i] != null && slot_data[i].slotData != null)
				{
					flag3 = true;
				}
				if (flag3)
				{
					bool is_attached = slot_data[i].itemData != null && (slot_data[i].itemData.isAttached || slot_data[i].itemData.isUniqueAttached) && slot_data[i].itemData.tableData.type == slot_data[i].slotData.slotType;
					component.get_gameObject().SetActive(true);
					component.spriteName = UIBehaviour.GetSkillIconSpriteName(slot_data[i].slotData.slotType, is_attached, is_button_icon: false);
				}
				else
				{
					component.get_gameObject().SetActive(false);
					component.spriteName = string.Empty;
				}
			}
		}
		grdSkillRoot.Reposition();
		bool enabled = true;
		objAbilityRoot.GetComponentsInChildren<UILabel>(Temporary.uiLabelList);
		EquipItemAbility[] array = new EquipItemAbility[Temporary.uiLabelList.Count];
		int j = 0;
		for (int count = Temporary.uiLabelList.Count; j < count; j++)
		{
			UILabel uILabel = Temporary.uiLabelList[j];
			array[j] = null;
			if (j < table.fixedAbility.Length)
			{
				array[j] = new EquipItemAbility((uint)table.fixedAbility[j].id, table.fixedAbility[j].pt);
			}
			uILabel.set_enabled(array[j] != null && array[j].id != 0 && array[j].ap > 0);
			if (uILabel.get_enabled())
			{
				uILabel.text = array[j].GetNameAndAP();
				enabled = false;
			}
		}
		Temporary.uiLabelList.Clear();
		lblNonAbility.set_enabled(enabled);
	}

	protected void SetLevel(int lv, int lv_max, bool is_visual_equip = false)
	{
		lvRoot.SetActive(!is_visual_equip);
		strVisualEquip.SetActive(is_visual_equip);
		if (!is_visual_equip)
		{
			lblLv.text = $"{lv}/{lv_max}";
		}
	}

	protected void SetEquipValue(bool is_weapon, int value)
	{
		spValueType.spriteName = ((!is_weapon) ? SPR_TYPE_DEF : SPR_TYPE_ATK);
		lblValue.get_gameObject().SetActive(true);
		lblValue.text = value.ToString();
	}

	protected void SetElement(ELEMENT_TYPE elem_type, int value, bool isWeapon)
	{
		spElem.get_gameObject().SetActive(value > 0);
		if (value > 0)
		{
			spElem.spriteName = ((!isWeapon) ? UIBehaviour.GetElemDefSpriteName((int)elem_type) : UIBehaviour.GetElemSpriteName((int)elem_type));
			lblElem.text = value.ToString();
		}
	}

	protected void SetIconStatusSprite(ItemIconDetail.ICON_STATUS icon_status)
	{
		SetRegistedIcon(is_visible: false);
		switch (icon_status)
		{
		case ItemIconDetail.ICON_STATUS.VALID_EVOLVE:
			spIsValidEvolve.get_gameObject().SetActive(true);
			spGrowMax.set_enabled(false);
			spGrayOut.set_enabled(false);
			break;
		case ItemIconDetail.ICON_STATUS.NOT_ENOUGH_MATERIAL:
		case ItemIconDetail.ICON_STATUS.GRAYOUT:
			spIsValidEvolve.get_gameObject().SetActive(false);
			spGrowMax.set_enabled(false);
			spGrayOut.set_enabled(true);
			break;
		case ItemIconDetail.ICON_STATUS.GROW_MAX:
			spIsValidEvolve.get_gameObject().SetActive(false);
			spGrowMax.set_enabled(true);
			spGrayOut.set_enabled(false);
			break;
		case ItemIconDetail.ICON_STATUS.NONE:
			spIsValidEvolve.get_gameObject().SetActive(false);
			spGrowMax.set_enabled(false);
			spGrayOut.set_enabled(false);
			break;
		}
	}

	protected void SetEquipIndexSprite(int index)
	{
		if (index < 0 || SPR_EQUIP_INDEX.Length <= index)
		{
			spEquipIndex.spriteName = string.Empty;
		}
		else
		{
			spEquipIndex.spriteName = SPR_EQUIP_INDEX[index];
		}
	}

	protected void SetFavorite(bool is_favorite)
	{
		spEquipFavorite.get_gameObject().SetActive(is_favorite);
		if (gridEquipMark != null)
		{
			gridEquipMark.Reposition();
		}
	}

	public void SetRegistedIcon(bool is_visible)
	{
		spRegistedAchievement.set_enabled(is_visible);
	}
}
