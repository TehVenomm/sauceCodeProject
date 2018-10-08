using UnityEngine;

public class BreakObject : GimmickObject
{
	[SerializeField]
	protected string breakEffectName = string.Empty;

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
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		base.OnAttackedHitFix(status);
		if (IsBreakable(status))
		{
			EffectManager.OneShot(breakEffectName, _position, _rotation, false);
			this.get_gameObject().SetActive(false);
		}
	}

	protected bool IsBreakable(AttackedHitStatusFix _fixedStatus)
	{
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
		if (_type == AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT || _type == AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST)
		{
			return true;
		}
		return false;
	}
}
