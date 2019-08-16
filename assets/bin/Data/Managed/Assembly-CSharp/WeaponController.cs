using System;

public class WeaponController
{
	[Flags]
	public enum ATTACK_TYPE
	{
		NONE = 0x0,
		ATTACK = 0x1,
		COMBO = 0x2,
		SPECIAL = 0x4,
		GUARD = 0x8,
		AIM = 0x10,
		AVOID_ATTACK = 0x10
	}

	private Brain brain;

	private int _changeIndex = -1;

	public ATTACK_TYPE attackType
	{
		get;
		private set;
	}

	public int beforeAttackId
	{
		get;
		private set;
	}

	public float chargeRate
	{
		get;
		private set;
	}

	public bool isFullCharge => chargeRate >= 1f;

	public int changeIndex => _changeIndex;

	public WeaponController(Brain brain)
	{
		this.brain = brain;
	}

	private void TypeOn(ATTACK_TYPE type)
	{
		attackType |= type;
	}

	private void TypeOff(ATTACK_TYPE type)
	{
		attackType &= ~type;
	}

	private bool TypeIsOn(ATTACK_TYPE type)
	{
		return (attackType & type) == type;
	}

	public void AttackOn()
	{
		TypeOn(ATTACK_TYPE.ATTACK);
	}

	public void ComboOn()
	{
		TypeOn(ATTACK_TYPE.COMBO);
	}

	public void SpecialOn()
	{
		TypeOn(ATTACK_TYPE.SPECIAL);
	}

	public void GuardOn()
	{
		TypeOn(ATTACK_TYPE.GUARD);
	}

	public void AimOn()
	{
		TypeOn(ATTACK_TYPE.AIM);
	}

	public void AvoidAttackOn()
	{
		TypeOn(ATTACK_TYPE.AIM);
	}

	public void AttackOff()
	{
		TypeOff(ATTACK_TYPE.ATTACK);
	}

	public void ComboOff()
	{
		TypeOff(ATTACK_TYPE.COMBO);
	}

	public void SpecialOff()
	{
		TypeOff(ATTACK_TYPE.SPECIAL);
	}

	public void GuardOff()
	{
		TypeOff(ATTACK_TYPE.GUARD);
	}

	public void AimOff()
	{
		TypeOff(ATTACK_TYPE.AIM);
	}

	public void AvoidAttackOff()
	{
		TypeOff(ATTACK_TYPE.AIM);
	}

	public bool IsAttack()
	{
		return TypeIsOn(ATTACK_TYPE.ATTACK);
	}

	public bool IsCombo()
	{
		return TypeIsOn(ATTACK_TYPE.COMBO);
	}

	public bool IsSpecial()
	{
		return TypeIsOn(ATTACK_TYPE.SPECIAL);
	}

	public bool IsGuard()
	{
		return TypeIsOn(ATTACK_TYPE.GUARD);
	}

	public bool IsAim()
	{
		return TypeIsOn(ATTACK_TYPE.AIM);
	}

	public bool IsAvoidAttack()
	{
		return TypeIsOn(ATTACK_TYPE.AIM);
	}

	public void SetBeforeAttackId(int id)
	{
		beforeAttackId = id;
	}

	public void SetChargeRate(float rate)
	{
		chargeRate = rate;
	}

	public bool IsGuardAttack()
	{
		bool result = false;
		if (brain.owner is Player)
		{
			result = (brain.owner as Player).CheckAttackMode(Player.ATTACK_MODE.ONE_HAND_SWORD);
		}
		return result;
	}

	public float GetAttackReach()
	{
		float result = 3f;
		if (brain.owner is Player)
		{
			result = (brain.owner as Player).attackReach;
		}
		return result;
	}

	public float GetSpecialReach()
	{
		float result = 0f;
		if (brain.owner is Player)
		{
			result = (brain.owner as Player).specialReach;
		}
		return result;
	}

	public float GetAvoidAttackReach()
	{
		float result = 0f;
		if (brain.owner is Player)
		{
			result = (brain.owner as Player).avoidAttackReach;
		}
		return result;
	}

	public void SetChangeIndex(int index)
	{
		_changeIndex = index;
	}

	public void ResetChangeIndex()
	{
		_changeIndex = -1;
	}
}
