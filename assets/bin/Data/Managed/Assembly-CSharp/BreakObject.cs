using UnityEngine;

public class BreakObject : GimmickObject
{
	[SerializeField]
	protected string breakEffectName = string.Empty;

	protected override bool IsValidAttackedHit(StageObject from_object)
	{
		if (!(from_object is Enemy))
		{
			return false;
		}
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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		base.OnAttackedHitFix(status);
		if (status.damage > 0 && status.fromType == OBJECT_TYPE.ENEMY)
		{
			EffectManager.OneShot(breakEffectName, _position, _rotation, false);
			this.get_gameObject().SetActive(false);
		}
	}
}
