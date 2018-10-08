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
		Transform transform = FindCtrl(root, UI.OBJ_ORDER_CENTER_ANIM_ROOT);
		Transform transform2 = FindCtrl(root, UI.OBJ_ORDER_L_ANIM_ROOT);
		Transform transform3 = FindCtrl(root, UI.OBJ_ORDER_R_ANIM_ROOT);
		defaultCenterPos = transform.localPosition;
		defaultRightPos = transform2.localPosition;
		defaultLeftPos = transform3.localPosition;
		centerAnim = transform.gameObject.AddComponent<TweenPosition>();
		rightAnim = transform3.gameObject.AddComponent<TweenPosition>();
		leftAnim = transform2.gameObject.AddComponent<TweenPosition>();
		smithType = SmithType.EVOLVE;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		Transform root = (!smithData.selectEquipData.tableData.IsWeapon()) ? GetCtrl(UI.OBJ_ARMOR_ROOT) : GetCtrl(UI.OBJ_WEAPON_ROOT);
		bool flag = smithData.evolveData.evolveEquipDataTable.Length > 1;
		SetActive(root, UI.BTN_EVO_L_INACTIVE, !flag);
		SetActive(root, UI.BTN_EVO_R_INACTIVE, !flag);
		SetColor(root, UI.SPR_EVO_L, (!flag) ? Color.clear : Color.white);
		SetColor(root, UI.SPR_EVO_R, (!flag) ? Color.clear : Color.white);
		SetLabelText(UI.LBL_EVO_INDEX, (smithData.evolveData.selectIndex + 1).ToString());
		SetLabelText(UI.LBL_EVO_INDEX_MAX, smithData.evolveData.evolveTable.Length.ToString());
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
			StartCoroutine("ChangePanelRight", smithData);
		}
		else
		{
			StartCoroutine("ChangePanelLeft", smithData);
		}
		isButtonChange = false;
		base.UpdateUI();
	}

	private void SetModifyPanel(SmithManager.SmithGrowData smith_data)
	{
		int num = smith_data.evolveData.evolveEquipDataTable.Length;
		int selectIndex = smith_data.evolveData.selectIndex;
		Transform transform = (!smith_data.evolveData.evolveEquipDataTable[selectIndex].IsWeapon()) ? GetCtrl(UI.OBJ_ARMOR_ROOT) : GetCtrl(UI.OBJ_WEAPON_ROOT);
		if (num == 0)
		{
			SetActive(UI.OBJ_EVOLVE_ROOT, false);
		}
		else if (num == 1)
		{
			SetModifyCenterPanel(smith_data.evolveData.evolveEquipDataTable[selectIndex], transform);
			SetActive(transform, UI.OBJ_ORDER_L2, false);
			SetActive(transform, UI.OBJ_ORDER_R2, false);
		}
		else if (num == 2)
		{
			SetModifyCenterPanel(smith_data.evolveData.evolveEquipDataTable[selectIndex], transform);
			if (selectIndex == 0)
			{
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex + 1], true, transform);
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex + 1], false, transform);
			}
			else
			{
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex - 1], true, transform);
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex - 1], false, transform);
			}
		}
		else if (num > 2)
		{
			SetModifyCenterPanel(smith_data.evolveData.evolveEquipDataTable[selectIndex], transform);
			if (selectIndex == 0)
			{
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex + 1], true, transform);
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[num - 1], false, transform);
			}
			else if (selectIndex == num - 1)
			{
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[0], true, transform);
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex - 1], false, transform);
			}
			else
			{
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex + 1], true, transform);
				SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex - 1], false, transform);
			}
		}
	}

	private IEnumerator ChangePanelLeft(SmithManager.SmithGrowData data)
	{
		centerAnim.enabled = true;
		centerAnim.ResetToBeginning();
		centerAnim.duration = 0.1f;
		centerAnim.from = defaultCenterPos;
		centerAnim.to = new Vector3(defaultCenterPos.x + 200f, defaultCenterPos.y, defaultCenterPos.z);
		centerAnim.PlayForward();
		rightAnim.enabled = true;
		rightAnim.ResetToBeginning();
		rightAnim.duration = 0.1f;
		rightAnim.from = defaultRightPos;
		rightAnim.to = new Vector3(defaultRightPos.x + 200f, defaultRightPos.y, defaultRightPos.z);
		rightAnim.PlayForward();
		leftAnim.enabled = true;
		leftAnim.ResetToBeginning();
		leftAnim.duration = 0.1f;
		leftAnim.from = defaultLeftPos;
		leftAnim.to = new Vector3(defaultLeftPos.x + 200f, defaultLeftPos.y, defaultLeftPos.z);
		leftAnim.PlayForward();
		while (centerAnim.enabled)
		{
			yield return (object)null;
		}
		SetModifyPanel(data);
		centerAnim.enabled = true;
		centerAnim.ResetToBeginning();
		centerAnim.duration = 0.1f;
		centerAnim.from = new Vector3(defaultCenterPos.x - 200f, defaultCenterPos.y, defaultCenterPos.z);
		centerAnim.to = defaultCenterPos;
		centerAnim.PlayForward();
		rightAnim.enabled = true;
		rightAnim.ResetToBeginning();
		rightAnim.duration = 0.1f;
		rightAnim.from = new Vector3(defaultRightPos.x - 200f, defaultRightPos.y, defaultRightPos.z);
		rightAnim.to = defaultRightPos;
		rightAnim.PlayForward();
		leftAnim.enabled = true;
		leftAnim.ResetToBeginning();
		leftAnim.duration = 0.1f;
		leftAnim.from = new Vector3(defaultLeftPos.x - 200f, defaultLeftPos.y, defaultLeftPos.z);
		leftAnim.to = defaultLeftPos;
		leftAnim.PlayForward();
	}

	private IEnumerator ChangePanelRight(SmithManager.SmithGrowData data)
	{
		centerAnim.enabled = true;
		centerAnim.ResetToBeginning();
		centerAnim.duration = 0.1f;
		centerAnim.from = defaultCenterPos;
		centerAnim.to = new Vector3(defaultCenterPos.x - 200f, defaultCenterPos.y, defaultCenterPos.z);
		centerAnim.PlayForward();
		rightAnim.enabled = true;
		rightAnim.ResetToBeginning();
		rightAnim.duration = 0.1f;
		rightAnim.from = defaultRightPos;
		rightAnim.to = new Vector3(defaultRightPos.x - 200f, defaultRightPos.y, defaultRightPos.z);
		rightAnim.PlayForward();
		leftAnim.enabled = true;
		leftAnim.ResetToBeginning();
		leftAnim.duration = 0.1f;
		leftAnim.from = defaultLeftPos;
		leftAnim.to = new Vector3(defaultLeftPos.x - 200f, defaultLeftPos.y, defaultLeftPos.z);
		leftAnim.PlayForward();
		while (centerAnim.enabled)
		{
			yield return (object)null;
		}
		SetModifyPanel(data);
		centerAnim.enabled = true;
		centerAnim.ResetToBeginning();
		centerAnim.duration = 0.1f;
		centerAnim.from = new Vector3(defaultCenterPos.x + 200f, defaultCenterPos.y, defaultCenterPos.z);
		centerAnim.to = defaultCenterPos;
		centerAnim.PlayForward();
		rightAnim.enabled = true;
		rightAnim.ResetToBeginning();
		rightAnim.duration = 0.1f;
		rightAnim.from = new Vector3(defaultRightPos.x + 200f, defaultRightPos.y, defaultRightPos.z);
		rightAnim.to = defaultRightPos;
		rightAnim.PlayForward();
		leftAnim.enabled = true;
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
		SetActive(UI.OBJ_ARMOR_ROOT, !flag);
		SetActive(UI.OBJ_WEAPON_ROOT, flag);
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
		SetActive(UI.OBJ_ARMOR_ROOT, !flag);
		SetActive(UI.OBJ_WEAPON_ROOT, flag);
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

	protected override void EquipTableParam()
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
			int j = 0;
			for (int num = equipTable.fixedAbility.Length; j < num; j++)
			{
				AdapterAbility adapterAbility = new AdapterAbility();
				adapterAbility.Set(equipTable.fixedAbility[j]);
				adapterAbilityList.Add(adapterAbility);
			}
		}
		EquipItemInfo selectEquipData = smithData.selectEquipData;
		if (!object.ReferenceEquals(selectEquipData.ability, null) && selectEquipData.ability.Length > 0)
		{
			int k = 0;
			for (int num2 = selectEquipData.ability.Length; k < num2; k++)
			{
				if (!selectEquipData.IsFixedAbility(k))
				{
					AdapterAbility adapterAbility2 = new AdapterAbility();
					adapterAbility2.Set(selectEquipData.ability[k]);
					adapterAbilityList.Add(adapterAbility2);
				}
			}
		}
		if (adapterAbilityList.Count > 0 || abilityItem != null)
		{
			bool empty_ability = true;
			SetTable(UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", adapterAbilityList.Count + ((abilityItem != null) ? 1 : 0), false, delegate(int i, Transform t, bool is_recycle)
			{
				if (i < adapterAbilityList.Count)
				{
					AdapterAbility adapterAbility3 = adapterAbilityList[i];
					if (adapterAbility3.GetId() == 0)
					{
						SetActive(t, false);
					}
					else
					{
						empty_ability = false;
						SetActive(t, true);
						if (adapterAbility3.isFix)
						{
							SetActive(t, UI.OBJ_ABILITY, false);
							SetActive(t, UI.OBJ_FIXEDABILITY, true);
							SetLabelText(t, UI.LBL_FIXEDABILITY, adapterAbility3.GetName());
							SetLabelText(t, UI.LBL_FIXEDABILITY_NUM, adapterAbility3.GetAP());
						}
						else
						{
							SetLabelText(t, UI.LBL_ABILITY, adapterAbility3.GetName());
							SetLabelText(t, UI.LBL_ABILITY_NUM, adapterAbility3.GetAP());
						}
						SetAbilityItemEvent(t, i, touchAndReleaseButtons);
					}
				}
				else if (abilityItem != null)
				{
					SetActive(t, UI.OBJ_ABILITY, false);
					SetActive(t, UI.OBJ_ABILITY_ITEM, true);
					SetLabelText(t, UI.LBL_ABILITY_ITEM, abilityItem.GetName());
					SetTouchAndRelease(t.GetComponentInChildren<UIButton>().transform, "ABILITY_ITEM_DATA_POPUP", "RELEASE_ABILITY", t);
				}
			});
			if (empty_ability)
			{
				SetActive(UI.STR_NON_ABILITY, true);
			}
			else
			{
				SetActive(UI.STR_NON_ABILITY, false);
			}
		}
		else
		{
			SetActive(UI.STR_NON_ABILITY, true);
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

	protected override void Send()
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
			MonoBehaviourSingleton<SmithManager>.I.SendEvolveEquipItem(data.evolveData.evolveBeforeEquipData.uniqueID, data.evolveData.GetEvolveTable().id, selectedUniqueIdList, delegate(Error err, EquipItemInfo evolve_item)
			{
				if (err == Error.None)
				{
					result_data.itemData = evolve_item;
					result_data.beforeRarity = (int)data.evolveData.evolveBeforeEquipData.tableData.rarity;
					result_data.beforeLevel = data.evolveData.evolveBeforeEquipData.level;
					result_data.beforeMaxLevel = data.evolveData.evolveBeforeEquipData.tableData.maxLv;
					result_data.beforeExceedCnt = data.evolveData.evolveBeforeEquipData.exceed;
					result_data.beforeAtk = data.evolveData.evolveBeforeEquipData.atk;
					result_data.beforeDef = data.evolveData.evolveBeforeEquipData.def;
					result_data.beforeHp = data.evolveData.evolveBeforeEquipData.hp;
					result_data.beforeElemAtk = data.evolveData.evolveBeforeEquipData.elemAtk;
					result_data.beforeElemDef = data.evolveData.evolveBeforeEquipData.elemDef;
					SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
					smithData.selectEquipData = evolve_item;
					MonoBehaviourSingleton<SmithManager>.I.CreateLocalInventory();
					MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = true;
					GameSection.ResumeEvent(true, null);
				}
				else
				{
					GameSection.ResumeEvent(false, null);
				}
			});
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
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		int num = smithData.evolveData.evolveEquipDataTable.Length;
		int selectIndex = smithData.evolveData.selectIndex;
		if (--smithData.evolveData.selectIndex < 0)
		{
			smithData.evolveData.selectIndex = num - 1;
		}
		rotateSign = -1f;
		StartCoroutine(ChangeLeftModel(selectIndex, smithData.evolveData.selectIndex));
		isButtonChange = true;
		isRightChange = false;
	}

	private void MoveRightIndex()
	{
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		int num = smithData.evolveData.evolveEquipDataTable.Length;
		int selectIndex = smithData.evolveData.selectIndex;
		if (++smithData.evolveData.selectIndex >= num)
		{
			smithData.evolveData.selectIndex = 0;
		}
		rotateSign = 1f;
		StartCoroutine(ChangeRightModel(selectIndex, smithData.evolveData.selectIndex));
		isButtonChange = true;
		isRightChange = true;
	}

	private void UpdateModel()
	{
		DeleteItemModelObject();
		StopCoroutine("DoLoadModel");
		StartCoroutine("DoLoadModel");
	}

	private IEnumerator DoLoadModel()
	{
		InitRenderTexture(UI.TEX_MODEL, 45f, false);
		SmithManager.SmithGrowData smith_data = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		int max = smith_data.evolveData.evolveEquipDataTable.Length;
		itemModelRoot = Utility.CreateGameObject("ItemModelRoot", GetRenderTextureModelTransform(UI.TEX_MODEL), -1);
		itemModelRoot.localPosition = Vector3.up * 100f;
		itemModelRoot.localEulerAngles = Vector3.zero;
		itemModels = new Transform[max];
		bool[] load_complete = new bool[max];
		for (int l = 0; l < max; l++)
		{
			LoadItemModelData(l, max, delegate(int index)
			{
				((_003CDoLoadModel_003Ec__Iterator160)/*Error near IL_0101: stateMachine*/)._003Cload_complete_003E__2[index] = true;
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
		defaultModelPos = itemModels[data.evolveData.selectIndex].transform.localPosition;
		animPosList = new List<TweenPosition>();
		for (int j = 0; j < max; j++)
		{
			TweenPosition animPos = itemModels[j].gameObject.AddComponent<TweenPosition>();
			animPosList.Add(animPos);
			animPos.enabled = false;
		}
		for (int i = 0; i < max; i++)
		{
			if (i != data.evolveData.selectIndex)
			{
				itemModels[i].localPosition = new Vector3(10f, 0f, 0f);
			}
		}
		EnableRenderTexture(UI.TEX_MODEL);
	}

	private void LoadItemModelData(int i, int max, Action<int> callback)
	{
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		ItemLoader loader;
		if ((UnityEngine.Object)itemModels[i] == (UnityEngine.Object)null)
		{
			itemModels[i] = Utility.CreateGameObject("ItemModel", itemModelRoot, -1);
			itemModels[i].localPosition = new Vector3((float)(i * 10), 0f, 0f);
		}
		else
		{
			loader = itemModels[i].gameObject.GetComponent<ItemLoader>();
			if ((UnityEngine.Object)loader != (UnityEngine.Object)null)
			{
				loader.Clear();
				UnityEngine.Object.DestroyImmediate(loader);
			}
		}
		loader = itemModels[i].gameObject.AddComponent<ItemLoader>();
		loader.LoadEquip(smithData.evolveData.evolveEquipDataTable[i].id, GetRenderTextureModelTransform(UI.TEX_MODEL), GetRenderTextureLayer(UI.TEX_MODEL), -1, -1, delegate
		{
			itemModelRoot.localPosition = new Vector3(0f, 0f, loader.displayInfo.zFromCamera);
			callback(i);
		});
	}

	protected override void OnOpen()
	{
		if ((UnityEngine.Object)itemModelRoot == (UnityEngine.Object)null)
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
		DeleteRenderTexture(UI.TEX_MODEL);
		itemModels = null;
		itemModelRoot = null;
	}

	private void TurnItemModel(int before_index, int index, int max, bool is_immediate = true)
	{
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
				itemModels[i].transform.localPosition = ((index != i) ? (Vector3.back * 100f) : Vector3.zero);
			}
			isModelScrolling = false;
		}
	}

	private IEnumerator ChangeRightModel(int beforeIndex, int selectIndex)
	{
		if ((UnityEngine.Object)animPosList[beforeIndex] != (UnityEngine.Object)null)
		{
			animPosList[beforeIndex].enabled = true;
			animPosList[beforeIndex].ResetToBeginning();
			animPosList[beforeIndex].duration = 0.15f;
			animPosList[beforeIndex].from = defaultModelPos;
			animPosList[beforeIndex].to = new Vector3(defaultModelPos.x - 2f, defaultModelPos.y, defaultModelPos.z);
			animPosList[beforeIndex].PlayForward();
		}
		if ((UnityEngine.Object)animPosList[selectIndex] != (UnityEngine.Object)null)
		{
			animPosList[selectIndex].enabled = true;
			animPosList[selectIndex].ResetToBeginning();
			animPosList[selectIndex].duration = 0.15f;
			animPosList[selectIndex].from = new Vector3(defaultModelPos.x + 2f, defaultModelPos.y, defaultModelPos.z);
			animPosList[selectIndex].to = defaultModelPos;
			animPosList[selectIndex].PlayForward();
		}
		if ((UnityEngine.Object)animPosList[beforeIndex] != (UnityEngine.Object)null)
		{
			while (animPosList[beforeIndex].enabled)
			{
				yield return (object)null;
			}
		}
		if ((UnityEngine.Object)animPosList[selectIndex] != (UnityEngine.Object)null)
		{
			while (animPosList[selectIndex].enabled)
			{
				yield return (object)null;
			}
		}
	}

	private IEnumerator ChangeLeftModel(int beforeIndex, int selectIndex)
	{
		if ((UnityEngine.Object)animPosList[beforeIndex] != (UnityEngine.Object)null)
		{
			animPosList[beforeIndex].enabled = true;
			animPosList[beforeIndex].ResetToBeginning();
			animPosList[beforeIndex].duration = 0.15f;
			animPosList[beforeIndex].from = defaultModelPos;
			animPosList[beforeIndex].to = new Vector3(defaultModelPos.x + 2f, defaultModelPos.y, defaultModelPos.z);
			animPosList[beforeIndex].PlayForward();
		}
		if ((UnityEngine.Object)animPosList[selectIndex] != (UnityEngine.Object)null)
		{
			animPosList[selectIndex].enabled = true;
			animPosList[selectIndex].ResetToBeginning();
			animPosList[selectIndex].duration = 0.15f;
			animPosList[selectIndex].from = new Vector3(defaultModelPos.x - 2f, defaultModelPos.y, defaultModelPos.z);
			animPosList[selectIndex].to = defaultModelPos;
			animPosList[selectIndex].PlayForward();
		}
		if ((UnityEngine.Object)animPosList[beforeIndex] != (UnityEngine.Object)null)
		{
			while (animPosList[beforeIndex].enabled)
			{
				yield return (object)null;
			}
		}
		if ((UnityEngine.Object)animPosList[selectIndex] != (UnityEngine.Object)null)
		{
			while (animPosList[selectIndex].enabled)
			{
				yield return (object)null;
			}
		}
	}

	public void LateUpdate()
	{
		if (itemModels != null)
		{
			if (isModelScrolling)
			{
				float num = targetAngle * Time.deltaTime * 4f;
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
					itemModels[i].RotateAround(itemModelRoot.position, itemModelRoot.up, num * rotateSign);
					itemModels[i].eulerAngles = new Vector3(0f, 0f, 0f);
				}
			}
			if (localRotateWait < 1f)
			{
				localRotateWait += Time.deltaTime;
			}
			else
			{
				localRotate += Time.deltaTime * 10f;
				int j = 0;
				for (int num3 = itemModels.Length; j < num3; j++)
				{
					itemModels[j].eulerAngles = new Vector3(0f, 0f, 0f);
					itemModels[j].Rotate(itemModels[j].up, localRotate);
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
		if ((UnityEngine.Object)abilityDetailPopUp == (UnityEngine.Object)null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail(UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.ShowAbilityDetail(targetTrans);
		abilityDetailPopUp.SetAbilityDetailText(adapterAbilityList[index].ability);
		GameSection.StopEvent();
	}

	private void OnQuery_ABILITY_ITEM_DATA_POPUP()
	{
		Transform targetTrans = GameSection.GetEventData() as Transform;
		AbilityItemInfo abilityItem = GetEquipData().GetAbilityItem();
		if ((UnityEngine.Object)abilityDetailPopUp == (UnityEngine.Object)null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail(UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.ShowAbilityDetail(targetTrans);
		abilityDetailPopUp.SetAbilityDetailText(abilityItem.GetName(), string.Empty, abilityItem.GetDescription());
	}
}
