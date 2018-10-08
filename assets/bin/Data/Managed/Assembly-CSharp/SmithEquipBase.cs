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

	private unsafe void SetupUIFunc()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected O, but got Unknown
		updateTopAreaUI = null;
		switch (type)
		{
		default:
			updateTopAreaUI = Delegate.Combine((Delegate)updateTopAreaUI, (Delegate)GetEquipInfoFunc());
			updateTopAreaUI = Delegate.Combine((Delegate)updateTopAreaUI, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/));
			break;
		case EquipDialogType.RESULT:
			updateTopAreaUI = Delegate.Combine((Delegate)updateTopAreaUI, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/));
			break;
		}
		updateMiddleAreaUI = null;
		switch (type)
		{
		default:
			updateMiddleAreaUI = Delegate.Combine((Delegate)updateMiddleAreaUI, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/));
			break;
		case EquipDialogType.MATERIAL:
			updateMiddleAreaUI = Delegate.Combine((Delegate)updateMiddleAreaUI, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/));
			break;
		case EquipDialogType.RESULT:
			if (smithType != SmithType.SKILL_GROW)
			{
				updateMiddleAreaUI = Delegate.Combine((Delegate)updateMiddleAreaUI, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/));
			}
			else
			{
				updateMiddleAreaUI = Delegate.Combine((Delegate)updateMiddleAreaUI, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/));
			}
			break;
		}
	}

	public override void UpdateUI()
	{
		if (updateTopAreaUI != null)
		{
			updateTopAreaUI.Invoke();
		}
		if (updateMiddleAreaUI != null)
		{
			updateMiddleAreaUI.Invoke();
		}
	}

	private unsafe Action GetEquipInfoFunc()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		if (smithType == SmithType.GENERATE && (type == EquipDialogType.MATERIAL || type == EquipDialogType.SELECT))
		{
			return new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/);
		}
		if (smithType == SmithType.EVOLVE && type == EquipDialogType.MATERIAL)
		{
			return new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/);
		}
		return new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/);
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
