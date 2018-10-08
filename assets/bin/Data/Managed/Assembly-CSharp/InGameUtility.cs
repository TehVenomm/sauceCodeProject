public class InGameUtility
{
	public class PlayerAtkCalcData
	{
		public SkillInfo.SkillParam skillParam;

		public AttackHitInfo atkInfo;

		public AtkAttribute weaponAtk;

		public float statusAtk;

		public AtkAttribute guardEquipAtk;

		public AtkAttribute buffAtkRate;

		public AtkAttribute passiveAtkRate;

		public AtkAttribute buffAtkConstant;

		public float buffAtkAllElementConstant;

		public AtkAttribute passiveAtkConstant;

		public float passiveAtkAllElementConstant;

		public bool isAtkElementOnly;
	}

	public class EnemyAtkCalcData
	{
		public AttackHitInfo atkInfo;

		public AtkAttribute buffAtkRate;

		public AtkAttribute buffAtkConstant;

		public float buffAtkAllElementConstant;
	}

	public const float COEFFICIENT_DAMAGE = 4.5f;

	public static AtkAttribute CalcPlayerATK(PlayerAtkCalcData calcData)
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		AttackHitInfo atkInfo = calcData.atkInfo;
		if (atkInfo != null)
		{
			atkAttribute.Add(atkInfo.atk);
		}
		SkillInfo.SkillParam skillParam = calcData.skillParam;
		if (skillParam != null && atkInfo != null && atkInfo.isSkillReference)
		{
			atkAttribute.Add(skillParam.atk);
		}
		if (atkInfo == null)
		{
			atkAttribute.Add(calcData.weaponAtk);
		}
		else if (atkInfo.IsReferenceAtkValue)
		{
			atkAttribute.Add(calcData.weaponAtk);
		}
		atkAttribute.normal += calcData.statusAtk;
		atkAttribute.Add(calcData.guardEquipAtk);
		float num = atkAttribute.normal;
		ELEMENT_TYPE eLEMENT_TYPE = atkAttribute.GetElementType();
		AtkAttribute atkAttribute2 = new AtkAttribute();
		atkAttribute2.Copy(atkAttribute);
		atkAttribute2.Mul(calcData.passiveAtkRate);
		atkAttribute.Add(atkAttribute2);
		atkAttribute.Add(calcData.passiveAtkConstant);
		if (calcData.passiveAtkAllElementConstant != 0f)
		{
			atkAttribute.AddElementValueWithCheck(calcData.passiveAtkAllElementConstant);
		}
		atkAttribute.CheckMinus();
		AtkAttribute atkAttribute3 = new AtkAttribute();
		atkAttribute3.Copy(atkAttribute);
		atkAttribute3.Mul(calcData.buffAtkRate);
		atkAttribute.Add(atkAttribute3);
		atkAttribute.Add(calcData.buffAtkConstant);
		if (calcData.buffAtkAllElementConstant > 0f)
		{
			atkAttribute.AddElementValueWithCheck(calcData.buffAtkAllElementConstant);
		}
		if (skillParam != null && atkInfo != null && atkInfo.isSkillReference)
		{
			if (skillParam.tableData.skillAtkType == ELEMENT_TYPE.MAX)
			{
				eLEMENT_TYPE = ELEMENT_TYPE.MAX;
			}
			else
			{
				num = 0f;
			}
			ELEMENT_TYPE type = skillParam.tableData.skillAtkType;
			float val = skillParam.atkRate;
			if (calcData.atkInfo.skillElementIndex > 0 && skillParam.tableData.GetAttackElementNum() > calcData.atkInfo.skillElementIndex)
			{
				type = skillParam.tableData.GetAttackElementByIndex(calcData.atkInfo.skillElementIndex);
				val = skillParam.GetAttackRateByIndex(calcData.atkInfo.skillElementIndex);
			}
			atkAttribute.ChangeElementType(type);
			atkAttribute.Mul(val);
		}
		else if (calcData.isAtkElementOnly && eLEMENT_TYPE != ELEMENT_TYPE.MAX)
		{
			num = 0f;
			atkAttribute.ChangeElementType(eLEMENT_TYPE);
		}
		atkAttribute.CheckMinus();
		if (num > 0f && atkAttribute.normal < 1f)
		{
			atkAttribute.normal = 1f;
		}
		if (eLEMENT_TYPE != ELEMENT_TYPE.MAX && atkAttribute.GetElementType() == ELEMENT_TYPE.MAX)
		{
			atkAttribute.SetTargetElement(eLEMENT_TYPE, 1f);
		}
		return atkAttribute;
	}

	public static AtkAttribute CalcEnemyATK(EnemyAtkCalcData calcData)
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		AttackHitInfo atkInfo = calcData.atkInfo;
		if (atkInfo != null)
		{
			atkAttribute.Add(atkInfo.atk);
		}
		AtkAttribute atkAttribute2 = new AtkAttribute();
		atkAttribute2.Set(0f);
		atkAttribute2.Add(calcData.buffAtkRate);
		AtkAttribute atkAttribute3 = new AtkAttribute();
		atkAttribute3.Copy(atkAttribute);
		atkAttribute3.Mul(atkAttribute2);
		atkAttribute.Add(atkAttribute3);
		atkAttribute.Add(calcData.buffAtkConstant);
		if (calcData.buffAtkAllElementConstant > 0f)
		{
			atkAttribute.AddElementValueWithCheck(calcData.buffAtkAllElementConstant);
		}
		return atkAttribute;
	}

	public static float CalcLevelRate(int enemyLevel)
	{
		int num = enemyLevel;
		if (enemyLevel <= 0)
		{
			num = 1;
		}
		return 100f + (float)(num - 1) * 10f;
	}

	public static float CalcDamageRateToPlayer(float levelRate, float defense, float tolerance, int threshold = -1, float coefficient = 1f)
	{
		if (threshold > 0 && defense > (float)threshold)
		{
			return levelRate / (levelRate * 0.5f + (float)threshold * 4.5f + (defense - (float)threshold) * coefficient + tolerance * 4.5f);
		}
		return levelRate / (levelRate * 0.5f + (defense + tolerance) * 4.5f);
	}

	public static float CalcDamageDetailToEnemy(float atk, float defense, float tolerance)
	{
		return atk * (1f - tolerance) - defense;
	}

	public static bool IsDamageUpAtkTypeNormal(Player player, AttackHitInfo info)
	{
		if (info.attackType != 0 && info.attackType != AttackHitInfo.ATTACK_TYPE.BURST_THS_COMBO03)
		{
			return false;
		}
		if (player.attackMode == Player.ATTACK_MODE.ARROW)
		{
			return true;
		}
		if (info.toEnemy.isSpecialAttack)
		{
			return false;
		}
		if (player.attackMode == Player.ATTACK_MODE.SPEAR && player.attackID == 97)
		{
			return false;
		}
		if (player.actionID == (Character.ACTION_ID)36 || player.actionID == (Character.ACTION_ID)37)
		{
			return false;
		}
		return true;
	}
}
