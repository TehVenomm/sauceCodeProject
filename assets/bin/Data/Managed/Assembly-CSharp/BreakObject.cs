using UnityEngine;

public class BreakObject : GimmickObject
{
	[SerializeField]
	protected string breakEffectName = "";

	protected override bool IsValidAttackedHit(StageObject from_object)
	{
		return base.IsValidAttackedHit(from_object);
	}

	protected override void OnAttackedHitLocal(AttackedHitStatusLocal status)
	{
		base.OnAttackedHitLocal(status);
		status.damage = (int)status.attackInfo.atk.normal;
		status.damage += (int)status.attackInfo.atk.fire;
		status.damage += (int)status.attackInfo.atk.water;
		status.damage += (int)status.attackInfo.atk.thunder;
		status.damage += (int)status.attackInfo.atk.soil;
		status.damage += (int)status.attackInfo.atk.light;
		status.damage += (int)status.attackInfo.atk.dark;
	}

	public override void OnAttackedHitFix(AttackedHitStatusFix status)
	{
		base.OnAttackedHitFix(status);
		if (IsBreakable(status))
		{
			EffectManager.OneShot(breakEffectName, _position, _rotation);
			base.gameObject.SetActive(value: false);
		}
	}

	protected bool IsBreakable(AttackedHitStatusFix _fixedStatus)
	{
		if (_fixedStatus.attackInfo != null && _fixedStatus.attackInfo.isBreakObject)
		{
			return true;
		}
		if (_fixedStatus.fromType == OBJECT_TYPE.ENEMY && _fixedStatus.damage > 0)
		{
			return true;
		}
		if (_fixedStatus.fromType == OBJECT_TYPE.PLAYER)
		{
			return IsBreakableAttack(_fixedStatus.attackInfo.attackType);
		}
		return false;
	}

	protected bool IsBreakableAttack(AttackHitInfo.ATTACK_TYPE _type)
	{
		if ((uint)(_type - 16) <= 1u)
		{
			return true;
		}
		return false;
	}
}
