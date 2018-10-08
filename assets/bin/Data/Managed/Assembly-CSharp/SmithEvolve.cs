using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithEvolve : EquipGenerateBase
{
	protected new enum UI
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

	public class AdapterAbility
	{
		public EquipItemAbility ability;

		public bool isFix;

		public void Set(EquipItem.Ability a)
		{
			ability = new EquipItemAbility((uint)a.id, a.pt);
			isFix = true;
		}

		public void Set(EquipItemAbility a)
		{
			ability = a;
			isFix = false;
		}

		public uint GetId()
		{
			if (object.ReferenceEquals(ability, null))
			{
				return 0u;
			}
			return ability.id;
		}

		public string GetName()
		{
			if (object.ReferenceEquals(ability, null))
			{
				return string.Empty;
			}
			return ability.GetName();
		}

		public string GetAP()
		{
			if (object.ReferenceEquals(ability, null))
			{
				return "+0";
			}
			return ability.GetAP();
		}
	}

	private const float RADIUS = 3f;

	private const float SPEED = 4f;

	private const float LOCAL_ROTATE_START = 1f;

	private const float LOCAL_ROTATE_SPEED = 10f;

	private List<AdapterAbility> adapterAbilityList = new List<AdapterAbility>();

	private string modifyText;

	private Vector3 defaultCenterPos;

	private Vector3 defaultRightPos;

	private Vector3 defaultLeftPos;

	private TweenPosition centerAnim;

	private TweenPosition rightAnim;

	private TweenPosition leftAnim;

	private bool isButtonChange;

	private bool isRightChange;

	private Transform[] itemModels;

	private Transform itemModelRoot;

	private float targetAngle;

	private float nowAngle;

	private float rotateSign;

	private float localRotateWait;

	private float localRotate;

	private bool isModelScrolling;

	private List<TweenPosition> animPosList;

	private Vector3 defaultModelPos;

	public override void Initialize()
	{
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		EquipItemInfo selectEquipData = smithData.selectEquipData;
		if (selectEquipData != null)
		{
			EvolveEquipItemTable.EvolveEquipItemData[] evolveTable = selectEquipData.tableData.GetEvolveTable();
			if (evolveTable != null)
			{
				SmithManager.SmithEvolveData smithEvolveData = new SmithManager.SmithEvolveData();
				smithEvolveData.selectIndex = 0;
				smithEvolveData.evolveBeforeEquipData = selectEquipData;
				smithEvolveData.evolveTable = evolveTable;
				smithEvolveData.evolveEquipDataTable = new EquipItemTable.EquipItemData[evolveTable.Length];
				for (int i = 0; i < evolveTable.Length; i++)
				{
					smithEvolveData.evolveEquipDataTable[i] = Singleton<EquipItemTable>.I.GetEquipItemData(evolveTable[i].equipEvolveItemID);
				}
				smithData.evolveData = smithEvolveData;
			}
		}
		Transform root = (!smithData.selectEquipData.tableData.IsWeapon()) ? GetCtrl(UI.OBJ_ARMOR_ROOT) : GetCtrl(UI.OBJ_WEAPON_ROOT);
		Transform val = FindCtrl(root, UI.OBJ_ORDER_CENTER_ANIM_ROOT);
		Transform val2 = FindCtrl(root, UI.OBJ_ORDER_L_ANIM_ROOT);
		Transform val3 = FindCtrl(root, UI.OBJ_ORDER_R_ANIM_ROOT);
		defaultCenterPos = val.get_localPosition();
		defaultRightPos = val2.get_localPosition();
		defaultLeftPos = val3.get_localPosition();
		centerAnim = val.get_gameObject().AddComponent<TweenPosition>();
		rightAnim = val3.get_gameObject().AddComponent<TweenPosition>();
		leftAnim = val2.get_gameObject().AddComponent<TweenPosition>();
		smithType = SmithType.EVOLVE;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		Transform root = (!smithData.selectEquipData.tableData.IsWeapon()) ? GetCtrl(UI.OBJ_ARMOR_ROOT) : GetCtrl(UI.OBJ_WEAPON_ROOT);
		bool flag = smithData.evolveData.evolveEquipDataTable.Length > 1;
		SetActive(root, UI.BTN_EVO_L_INACTIVE, !flag);
		SetActive(root, UI.BTN_EVO_R_INACTIVE, !flag);
		SetColor(root, UI.SPR_EVO_L, (!flag) ? Color.get_clear() : Color.get_white());
		SetColor(root, UI.SPR_EVO_R, (!flag) ? Color.get_clear() : Color.get_white());
		SetLabelText((Enum)UI.LBL_EVO_INDEX, (smithData.evolveData.selectIndex + 1).ToString());
		SetLabelText((Enum)UI.LBL_EVO_INDEX_MAX, smithData.evolveData.evolveTable.Length.ToString());
		int selectIndex = smithData.evolveData.selectIndex;
		SetActive(root, UI.OBJ_ORDER_NORMAL_CENTER, true);
		SetActive(root, UI.OBJ_ORDER_ATTRIBUTE_CENTER, true);
		SetActive(root, UI.OBJ_ORDER_NORMAL_R, true);
		SetActive(root, UI.OBJ_ORDER_NORMAL_L, true);
		SetActive(root, UI.OBJ_ORDER_ATTRIBUTE_R, true);
		SetActive(root, UI.OBJ_ORDER_ATTRIBUTE_L, true);
		SetActive(root, UI.OBJ_ORDER_L2, true);
		SetActive(root, UI.OBJ_ORDER_R2, true);
		SetActive(root, UI.BTN_EVO_R2, flag);
		SetActive(root, UI.BTN_EVO_L2, flag);
		SetActive(root, UI.BTN_EVO_L2_INACTIVE, !flag);
		SetActive(root, UI.BTN_EVO_R2_INACTIVE, !flag);
		SetEvolveText(smithData.evolveData.evolveEquipDataTable[selectIndex]);
		if (!isButtonChange)
		{
			SetModifyPanel(smithData);
		}
		else if (isRightChange)
		{
			this.StartCoroutine("ChangePanelRight", (object)smithData);
		}
		else
		{
			this.StartCoroutine("ChangePanelLeft", (object)smithData);
		}
		isButtonChange = false;
		base.UpdateUI();
	}

	private void SetModifyPanel(SmithManager.SmithGrowData smith_data)
	{
		int num = smith_data.evolveData.evolveEquipDataTable.Length;
		int selectIndex = smith_data.evolveData.selectIndex;
		Transform val = (!smith_data.evolveData.evolveEquipDataTable[selectIndex].IsWeapon()) ? GetCtrl(UI.OBJ_ARMOR_ROOT) : GetCtrl(UI.OBJ_WEAPON_ROOT);
		if (num == 0)
		{
			SetActive((Enum)UI.OBJ_EVOLVE_ROOT, false);
		}
		else if (num == 1)
		{
			SetModifyCenterPanel(smith_data.evolveData.evolveEquipDataTable[selectIndex], val);
			SetActive(val, UI.OBJ_ORDER_L2, false);
			SetActive(val, UI.OBJ_ORDER_R2, false);
		}
		else if (num == 2)
		{
			SetModifyCenterPanel(smith_data.evolveData.evolveEquipDataTable[selectIndex], val);
			if (selectIndex == 0)
			{
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex + 1], true, val);
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex + 1], false, val);
			}
			else
			{
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex - 1], true, val);
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex - 1], false, val);
			}
		}
		else if (num > 2)
		{
			SetModifyCenterPanel(smith_data.evolveData.evolveEquipDataTable[selectIndex], val);
			if (selectIndex == 0)
			{
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex + 1], true, val);
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[num - 1], false, val);
			}
			else if (selectIndex == num - 1)
			{
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[0], true, val);
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex - 1], false, val);
			}
			else
			{
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex + 1], true, val);
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex - 1], false, val);
			}
		}
	}

	private IEnumerator ChangePanelLeft(SmithManager.SmithGrowData data)
	{
		centerAnim.set_enabled(true);
		centerAnim.ResetToBeginning();
		centerAnim.duration = 0.1f;
		centerAnim.from = defaultCenterPos;
		centerAnim.to = new Vector3(defaultCenterPos.x + 200f, defaultCenterPos.y, defaultCenterPos.z);
		centerAnim.PlayForward();
		rightAnim.set_enabled(true);
		rightAnim.ResetToBeginning();
		rightAnim.duration = 0.1f;
		rightAnim.from = defaultRightPos;
		rightAnim.to = new Vector3(defaultRightPos.x + 200f, defaultRightPos.y, defaultRightPos.z);
		rightAnim.PlayForward();
		leftAnim.set_enabled(true);
		leftAnim.ResetToBeginning();
		leftAnim.duration = 0.1f;
		leftAnim.from = defaultLeftPos;
		leftAnim.to = new Vector3(defaultLeftPos.x + 200f, defaultLeftPos.y, defaultLeftPos.z);
		leftAnim.PlayForward();
		while (centerAnim.get_enabled())
		{
			yield return (object)null;
		}
		SetModifyPanel(data);
		centerAnim.set_enabled(true);
		centerAnim.ResetToBeginning();
		centerAnim.duration = 0.1f;
		centerAnim.from = new Vector3(defaultCenterPos.x - 200f, defaultCenterPos.y, defaultCenterPos.z);
		centerAnim.to = defaultCenterPos;
		centerAnim.PlayForward();
		rightAnim.set_enabled(true);
		rightAnim.ResetToBeginning();
		rightAnim.duration = 0.1f;
		rightAnim.from = new Vector3(defaultRightPos.x - 200f, defaultRightPos.y, defaultRightPos.z);
		rightAnim.to = defaultRightPos;
		rightAnim.PlayForward();
		leftAnim.set_enabled(true);
		leftAnim.ResetToBeginning();
		leftAnim.duration = 0.1f;
		leftAnim.from = new Vector3(defaultLeftPos.x - 200f, defaultLeftPos.y, defaultLeftPos.z);
		leftAnim.to = defaultLeftPos;
		leftAnim.PlayForward();
	}

	private IEnumerator ChangePanelRight(SmithManager.SmithGrowData data)
	{
		centerAnim.set_enabled(true);
		centerAnim.ResetToBeginning();
		centerAnim.duration = 0.1f;
		centerAnim.from = defaultCenterPos;
		centerAnim.to = new Vector3(defaultCenterPos.x - 200f, defaultCenterPos.y, defaultCenterPos.z);
		centerAnim.PlayForward();
		rightAnim.set_enabled(true);
		rightAnim.ResetToBeginning();
		rightAnim.duration = 0.1f;
		rightAnim.from = defaultRightPos;
		rightAnim.to = new Vector3(defaultRightPos.x - 200f, defaultRightPos.y, defaultRightPos.z);
		rightAnim.PlayForward();
		leftAnim.set_enabled(true);
		leftAnim.ResetToBeginning();
		leftAnim.duration = 0.1f;
		leftAnim.from = defaultLeftPos;
		leftAnim.to = new Vector3(defaultLeftPos.x - 200f, defaultLeftPos.y, defaultLeftPos.z);
		leftAnim.PlayForward();
		while (centerAnim.get_enabled())
		{
			yield return (object)null;
		}
		SetModifyPanel(data);
		centerAnim.set_enabled(true);
		centerAnim.ResetToBeginning();
		centerAnim.duration = 0.1f;
		centerAnim.from = new Vector3(defaultCenterPos.x + 200f, defaultCenterPos.y, defaultCenterPos.z);
		centerAnim.to = defaultCenterPos;
		centerAnim.PlayForward();
		rightAnim.set_enabled(true);
		rightAnim.ResetToBeginning();
		rightAnim.duration = 0.1f;
		rightAnim.from = new Vector3(defaultRightPos.x + 200f, defaultRightPos.y, defaultRightPos.z);
		rightAnim.to = defaultRightPos;
		rightAnim.PlayForward();
		leftAnim.set_enabled(true);
		leftAnim.ResetToBeginning();
		leftAnim.duration = 0.1f;
		leftAnim.from = new Vector3(defaultLeftPos.x + 200f, defaultLeftPos.y, defaultLeftPos.z);
		leftAnim.to = defaultLeftPos;
		leftAnim.PlayForward();
	}

	private void SetEvolveText(EquipItemTable.EquipItemData data)
	{
		bool flag = data.IsWeapon();
		int num = 0;
		num = ((!flag) ? data.GetElemDefTypePriorityToTable(null) : data.GetElemAtkTypePriorityToTable(null));
		modifyText = StringTable.Get(STRING_CATEGORY.EVOLVE, (uint)num);
	}

	private void SetModifyCenterPanel(EquipItemTable.EquipItemData data, Transform rootObj)
	{
		bool flag = data.IsWeapon();
		SetActive((Enum)UI.OBJ_ARMOR_ROOT, !flag);
		SetActive((Enum)UI.OBJ_WEAPON_ROOT, flag);
		int num = 0;
		if (flag)
		{
			num = data.GetElemAtkTypePriorityToTable(null);
			string spTypeTextSpriteName = data.spAttackType.GetSpTypeTextSpriteName();
			SetSprite(rootObj, UI.SPR_ORDER_ACTIONTYPE_CENTER, spTypeTextSpriteName);
		}
		else
		{
			num = data.GetElemDefTypePriorityToTable(null);
		}
		switch (num)
		{
		case 6:
			SetActive(rootObj, UI.OBJ_ORDER_ATTRIBUTE_CENTER, false);
			break;
		default:
			SetElementSprite(rootObj, UI.SPR_ORDER_ELEM_CENTER, num);
			SetActive(rootObj, UI.OBJ_ORDER_NORMAL_CENTER, false);
			break;
		}
	}

	private void SetModifySidePanel(EquipItemTable.EquipItemData data, bool isRight, Transform rootObj)
	{
		bool flag = data.IsWeapon();
		SetActive((Enum)UI.OBJ_ARMOR_ROOT, !flag);
		SetActive((Enum)UI.OBJ_WEAPON_ROOT, flag);
		int num = 0;
		if (flag)
		{
			num = data.GetElemAtkTypePriorityToTable(null);
			string spTypeTextSpriteName = data.spAttackType.GetSpTypeTextSpriteName();
			bool flag2 = data.spAttackType == SP_ATTACK_TYPE.NONE;
			if (isRight)
			{
				SetSprite(rootObj, UI.SPR_ORDER_ACTIONTYPE_RIGHT, spTypeTextSpriteName);
			}
			else
			{
				SetSprite(rootObj, UI.SPR_ORDER_ACTIONTYPE_LEFT, spTypeTextSpriteName);
			}
		}
		else
		{
			num = data.GetElemDefTypePriorityToTable(null);
		}
		switch (num)
		{
		case 6:
			if (isRight)
			{
				SetActive(rootObj, UI.OBJ_ORDER_ATTRIBUTE_R, false);
			}
			else
			{
				SetActive(rootObj, UI.OBJ_ORDER_ATTRIBUTE_L, false);
			}
			break;
		default:
			if (isRight)
			{
				SetElementSprite(rootObj, UI.SPR_ORDER_ELEM_R, num);
				SetActive(rootObj, UI.OBJ_ORDER_NORMAL_R, false);
			}
			else
			{
				SetElementSprite(rootObj, UI.SPR_ORDER_ELEM_L, num);
				SetActive(rootObj, UI.OBJ_ORDER_NORMAL_L, false);
			}
			break;
		}
	}

	protected override void InitNeedMaterialData()
	{
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		needMaterial = smithData?.evolveData.GetEvolveTable().needMaterial;
		needMaterial = MaterialSort(needMaterial);
		needMoney = ((smithData != null) ? ((int)smithData.evolveData.GetEvolveTable().needMoney) : 0);
		needEquip = smithData?.evolveData.GetEvolveTable().needEquip;
		CheckNeedMaterialNumFromInventory();
	}

	protected unsafe override void EquipTableParam()
	{
		base.EquipTableParam();
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		EquipItemTable.EquipItemData equipTable = smithData.evolveData.GetEquipTable();
		SkillSlotUIData[] evolveInheritanceSkill = GetEvolveInheritanceSkill(GetSkillSlotData(smithData.selectEquipData), equipTable, smithData.selectEquipData.exceed);
		AbilityItemInfo abilityItem = smithData.selectEquipData.GetAbilityItem();
		SetSkillIconButton(UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", equipTable, evolveInheritanceSkill, "SKILL_ICON_BUTTON", 0);
		adapterAbilityList.Clear();
		if (!object.ReferenceEquals(equipTable.fixedAbility, null) && equipTable.fixedAbility.Length > 0)
		{
			int i = 0;
			for (int num = equipTable.fixedAbility.Length; i < num; i++)
			{
				AdapterAbility adapterAbility = new AdapterAbility();
				adapterAbility.Set(equipTable.fixedAbility[i]);
				adapterAbilityList.Add(adapterAbility);
			}
		}
		EquipItemInfo selectEquipData = smithData.selectEquipData;
		if (!object.ReferenceEquals(selectEquipData.ability, null) && selectEquipData.ability.Length > 0)
		{
			int j = 0;
			for (int num2 = selectEquipData.ability.Length; j < num2; j++)
			{
				if (!selectEquipData.IsFixedAbility(j))
				{
					AdapterAbility adapterAbility2 = new AdapterAbility();
					adapterAbility2.Set(selectEquipData.ability[j]);
					adapterAbilityList.Add(adapterAbility2);
				}
			}
		}
		if (adapterAbilityList.Count > 0 || abilityItem != null)
		{
			bool empty_ability = true;
			_003CEquipTableParam_003Ec__AnonStorey461 _003CEquipTableParam_003Ec__AnonStorey;
			SetTable(UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", adapterAbilityList.Count + ((abilityItem != null) ? 1 : 0), false, new Action<int, Transform, bool>((object)_003CEquipTableParam_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			if (empty_ability)
			{
				SetActive((Enum)UI.STR_NON_ABILITY, true);
			}
			else
			{
				SetActive((Enum)UI.STR_NON_ABILITY, false);
			}
		}
		else
		{
			SetActive((Enum)UI.STR_NON_ABILITY, true);
		}
	}

	protected override void EquipImg()
	{
	}

	protected override string GetEquipItemName()
	{
		string result = string.Empty;
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		if (smithData != null)
		{
			result = smithData.evolveData.evolveBeforeEquipData.tableData.name;
		}
		return result;
	}

	protected override void OnQuery_SKILL_ICON_BUTTON()
	{
		EquipItemAndSkillData equipItemAndSkillData = new EquipItemAndSkillData();
		equipItemAndSkillData.equipItemInfo = new EquipItemInfo();
		equipItemAndSkillData.equipItemInfo.tableData = GetEquipTableData();
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		SkillSlotUIData[] array = equipItemAndSkillData.skillSlotUIData = GetEvolveInheritanceSkill(GetSkillSlotData(smithData.selectEquipData), smithData.evolveData.GetEquipTable(), smithData.selectEquipData.exceed);
		GameSection.SetEventData(new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.SMITH_EVOLVE,
			equipItemAndSkillData
		});
	}

	private void OnQuery_BACK()
	{
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		if (smithData.evolveData.evolveTable.Length == 1)
		{
			GameSection.ChangeEvent("TO_SELECT", null);
		}
	}

	protected override void OnQuery_START()
	{
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		if (smithData != null)
		{
			SmithManager.ERR_SMITH_SEND eRR_SMITH_SEND = MonoBehaviourSingleton<SmithManager>.I.CheckEvolveEquipItem(smithData.evolveData.evolveBeforeEquipData, smithData.evolveData.GetEvolveTable().id, selectedUniqueIdList);
			if (eRR_SMITH_SEND != 0)
			{
				GameSection.ChangeEvent(eRR_SMITH_SEND.ToString(), null);
			}
			else
			{
				isDialogEventYES = false;
				GameSection.SetEventData(new object[2]
				{
					GetEquipItemName() + " ",
					" " + modifyText + " "
				});
			}
		}
	}

	private void OnQuery_SmithConfirmEvolve_YES()
	{
		OnQueryConfirmYES();
	}

	protected unsafe override void Send()
	{
		SmithManager.SmithGrowData data = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		if (data == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			SmithManager.ResultData result_data = new SmithManager.ResultData();
			GameSection.SetEventData(result_data);
			GameSection.StayEvent();
			_003CSend_003Ec__AnonStorey463 _003CSend_003Ec__AnonStorey;
			MonoBehaviourSingleton<SmithManager>.I.SendEvolveEquipItem(data.evolveData.evolveBeforeEquipData.uniqueID, data.evolveData.GetEvolveTable().id, selectedUniqueIdList, new Action<Error, EquipItemInfo>((object)_003CSend_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void OnQuery_EVO_L()
	{
		if (!isModelScrolling)
		{
			selectedUniqueIdList = null;
			MoveLeftIndex();
			RefreshUI();
		}
	}

	private void OnQuery_EVO_R()
	{
		if (!isModelScrolling)
		{
			selectedUniqueIdList = null;
			MoveRightIndex();
			RefreshUI();
		}
	}

	private void MoveLeftIndex()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		int num = smithData.evolveData.evolveEquipDataTable.Length;
		int selectIndex = smithData.evolveData.selectIndex;
		if (--smithData.evolveData.selectIndex < 0)
		{
			smithData.evolveData.selectIndex = num - 1;
		}
		rotateSign = -1f;
		this.StartCoroutine(ChangeLeftModel(selectIndex, smithData.evolveData.selectIndex));
		isButtonChange = true;
		isRightChange = false;
	}

	private void MoveRightIndex()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		int num = smithData.evolveData.evolveEquipDataTable.Length;
		int selectIndex = smithData.evolveData.selectIndex;
		if (++smithData.evolveData.selectIndex >= num)
		{
			smithData.evolveData.selectIndex = 0;
		}
		rotateSign = 1f;
		this.StartCoroutine(ChangeRightModel(selectIndex, smithData.evolveData.selectIndex));
		isButtonChange = true;
		isRightChange = true;
	}

	private void UpdateModel()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		DeleteItemModelObject();
		this.StopCoroutine("DoLoadModel");
		this.StartCoroutine("DoLoadModel");
	}

	private IEnumerator DoLoadModel()
	{
		InitRenderTexture(UI.TEX_MODEL, 45f, false);
		SmithManager.SmithGrowData smith_data = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		int max = smith_data.evolveData.evolveEquipDataTable.Length;
		itemModelRoot = Utility.CreateGameObject("ItemModelRoot", GetRenderTextureModelTransform(UI.TEX_MODEL), -1);
		itemModelRoot.set_localPosition(Vector3.get_up() * 100f);
		itemModelRoot.set_localEulerAngles(Vector3.get_zero());
		itemModels = (Transform[])new Transform[max];
		bool[] load_complete = new bool[max];
		for (int l = 0; l < max; l++)
		{
			LoadItemModelData(l, max, delegate(int index)
			{
				((_003CDoLoadModel_003Ec__Iterator168)/*Error near IL_0101: stateMachine*/)._003Cload_complete_003E__2[index] = true;
			});
		}
		while (true)
		{
			bool is_wait = false;
			for (int k = 0; k < max; k++)
			{
				if (!load_complete[k])
				{
					is_wait = true;
					break;
				}
			}
			if (!is_wait)
			{
				break;
			}
			yield return (object)null;
		}
		SmithManager.SmithGrowData data = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		defaultModelPos = itemModels[data.evolveData.selectIndex].get_transform().get_localPosition();
		animPosList = new List<TweenPosition>();
		for (int j = 0; j < max; j++)
		{
			TweenPosition animPos = itemModels[j].get_gameObject().AddComponent<TweenPosition>();
			animPosList.Add(animPos);
			animPos.set_enabled(false);
		}
		for (int i = 0; i < max; i++)
		{
			if (i != data.evolveData.selectIndex)
			{
				itemModels[i].set_localPosition(new Vector3(10f, 0f, 0f));
			}
		}
		EnableRenderTexture(UI.TEX_MODEL);
	}

	private unsafe void LoadItemModelData(int i, int max, Action<int> callback)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		ItemLoader loader;
		if (itemModels[i] == null)
		{
			itemModels[i] = Utility.CreateGameObject("ItemModel", itemModelRoot, -1);
			itemModels[i].set_localPosition(new Vector3((float)(i * 10), 0f, 0f));
		}
		else
		{
			loader = itemModels[i].get_gameObject().GetComponent<ItemLoader>();
			if (loader != null)
			{
				loader.Clear();
				Object.DestroyImmediate(loader);
			}
		}
		loader = itemModels[i].get_gameObject().AddComponent<ItemLoader>();
		_003CLoadItemModelData_003Ec__AnonStorey464 _003CLoadItemModelData_003Ec__AnonStorey;
		loader.LoadEquip(smithData.evolveData.evolveEquipDataTable[i].id, GetRenderTextureModelTransform(UI.TEX_MODEL), GetRenderTextureLayer(UI.TEX_MODEL), -1, -1, new Action((object)_003CLoadItemModelData_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected override void OnOpen()
	{
		if (itemModelRoot == null)
		{
			UpdateModel();
		}
		localRotateWait = 0f;
		localRotate = 0f;
		selectedUniqueIdList = (GameSection.GetEventData() as ulong[]);
	}

	protected override void OnClose()
	{
		DeleteItemModelObject();
		adapterAbilityList.Clear();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		DeleteItemModelObject();
		adapterAbilityList.Clear();
	}

	private void DeleteItemModelObject()
	{
		DeleteRenderTexture((Enum)UI.TEX_MODEL);
		itemModels = null;
		itemModelRoot = null;
	}

	private void TurnItemModel(int before_index, int index, int max, bool is_immediate = true)
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		float num = 360f / (float)max * (float)before_index;
		float num2 = 360f / (float)max * (float)index;
		nowAngle = 10f;
		if (rotateSign > 0f)
		{
			if (num2 < num)
			{
				num2 += 360f;
			}
		}
		else if (num < num2)
		{
			num += 360f;
		}
		targetAngle = Mathf.Abs(num2 - num);
		if (!is_immediate)
		{
			isModelScrolling = true;
		}
		else
		{
			int i = 0;
			for (int num3 = itemModels.Length; i < num3; i++)
			{
				itemModels[i].get_transform().set_localPosition((index != i) ? (Vector3.get_back() * 100f) : Vector3.get_zero());
			}
			isModelScrolling = false;
		}
	}

	private IEnumerator ChangeRightModel(int beforeIndex, int selectIndex)
	{
		if (animPosList[beforeIndex] != null)
		{
			animPosList[beforeIndex].set_enabled(true);
			animPosList[beforeIndex].ResetToBeginning();
			animPosList[beforeIndex].duration = 0.15f;
			animPosList[beforeIndex].from = defaultModelPos;
			animPosList[beforeIndex].to = new Vector3(defaultModelPos.x - 2f, defaultModelPos.y, defaultModelPos.z);
			animPosList[beforeIndex].PlayForward();
		}
		if (animPosList[selectIndex] != null)
		{
			animPosList[selectIndex].set_enabled(true);
			animPosList[selectIndex].ResetToBeginning();
			animPosList[selectIndex].duration = 0.15f;
			animPosList[selectIndex].from = new Vector3(defaultModelPos.x + 2f, defaultModelPos.y, defaultModelPos.z);
			animPosList[selectIndex].to = defaultModelPos;
			animPosList[selectIndex].PlayForward();
		}
		if (animPosList[beforeIndex] != null)
		{
			while (animPosList[beforeIndex].get_enabled())
			{
				yield return (object)null;
			}
		}
		if (animPosList[selectIndex] != null)
		{
			while (animPosList[selectIndex].get_enabled())
			{
				yield return (object)null;
			}
		}
	}

	private IEnumerator ChangeLeftModel(int beforeIndex, int selectIndex)
	{
		if (animPosList[beforeIndex] != null)
		{
			animPosList[beforeIndex].set_enabled(true);
			animPosList[beforeIndex].ResetToBeginning();
			animPosList[beforeIndex].duration = 0.15f;
			animPosList[beforeIndex].from = defaultModelPos;
			animPosList[beforeIndex].to = new Vector3(defaultModelPos.x + 2f, defaultModelPos.y, defaultModelPos.z);
			animPosList[beforeIndex].PlayForward();
		}
		if (animPosList[selectIndex] != null)
		{
			animPosList[selectIndex].set_enabled(true);
			animPosList[selectIndex].ResetToBeginning();
			animPosList[selectIndex].duration = 0.15f;
			animPosList[selectIndex].from = new Vector3(defaultModelPos.x - 2f, defaultModelPos.y, defaultModelPos.z);
			animPosList[selectIndex].to = defaultModelPos;
			animPosList[selectIndex].PlayForward();
		}
		if (animPosList[beforeIndex] != null)
		{
			while (animPosList[beforeIndex].get_enabled())
			{
				yield return (object)null;
			}
		}
		if (animPosList[selectIndex] != null)
		{
			while (animPosList[selectIndex].get_enabled())
			{
				yield return (object)null;
			}
		}
	}

	public void LateUpdate()
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		if (itemModels != null)
		{
			if (isModelScrolling)
			{
				float num = targetAngle * Time.get_deltaTime() * 4f;
				if (nowAngle + num >= targetAngle)
				{
					num = targetAngle - nowAngle;
					isModelScrolling = false;
				}
				else
				{
					nowAngle += num;
				}
				int i = 0;
				for (int num2 = itemModels.Length; i < num2; i++)
				{
					itemModels[i].RotateAround(itemModelRoot.get_position(), itemModelRoot.get_up(), num * rotateSign);
					itemModels[i].set_eulerAngles(new Vector3(0f, 0f, 0f));
				}
			}
			if (localRotateWait < 1f)
			{
				localRotateWait += Time.get_deltaTime();
			}
			else
			{
				localRotate += Time.get_deltaTime() * 10f;
				int j = 0;
				for (int num3 = itemModels.Length; j < num3; j++)
				{
					itemModels[j].set_eulerAngles(new Vector3(0f, 0f, 0f));
					itemModels[j].Rotate(itemModels[j].get_up(), localRotate);
				}
			}
		}
	}

	private void OnQuery_SECTION_BACK()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithGrowItemSelect"))
		{
			GameSection.StopEvent();
			OnQuery_MAIN_MENU_STATUS();
		}
	}

	protected override void OnQuery_ABILITY_DATA_POPUP()
	{
		object[] array = GameSection.GetEventData() as object[];
		int index = (int)array[0];
		Transform targetTrans = array[1] as Transform;
		if (abilityDetailPopUp == null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail((Enum)UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.ShowAbilityDetail(targetTrans);
		abilityDetailPopUp.SetAbilityDetailText(adapterAbilityList[index].ability);
		GameSection.StopEvent();
	}

	private void OnQuery_ABILITY_ITEM_DATA_POPUP()
	{
		Transform targetTrans = GameSection.GetEventData() as Transform;
		AbilityItemInfo abilityItem = GetEquipData().GetAbilityItem();
		if (abilityDetailPopUp == null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail((Enum)UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.ShowAbilityDetail(targetTrans);
		abilityDetailPopUp.SetAbilityDetailText(abilityItem.GetName(), string.Empty, abilityItem.GetDescription());
	}
}
