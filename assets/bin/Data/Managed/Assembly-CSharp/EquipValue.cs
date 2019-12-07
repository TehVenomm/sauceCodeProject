using Network;
using System.Collections.Generic;

public class EquipValue
{
	public class SkillSupport
	{
		public ENABLE_EQUIP_TYPE targetEquip;

		public BuffParam.BUFFTYPE type;

		public SP_ATTACK_TYPE targetSpAttackType;

		public int value;

		public SkillSupport(ENABLE_EQUIP_TYPE e, BuffParam.BUFFTYPE t, int v, SP_ATTACK_TYPE spAttackType)
		{
			targetEquip = e;
			type = t;
			value = v;
			targetSpAttackType = spAttackType;
		}
	}

	public EQUIPMENT_TYPE type;

	public SP_ATTACK_TYPE spAttackType;

	public SimpleStatus baseStatus = new SimpleStatus();

	public int constHp;

	public int[] constAtks = new int[7];

	public int[] constDefs = new int[7];

	public int[] constTols = new int[6];

	public List<SkillSupport> skillSupport = new List<SkillSupport>();

	public Dictionary<int, int> ability = new Dictionary<int, int>();

	private void _Reset()
	{
		type = EQUIPMENT_TYPE.NONE;
		spAttackType = SP_ATTACK_TYPE.NONE;
		baseStatus.Reset();
		constHp = 0;
		for (int i = 0; i < 7; i++)
		{
			constAtks[i] = 0;
			constDefs[i] = 0;
		}
		for (int j = 0; j < 6; j++)
		{
			constTols[j] = 0;
		}
		skillSupport.Clear();
		ability.Clear();
	}

	public void Parse(CharaInfo.EquipItem item, EquipItemTable.EquipItemData data)
	{
		_Reset();
		type = data.type;
		spAttackType = data.spAttackType;
		GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(data.growID, (uint)item.lv);
		if (growEquipItemData == null)
		{
			baseStatus.hp = data.baseHp;
			baseStatus.attacks[0] = data.baseAtk;
			baseStatus.defences[0] = data.baseDef;
			for (int i = 0; i < 6; i++)
			{
				baseStatus.attacks[i + 1] = data.atkElement[i];
				baseStatus.tolerances[i] = data.defElement[i];
			}
		}
		else
		{
			baseStatus.hp = growEquipItemData.GetGrowParamHp(data.baseHp);
			baseStatus.attacks[0] = growEquipItemData.GetGrowParamAtk(data.baseAtk);
			baseStatus.defences[0] = growEquipItemData.GetGrowParamDef(data.baseDef);
			int[] growParamElemAtk = growEquipItemData.GetGrowParamElemAtk(data.atkElement);
			int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(data.defElement);
			for (int j = 0; j < 6; j++)
			{
				baseStatus.attacks[j + 1] = growParamElemAtk[j];
				baseStatus.tolerances[j] = growParamElemDef[j];
			}
		}
		EquipItemExceedParamTable.EquipItemExceedParamAll exceedParam = data.GetExceedParam((uint)item.exceed);
		if (exceedParam != null)
		{
			baseStatus.hp += exceedParam.hp;
			baseStatus.attacks[0] += exceedParam.atk;
			baseStatus.defences[0] += exceedParam.def;
			for (int k = 0; k < 6; k++)
			{
				baseStatus.attacks[k + 1] += exceedParam.atkElement[k];
				baseStatus.tolerances[k] += exceedParam.defElement[k];
			}
		}
		int l = 0;
		for (int count = item.sIds.Count; l < count; l++)
		{
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)item.sIds[l]);
			GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, item.sLvs[l], item.GetSkillExceed(l));
			constHp += growSkillItemData.GetGrowParamHp(skillItemData.baseHp);
			constAtks[0] += growSkillItemData.GetGrowParamAtk(skillItemData.baseAtk);
			constDefs[0] += growSkillItemData.GetGrowParamDef(skillItemData.baseDef);
			int[] growParamElemAtk2 = growSkillItemData.GetGrowParamElemAtk(skillItemData.atkElement);
			int[] growParamElemDef2 = growSkillItemData.GetGrowParamElemDef(skillItemData.defElement);
			for (int m = 0; m < 6; m++)
			{
				constAtks[m + 1] += growParamElemAtk2[m];
				constTols[m] += growParamElemDef2[m];
			}
			if (!skillItemData.IsPassive())
			{
				continue;
			}
			int n = 0;
			for (int num = skillItemData.supportType.Length; n < num; n++)
			{
				if (skillItemData.supportType[n] != BuffParam.BUFFTYPE.NONE)
				{
					skillSupport.Add(new SkillSupport(skillItemData.supportPassiveEqType[n], skillItemData.supportType[n], growSkillItemData.GetGrowParamSupprtValue(skillItemData.supportValue, n), skillItemData.supportPassiveSpAttackType));
				}
			}
		}
		int num2 = 0;
		for (int count2 = item.aIds.Count; num2 < count2; num2++)
		{
			int key = item.aIds[num2];
			int num3 = item.aPts[num2];
			if (this.ability.ContainsKey(key))
			{
				this.ability[key] += num3;
			}
			else
			{
				this.ability.Add(key, num3);
			}
		}
		int num4 = 0;
		for (int num5 = data.fixedAbility.Length; num4 < num5; num4++)
		{
			EquipItem.Ability ability = data.fixedAbility[num4];
			if (this.ability.ContainsKey(ability.id))
			{
				this.ability[ability.id] += ability.pt;
			}
			else
			{
				this.ability.Add(ability.id, ability.pt);
			}
		}
		if (exceedParam == null)
		{
			return;
		}
		int num6 = 0;
		for (int num7 = exceedParam.ability.Length; num6 < num7; num6++)
		{
			EquipItem.Ability ability2 = exceedParam.ability[num6];
			if (this.ability.ContainsKey(ability2.id))
			{
				this.ability[ability2.id] += ability2.pt;
			}
			else
			{
				this.ability.Add(ability2.id, ability2.pt);
			}
		}
	}
}
