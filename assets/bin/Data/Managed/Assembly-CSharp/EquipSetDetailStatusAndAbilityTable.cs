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
		LBL_CAPTION
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
		Array.Sort(abilityCollection, delegate(EquipItemAbilityCollection l, EquipItemAbilityCollection r)
		{
			if (r.ability.ap != l.ability.ap)
			{
				return r.ability.ap - l.ability.ap;
			}
			return (int)(l.ability.id - r.ability.id);
		});
		isEquipSubWeapon = (equipSet.item[1] != null || equipSet.item[2] != null);
		selectEquipIndex = 0;
		SetSupportEncoding(UI.LBL_HP, true);
		SetSupportEncoding(UI.LBL_ATK, true);
		SetSupportEncoding(UI.LBL_DEF, true);
		int j = 0;
		for (int num = uiAtkElem.Length; j < num; j++)
		{
			SetSupportEncoding(uiAtkElem[j], true);
			SetSupportEncoding(uiDefElem[j], true);
		}
		InitializeCaption();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_EQUIP_BTN_ROOT_ACTIVE, isEquipSubWeapon);
		SetActive((Enum)UI.OBJ_EQUIP_BTN_ROOT_INACTIVE, !isEquipSubWeapon);
	}

	protected unsafe void UpdateAbilityTable()
	{
		int item_num = Mathf.Max(abilityCollection.Length + abilityItems.Count, 5);
		bool is_scroll = true;
		string allAbilityName = string.Empty;
		string allAp = string.Empty;
		string allAbilityDesc = string.Empty;
		_003CUpdateAbilityTable_003Ec__AnonStorey2F5 _003CUpdateAbilityTable_003Ec__AnonStorey2F;
		SetGrid(UI.GRD_ABILITY, "EquipSetDetailAbilityTableItem", item_num, true, new Action<int, Transform, bool>((object)_003CUpdateAbilityTable_003Ec__AnonStorey2F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		PreCacheAbilityDetail(allAbilityName, allAp, allAbilityDesc);
		base.GetComponent<UIScrollView>((Enum)UI.SCR_ABILITY).set_enabled(is_scroll);
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
		string empty = string.Empty;
		bool flag = IsSupportEncoding(label_enum);
		int num = finalValue - baseValue;
		empty = ((num > 0) ? string.Format((!flag) ? "{0}+{1}" : "{0}[35FF00]+{1}[-]", baseValue, num) : ((num >= 0) ? $"{baseValue}" : string.Format((!flag) ? "{0}{1}" : "{0}[FF0000]{1}[-]", baseValue, num)));
		SetLabelText(label_enum, empty);
	}

	protected virtual void UpdateUIStatus()
	{
		int num;
		int num2;
		int num3;
		EquipSetCalculator equipSetCalculator;
		if (object.ReferenceEquals(baseStatus.charaListEquip, null))
		{
			num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hp;
			num2 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.atk;
			num3 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.def;
			equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetCalculator(MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo());
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
				equipSetCalculator.SetEquipSet(baseStatus.charaListEquip, false);
			}
			else
			{
				equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex);
			}
		}
		StatusFactor statusFactor = equipSetCalculator.GetStatusFactor(selectEquipIndex);
		SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(selectEquipIndex, num, num2, num3);
		SetLabelSeparateText(UI.LBL_HP, num + statusFactor.baseStatus.hp, finalStatus.hp);
		SetLabelText((Enum)UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
		SetLabelText((Enum)UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
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
		SetToggle((Enum)uiToggleStatusIndex[selectEquipIndex], true);
		SetToggle((Enum)uiToggleWindowIconIndex[selectEquipIndex], true);
		SetToggle((Enum)uiToggleButtonIndex[selectEquipIndex], true);
	}

	private void SetAPLabel(Transform parent, Enum _enum, string ap, int swap_value)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		SetActive(parent, _enum, !string.IsNullOrEmpty(ap));
		SetLabelText(parent, _enum, ap);
		UILabel component = base.GetComponent<UILabel>(parent, _enum);
		if (swap_value == 0)
		{
			component.color = Color.get_white();
		}
		else if (swap_value < 0)
		{
			component.color = Color.get_red();
		}
		else
		{
			component.color = Color.get_green();
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
		selectEquipIndex = ((selectEquipIndex != 0) ? (selectEquipIndex - 1) : 2);
		if (equipSet.item[selectEquipIndex] == null)
		{
			selectEquipIndex = ((selectEquipIndex != 0) ? (selectEquipIndex - 1) : 2);
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
			spring = base.GetComponent<SpringPanel>((Enum)UI.SCR_ABILITY);
		}
		if (spring != null && spring.get_enabled())
		{
			int i = 0;
			for (int num = spr.Length; i < num; i++)
			{
				UISprite component = base.GetComponent<UISprite>((Enum)spr[i]);
				component.UpdateAnchors();
			}
		}
	}

	protected virtual void OnQuery_TO_STATUS()
	{
		GameSection.SetEventData(currentEventData);
	}

	private void InitializeCaption()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
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
			component.Play(true, null);
		}
	}

	protected virtual void PreCacheAbilityDetail(string name, string ap, string desc)
	{
	}
}
