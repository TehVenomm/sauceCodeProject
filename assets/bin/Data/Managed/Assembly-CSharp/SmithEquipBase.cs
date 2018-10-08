using System;

public abstract class SmithEquipBase : SkillInfoBase
{
	public enum EquipDialogType
	{
		SELECT,
		MATERIAL,
		RESULT
	}

	public enum SmithType
	{
		GENERATE,
		GROW,
		EVOLVE,
		SKILL_GROW,
		ABILITY_CHANGE,
		REVERT_LITHOGRAPH
	}

	protected SkillItemInfo[] equipAttachSkill;

	protected EquipDialogType type;

	protected SmithType smithType = SmithType.GROW;

	protected Action updateTopAreaUI;

	protected Action updateMiddleAreaUI;

	protected EquipItemInfo GetEquipData()
	{
		return MonoBehaviourSingleton<SmithManager>.I.GetSmithEquipItemInfo(smithType);
	}

	protected EquipItemTable.EquipItemData GetEquipTableData()
	{
		return MonoBehaviourSingleton<SmithManager>.I.GetSmithEquipItemTable(smithType);
	}

	public override void Initialize()
	{
		SetupUIFunc();
		base.Initialize();
	}

	private void SetupUIFunc()
	{
		updateTopAreaUI = null;
		switch (type)
		{
		default:
			updateTopAreaUI = (Action)Delegate.Combine(updateTopAreaUI, GetEquipInfoFunc());
			updateTopAreaUI = (Action)Delegate.Combine(updateTopAreaUI, new Action(EquipImg));
			break;
		case EquipDialogType.RESULT:
			updateTopAreaUI = (Action)Delegate.Combine(updateTopAreaUI, new Action(EquipImg));
			break;
		}
		updateMiddleAreaUI = null;
		switch (type)
		{
		default:
			updateMiddleAreaUI = (Action)Delegate.Combine(updateMiddleAreaUI, new Action(LocalInventory));
			break;
		case EquipDialogType.MATERIAL:
			updateMiddleAreaUI = (Action)Delegate.Combine(updateMiddleAreaUI, new Action(NeededMaterial));
			break;
		case EquipDialogType.RESULT:
			if (smithType != SmithType.SKILL_GROW)
			{
				updateMiddleAreaUI = (Action)Delegate.Combine(updateMiddleAreaUI, new Action(ResultEquipInfo));
			}
			else
			{
				updateMiddleAreaUI = (Action)Delegate.Combine(updateMiddleAreaUI, new Action(ResultSKillInfo));
			}
			break;
		}
	}

	public override void UpdateUI()
	{
		if (updateTopAreaUI != null)
		{
			updateTopAreaUI();
		}
		if (updateMiddleAreaUI != null)
		{
			updateMiddleAreaUI();
		}
	}

	private Action GetEquipInfoFunc()
	{
		if (smithType == SmithType.GENERATE && (type == EquipDialogType.MATERIAL || type == EquipDialogType.SELECT))
		{
			return EquipTableParam;
		}
		if (smithType == SmithType.EVOLVE && type == EquipDialogType.MATERIAL)
		{
			return EquipTableParam;
		}
		return EquipParam;
	}

	protected virtual void EquipParam()
	{
	}

	protected virtual void EquipTableParam()
	{
	}

	protected virtual void EquipImg()
	{
	}

	protected virtual void NeededMaterial()
	{
	}

	protected virtual void ResultEquipInfo()
	{
	}

	protected virtual void ResultSKillInfo()
	{
	}

	protected virtual void LocalInventory()
	{
	}

	public SortBase.TYPE EquipmentTypeToSortBaseType(EQUIPMENT_TYPE type)
	{
		switch (type)
		{
		default:
			return SortBase.TYPE.NONE;
		case EQUIPMENT_TYPE.ONE_HAND_SWORD:
			return SortBase.TYPE.ONE_HAND_SWORD;
		case EQUIPMENT_TYPE.TWO_HAND_SWORD:
			return SortBase.TYPE.TWO_HAND_SWORD;
		case EQUIPMENT_TYPE.SPEAR:
			return SortBase.TYPE.SPEAR;
		case EQUIPMENT_TYPE.PAIR_SWORDS:
			return SortBase.TYPE.PAIR_SWORDS;
		case EQUIPMENT_TYPE.ARROW:
			return SortBase.TYPE.ARROW;
		case EQUIPMENT_TYPE.ARMOR:
			return SortBase.TYPE.ARMOR;
		case EQUIPMENT_TYPE.HELM:
			return SortBase.TYPE.HELM;
		case EQUIPMENT_TYPE.ARM:
			return SortBase.TYPE.ARM;
		case EQUIPMENT_TYPE.LEG:
			return SortBase.TYPE.LEG;
		}
	}

	public EQUIPMENT_TYPE SortBaseTypeToEquipmentType(SortBase.TYPE type)
	{
		switch (type)
		{
		default:
			return EQUIPMENT_TYPE.ONE_HAND_SWORD;
		case SortBase.TYPE.ONE_HAND_SWORD:
			return EQUIPMENT_TYPE.ONE_HAND_SWORD;
		case SortBase.TYPE.TWO_HAND_SWORD:
			return EQUIPMENT_TYPE.TWO_HAND_SWORD;
		case SortBase.TYPE.SPEAR:
			return EQUIPMENT_TYPE.SPEAR;
		case SortBase.TYPE.PAIR_SWORDS:
			return EQUIPMENT_TYPE.PAIR_SWORDS;
		case SortBase.TYPE.ARROW:
			return EQUIPMENT_TYPE.ARROW;
		case SortBase.TYPE.ARMOR:
			return EQUIPMENT_TYPE.ARMOR;
		case SortBase.TYPE.HELM:
			return EQUIPMENT_TYPE.HELM;
		case SortBase.TYPE.ARM:
			return EQUIPMENT_TYPE.ARM;
		case SortBase.TYPE.LEG:
			return EQUIPMENT_TYPE.LEG;
		}
	}
}
