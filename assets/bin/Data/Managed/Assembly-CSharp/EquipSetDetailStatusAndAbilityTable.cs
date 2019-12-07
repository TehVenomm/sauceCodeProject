using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipSetDetailStatusAndAbilityTable : GameSection
{
	protected enum UI
	{
		GRD_ABILITY,
		SCR_ABILITY,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_ATK_ELEM_FIRE,
		LBL_ATK_ELEM_WATER,
		LBL_ATK_ELEM_THUNDER,
		LBL_ATK_ELEM_EARTH,
		LBL_ATK_ELEM_LIGHT,
		LBL_ATK_ELEM_DARK,
		LBL_ATK_ELEM_NONE,
		LBL_DEF_ELEM_FIRE,
		LBL_DEF_ELEM_WATER,
		LBL_DEF_ELEM_THUNDER,
		LBL_DEF_ELEM_EARTH,
		LBL_DEF_ELEM_LIGHT,
		LBL_DEF_ELEM_DARK,
		LBL_DEF_ELEM_NONE,
		TGL_STATUS_WINDOW_INDEX0,
		TGL_STATUS_WINDOW_INDEX1,
		TGL_STATUS_WINDOW_INDEX2,
		TGL_WINDOW_ICON_INDEX0,
		TGL_WINDOW_ICON_INDEX1,
		TGL_WINDOW_ICON_INDEX2,
		TGL_BUTTON_INDEX0,
		TGL_BUTTON_INDEX1,
		TGL_BUTTON_INDEX2,
		OBJ_EQUIP_BTN_ROOT_ACTIVE,
		OBJ_EQUIP_BTN_ROOT_INACTIVE,
		SPR_BG0,
		SPR_BG1,
		SPR_BG2,
		SPR_BG3,
		SPR_BG4,
		SPR_BG5,
		SPR_BG6,
		SPR_BG7,
		SPR_BG8,
		SPR_BG9,
		OBJ_DETAIL_ROOT,
		OBJ_ABILITY_ITEM_ROOT,
		OBJ_ABILITY_ITEM_ITEM_ROOT,
		BTN_ABILITY,
		LBL_ABILITY_NAME,
		LBL_AP_0,
		LBL_AP_1,
		LBL_AP_2,
		LBL_AP_3,
		LBL_AP_4,
		LBL_AP_5,
		LBL_AP_6,
		LBL_AP_TOTAL,
		SPR_NAME_TAG,
		SPR_NAME_TAG_OFF,
		TGL_BG,
		TGL_NAME_TAG,
		ICON_WEAPON,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		OBJ_CAPTION_3,
		LBL_CAPTION,
		OBJ_EMPTY,
		LBL_NO_ITEM
	}

	public class BaseStatus
	{
		public int atk;

		public int def;

		public int hp;

		public List<CharaInfo.EquipItem> charaListEquip;

		public BaseStatus(int _atk, int _def, int _hp, List<CharaInfo.EquipItem> chara_list_equip_data)
		{
			atk = _atk;
			def = _def;
			hp = _hp;
			charaListEquip = chara_list_equip_data;
		}
	}

	private UI[] uiAbility = new UI[7]
	{
		UI.LBL_AP_0,
		UI.LBL_AP_1,
		UI.LBL_AP_2,
		UI.LBL_AP_3,
		UI.LBL_AP_4,
		UI.LBL_AP_5,
		UI.LBL_AP_6
	};

	private UI[] uiAtkElem = new UI[7]
	{
		UI.LBL_ATK_ELEM_NONE,
		UI.LBL_ATK_ELEM_FIRE,
		UI.LBL_ATK_ELEM_WATER,
		UI.LBL_ATK_ELEM_THUNDER,
		UI.LBL_ATK_ELEM_EARTH,
		UI.LBL_ATK_ELEM_LIGHT,
		UI.LBL_ATK_ELEM_DARK
	};

	private UI[] uiDefElem = new UI[7]
	{
		UI.LBL_DEF_ELEM_NONE,
		UI.LBL_DEF_ELEM_FIRE,
		UI.LBL_DEF_ELEM_WATER,
		UI.LBL_DEF_ELEM_THUNDER,
		UI.LBL_DEF_ELEM_EARTH,
		UI.LBL_DEF_ELEM_LIGHT,
		UI.LBL_DEF_ELEM_DARK
	};

	private UI[] uiToggleStatusIndex = new UI[3]
	{
		UI.TGL_STATUS_WINDOW_INDEX0,
		UI.TGL_STATUS_WINDOW_INDEX1,
		UI.TGL_STATUS_WINDOW_INDEX2
	};

	private UI[] uiToggleWindowIconIndex = new UI[3]
	{
		UI.TGL_WINDOW_ICON_INDEX0,
		UI.TGL_WINDOW_ICON_INDEX1,
		UI.TGL_WINDOW_ICON_INDEX2
	};

	private UI[] uiToggleButtonIndex = new UI[3]
	{
		UI.TGL_BUTTON_INDEX0,
		UI.TGL_BUTTON_INDEX1,
		UI.TGL_BUTTON_INDEX2
	};

	private BaseStatus baseStatus;

	protected EquipItemAbilityCollection[] abilityCollection;

	protected EquipSetInfo equipSet;

	protected int selectEquipIndex;

	private bool isEquipSubWeapon;

	protected List<AbilityItemInfo> abilityItems = new List<AbilityItemInfo>();

	protected object[] currentEventData;

	private UI[] spr = new UI[10]
	{
		UI.SPR_BG0,
		UI.SPR_BG1,
		UI.SPR_BG2,
		UI.SPR_BG3,
		UI.SPR_BG4,
		UI.SPR_BG5,
		UI.SPR_BG6,
		UI.SPR_BG7,
		UI.SPR_BG8,
		UI.SPR_BG9
	};

	private SpringPanel spring;

	public override void Initialize()
	{
		currentEventData = (GameSection.GetEventData() as object[]);
		equipSet = (currentEventData[0] as EquipSetInfo);
		abilityCollection = (currentEventData[1] as EquipItemAbilityCollection[]);
		baseStatus = (currentEventData[2] as BaseStatus);
		EquipItemInfo[] item = equipSet.item;
		foreach (EquipItemInfo equipItemInfo in item)
		{
			if (equipItemInfo != null)
			{
				AbilityItemInfo abilityItem = equipItemInfo.GetAbilityItem();
				if (abilityItem != null)
				{
					abilityItems.Add(abilityItem);
				}
			}
		}
		Array.Sort(abilityCollection, (EquipItemAbilityCollection l, EquipItemAbilityCollection r) => (r.ability.ap != l.ability.ap) ? (r.ability.ap - l.ability.ap) : ((int)(l.ability.id - r.ability.id)));
		isEquipSubWeapon = (equipSet.item[1] != null || equipSet.item[2] != null);
		selectEquipIndex = 0;
		SetSupportEncoding(UI.LBL_HP, isEnable: true);
		SetSupportEncoding(UI.LBL_ATK, isEnable: true);
		SetSupportEncoding(UI.LBL_DEF, isEnable: true);
		int j = 0;
		for (int num = uiAtkElem.Length; j < num; j++)
		{
			SetSupportEncoding(uiAtkElem[j], isEnable: true);
			SetSupportEncoding(uiDefElem[j], isEnable: true);
		}
		InitializeCaption();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive(UI.OBJ_EQUIP_BTN_ROOT_ACTIVE, isEquipSubWeapon);
		SetActive(UI.OBJ_EQUIP_BTN_ROOT_INACTIVE, !isEquipSubWeapon);
	}

	protected void UpdateAbilityTable()
	{
		int item_num = Mathf.Max(abilityCollection.Length + abilityItems.Count, 5);
		bool is_scroll = true;
		string allAbilityName = "";
		string allAp = "";
		string allAbilityDesc = "";
		bool isEmpty = true;
		SetGrid(UI.GRD_ABILITY, "EquipSetDetailAbilityTableItem", item_num, reset: true, delegate(int i, Transform t, bool is_recycle)
		{
			if (i >= abilityCollection.Length + abilityItems.Count)
			{
				is_scroll = false;
				SetActive(t, UI.OBJ_ABILITY_ITEM_ROOT, is_visible: false);
			}
			else
			{
				isEmpty = false;
				SetActive(base._transform, UI.OBJ_EMPTY, is_visible: false);
				SetActive(t, UI.OBJ_ABILITY_ITEM_ROOT, is_visible: true);
				int num = i;
				if (i < abilityCollection.Length)
				{
					SetActive(t, UI.OBJ_ABILITY_ITEM_ITEM_ROOT, is_visible: false);
					EquipItemAbilityCollection equipItemAbilityCollection = abilityCollection[num];
					if (equipItemAbilityCollection.ability.id == 0 || equipItemAbilityCollection.ability.IsNeedUpdate() || !equipItemAbilityCollection.ability.IsActiveAbility())
					{
						SetActive(t, is_visible: false);
					}
					else
					{
						SetActive(t, is_visible: true);
						int j = 0;
						for (int num2 = equipItemAbilityCollection.equip.Length; j < num2; j++)
						{
							SetAPLabel(t, uiAbility[j], equipItemAbilityCollection.GetAP(j), equipItemAbilityCollection.swapValue[j]);
						}
						SetLabelText(t, UI.LBL_AP_TOTAL, equipItemAbilityCollection.ability.GetAP());
						Color color = Color.white;
						if (equipItemAbilityCollection.GetSwapBalance() < 0)
						{
							color = Color.red;
						}
						else if (equipItemAbilityCollection.GetSwapBalance() > 0)
						{
							color = Color.green;
						}
						GetComponent<UILabel>(t, UI.LBL_AP_TOTAL).color = color;
						SetLabelText(t, UI.LBL_ABILITY_NAME, equipItemAbilityCollection.ability.GetName());
						SetAbilityItemEvent(t, num);
						allAbilityName += equipItemAbilityCollection.ability.GetName();
						allAp += equipItemAbilityCollection.ability.GetAP();
						allAbilityDesc += equipItemAbilityCollection.ability.GetDescription();
						SetToggle(t, UI.TGL_NAME_TAG, equipItemAbilityCollection.IsAbilityOn());
					}
				}
				else
				{
					int k = 0;
					for (int num3 = uiAbility.Length; k < num3; k++)
					{
						SetAPLabel(t, uiAbility[k], "", 0);
					}
					SetLabelText(t, UI.LBL_AP_TOTAL, "");
					num = i - abilityCollection.Length;
					AbilityItemInfo abilityItemInfo = abilityItems[num];
					SetActive(t, UI.OBJ_ABILITY_ITEM_ITEM_ROOT, is_visible: true);
					SetLabelText(t, UI.LBL_ABILITY_NAME, abilityItemInfo.GetName());
					SetAbilityItemItemEvent(t, i);
					allAbilityName += abilityItemInfo.GetName();
					allAbilityDesc += abilityItemInfo.GetDescription();
				}
			}
		});
		SetActive(base._transform, UI.OBJ_EMPTY, isEmpty);
		SetLabelText(GetCtrl(UI.OBJ_EMPTY), UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19800u));
		PreCacheAbilityDetail(allAbilityName, allAp, allAbilityDesc);
		GetComponent<UIScrollView>(UI.SCR_ABILITY).enabled = is_scroll;
	}

	protected virtual void SetAbilityItemEvent(Transform t, int index)
	{
		SetEvent(t, UI.BTN_ABILITY, "ABILITY_DATA", index);
	}

	protected virtual void SetAbilityItemItemEvent(Transform t, int index)
	{
		SetEvent(t, UI.BTN_ABILITY, "ABILITY_ITEM_DATA", index);
	}

	protected void SetLabelSeparateText(Enum label_enum, int baseValue, int finalValue)
	{
		string text = "";
		bool flag = IsSupportEncoding(label_enum);
		int num = finalValue - baseValue;
		text = ((num > 0) ? string.Format(flag ? "{0}[35FF00]+{1}[-]" : "{0}+{1}", baseValue, num) : ((num >= 0) ? $"{baseValue}" : string.Format(flag ? "{0}[FF0000]{1}[-]" : "{0}{1}", baseValue, num)));
		SetLabelText(label_enum, text);
	}

	protected virtual void UpdateUIStatus()
	{
		int num;
		int num2;
		int num3;
		EquipSetCalculator equipSetCalculator;
		if (baseStatus.charaListEquip == null)
		{
			num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hp;
			num2 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.atk;
			num3 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.def;
			equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSetCalculator(MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo());
		}
		else
		{
			num = baseStatus.hp;
			num2 = baseStatus.atk;
			num3 = baseStatus.def;
			if (MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex == -1)
			{
				MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = 0;
				equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(0);
				equipSetCalculator.SetEquipSet(baseStatus.charaListEquip);
			}
			else
			{
				equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex);
			}
		}
		StatusFactor statusFactor = equipSetCalculator.GetStatusFactor(selectEquipIndex);
		SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(selectEquipIndex, num, num2, num3);
		SetLabelSeparateText(UI.LBL_HP, num + statusFactor.baseStatus.hp, finalStatus.hp);
		SetLabelText(UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
		SetLabelText(UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
		int i = 0;
		for (int num4 = uiAtkElem.Length; i < num4; i++)
		{
			if (i == 0)
			{
				SetLabelSeparateText(uiAtkElem[i], num2 + statusFactor.baseStatus.attacks[i], finalStatus.attacks[i]);
				SetLabelSeparateText(uiDefElem[i], num3 + statusFactor.baseStatus.defences[i], finalStatus.defences[i]);
			}
			else
			{
				SetLabelSeparateText(uiAtkElem[i], statusFactor.baseStatus.attacks[i], finalStatus.attacks[i]);
				SetLabelSeparateText(uiDefElem[i], statusFactor.baseStatus.tolerances[i - 1], finalStatus.tolerances[i - 1]);
			}
		}
		SetToggle(uiToggleStatusIndex[selectEquipIndex], value: true);
		SetToggle(uiToggleWindowIconIndex[selectEquipIndex], value: true);
		SetToggle(uiToggleButtonIndex[selectEquipIndex], value: true);
	}

	private void SetAPLabel(Transform parent, Enum _enum, string ap, int swap_value)
	{
		SetActive(parent, _enum, !string.IsNullOrEmpty(ap));
		SetLabelText(parent, _enum, ap);
		UILabel component = GetComponent<UILabel>(parent, _enum);
		if (swap_value == 0)
		{
			component.color = Color.white;
		}
		else if (swap_value < 0)
		{
			component.color = Color.red;
		}
		else
		{
			component.color = Color.green;
		}
	}

	protected virtual void OnQuery_ABILITY_DATA()
	{
		int num = (int)GameSection.GetEventData();
		GameSection.SetEventData(abilityCollection[num].ability);
	}

	protected virtual void OnQuery_ABILITY_ITEM_DATA()
	{
		int num = (int)GameSection.GetEventData();
		GameSection.SetEventData(abilityItems[abilityCollection.Length - num]);
	}

	protected virtual void OnQuery_INDEX_L()
	{
		selectEquipIndex = ((selectEquipIndex == 0) ? 2 : (selectEquipIndex - 1));
		if (equipSet.item[selectEquipIndex] == null)
		{
			selectEquipIndex = ((selectEquipIndex == 0) ? 2 : (selectEquipIndex - 1));
		}
		UpdateUIStatus();
	}

	protected virtual void OnQuery_INDEX_R()
	{
		selectEquipIndex = ((selectEquipIndex != 2) ? (selectEquipIndex + 1) : 0);
		if (equipSet.item[selectEquipIndex] == null)
		{
			selectEquipIndex = ((selectEquipIndex != 2) ? (selectEquipIndex + 1) : 0);
		}
		UpdateUIStatus();
	}

	private void LateUpdate()
	{
		if (spring == null)
		{
			spring = GetComponent<SpringPanel>(UI.SCR_ABILITY);
		}
		if (spring != null && spring.enabled)
		{
			int i = 0;
			for (int num = spr.Length; i < num; i++)
			{
				GetComponent<UISprite>(spr[i]).UpdateAnchors();
			}
		}
	}

	protected virtual void OnQuery_TO_STATUS()
	{
		GameSection.SetEventData(currentEventData);
	}

	private void InitializeCaption()
	{
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_3);
		string text = base.sectionData.GetText("CAPTION");
		SetLabelText(ctrl, UI.LBL_CAPTION, text);
		UITweenCtrl component = ctrl.gameObject.GetComponent<UITweenCtrl>();
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

	protected virtual void PreCacheAbilityDetail(string name, string ap, string desc)
	{
	}
}
