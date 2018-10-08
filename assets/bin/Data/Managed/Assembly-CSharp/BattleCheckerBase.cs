using System;

[Serializable]
public abstract class BattleCheckerBase
{
	private enum SPECIAL_ATTACK_WEAPON_TYPE
	{
		ONE_HAND_SWORD,
		TWO_HAND_SWORD,
		LANCE,
		PAIR_SWORDS,
		MAX_NUM
	}

	public class JudgementParam
	{
		public float exRushChargeRate;

		public float chargeExpandRate;

		public float chargeRate;

		private JudgementParam(float chargeRate, float exRushChargeRate, float chargeExpandRate)
		{
			this.chargeRate = chargeRate;
			this.exRushChargeRate = exRushChargeRate;
			this.chargeExpandRate = chargeExpandRate;
		}

		public static JudgementParam Create(AttackInfo attackInfo, Self self)
		{
			return new JudgementParam(attackInfo.rateInfoRate, self.GetExRushChargeRate(), self.GetChargeExpandingRate());
		}
	}

	private const int ERROR_SPECIAL_ACTION_ID = -1;

	private int specialActionId = -1;

	private string[] specialAttackNames = new string[4]
	{
		"PLC00_attack_",
		"PLC01_attack_",
		"PLC02_attack_",
		"PLC04_attack_"
	};

	private readonly string BOW_CHARGE_ATTACK = "PLC05_attack_00";

	private readonly string TWO_HAND_SWORD_CHARGE_ATTACK_HEAT = "PLC01_attack_88";

	private readonly string ONE_HAND_SWORD_HEAT_COUNTER = "PLC00_attack_98";

	private readonly string ONE_HAND_SWORD_REVENGE_BURST = "PLC00_attack_96";

	private readonly string ONE_HAND_SWORD_BURST_COUNTER = "PLC00_attack_93";

	public bool isEnableAttackCount
	{
		get;
		set;
	}

	public void OnAttackHit(string attackName, JudgementParam judgementParam, int damage = 0)
	{
		bool flag = judgementParam.chargeRate >= 0.99f;
		if (attackName == BOW_CHARGE_ATTACK && flag)
		{
			OnArrowChargeAttack(damage);
		}
		else if (isEnableAttackCount)
		{
			if (attackName == TWO_HAND_SWORD_CHARGE_ATTACK_HEAT)
			{
				OnHeatTwoHandSword(damage);
			}
			else if (attackName == ONE_HAND_SWORD_HEAT_COUNTER)
			{
				OnCounter(damage);
			}
			else if (attackName == ONE_HAND_SWORD_REVENGE_BURST)
			{
				OnRevengeBurst(damage);
			}
			else if (attackName == ONE_HAND_SWORD_BURST_COUNTER)
			{
				OnBurstOneHandSword(damage);
			}
			else
			{
				if (specialActionId == -1)
				{
					if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						return;
					}
					InGameSettingsManager.Player player = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
					specialActionId = player.specialActionInfo.spAttackID;
					int rushLoopAttackID = player.spearActionInfo.rushLoopAttackID;
					for (int i = 0; i < specialAttackNames.Length; i++)
					{
						if (specialAttackNames[i] == specialAttackNames[2])
						{
							specialAttackNames[i] += rushLoopAttackID.ToString();
						}
						else
						{
							specialAttackNames[i] += specialActionId.ToString();
						}
					}
				}
				int num = 0;
				while (true)
				{
					if (num >= specialAttackNames.Length)
					{
						return;
					}
					if (attackName.Contains(specialAttackNames[num]))
					{
						break;
					}
					num++;
				}
				switch (num)
				{
				case 0:
					OnCounter(damage);
					break;
				case 1:
					if (flag)
					{
						OnTwoHandSwordChargeAttack(damage);
					}
					if (judgementParam.chargeExpandRate > 0f)
					{
						OnTwoHandSwordExChargeAttack(damage);
					}
					break;
				case 2:
					OnSpearSpecialAttack(damage);
					if (judgementParam.exRushChargeRate > 0f)
					{
						OnSpearExChargeAttack(damage);
					}
					break;
				case 3:
					OnPairSwordsCombo(damage);
					break;
				}
			}
		}
	}

	protected abstract void OnCounter(int damage);

	protected abstract void OnSpearSpecialAttack(int damage);

	protected abstract void OnSpearExChargeAttack(int damage);

	protected abstract void OnPairSwordsCombo(int damage);

	protected abstract void OnTwoHandSwordChargeAttack(int damage);

	protected abstract void OnTwoHandSwordExChargeAttack(int damage);

	protected abstract void OnArrowChargeAttack(int damage);

	protected abstract void OnHeatTwoHandSword(int damage);

	protected abstract void OnRevengeBurst(int damage);

	protected abstract void OnBurstOneHandSword(int damage);

	public void OnWeakAttack(Enemy.WEAK_STATE weakState, int damage = 0)
	{
		OnNormalWeakHit(damage);
		if (Enemy.IsWeakStateSpAttack(weakState))
		{
			OnWeaponWeakHit(damage);
		}
	}

	protected abstract void OnNormalWeakHit(int damage);

	protected abstract void OnWeaponWeakHit(int damage);
}
