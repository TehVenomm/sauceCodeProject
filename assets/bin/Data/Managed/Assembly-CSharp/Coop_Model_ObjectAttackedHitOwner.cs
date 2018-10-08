using UnityEngine;

public class Coop_Model_ObjectAttackedHitOwner : Coop_Model_ObjectBase
{
	public string attackInfoName;

	public float attackInfoRate;

	public int fromObjectID;

	public int fromType;

	public Vector3 fromPos = Vector3.get_zero();

	public Vector3 hitPos = Vector3.get_zero();

	public int fromClientID;

	public int skillIndex = -1;

	public int regionID = -1;

	public int weakState;

	public int damage;

	public bool validDamage;

	public float downAddBase;

	public float downAddWeak;

	public bool isArrowBleed;

	public int arrowBleedDamage;

	public int arrowBurstDamage;

	public BadStatus badStatusAdd = new BadStatus();

	public bool isSpAttackHit;

	public AtkAttribute damageDetails = new AtkAttribute();

	public bool isShadowSealing;

	public Coop_Model_ObjectAttackedHitOwner()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.OBJECT_ATTACKED_HIT_OWNER;
	}

	public void SetAttackedHitStatus(AttackedHitStatusOwner status)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		attackInfoName = status.attackInfo.name;
		attackInfoRate = status.attackInfo.rateInfoRate;
		fromObjectID = status.fromObjectID;
		fromType = (int)status.fromType;
		fromPos = status.fromPos;
		hitPos = status.hitPos;
		fromClientID = status.fromClientID;
		if (status.skillParam != null)
		{
			skillIndex = status.skillParam.skillIndex;
		}
		regionID = status.regionID;
		weakState = (int)status.weakState;
		damage = status.damage;
		validDamage = status.validDamage;
		downAddBase = status.downAddBase;
		downAddWeak = status.downAddWeak;
		isArrowBleed = status.isArrowBleed;
		arrowBleedDamage = status.arrowBleedDamage;
		arrowBurstDamage = status.arrowBurstDamage;
		badStatusAdd.Copy(status.badStatusAdd);
		isSpAttackHit = status.IsSpAttackHit;
		damageDetails = status.damageDetails;
		isShadowSealing = status.isShadowSealing;
	}

	public void CopyAttackedHitStatus(out AttackedHitStatusOwner status)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		AttackedHitStatus attackedHitStatus = new AttackedHitStatus();
		attackedHitStatus.fromObjectID = fromObjectID;
		attackedHitStatus.fromObject = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(fromObjectID);
		if (attackedHitStatus.fromObject != null)
		{
			attackedHitStatus.attackInfo = (attackedHitStatus.fromObject.FindAttackInfoExternal(attackInfoName, true, attackInfoRate) as AttackHitInfo);
		}
		if (attackedHitStatus.attackInfo == null)
		{
			attackedHitStatus.attackInfo = new AttackHitInfo();
		}
		attackedHitStatus.fromType = (StageObject.OBJECT_TYPE)fromType;
		attackedHitStatus.fromPos = fromPos;
		attackedHitStatus.hitPos = hitPos;
		attackedHitStatus.fromClientID = fromClientID;
		if (attackedHitStatus.fromObject != null)
		{
			attackedHitStatus.skillParam = attackedHitStatus.fromObject.GetSkillParam(skillIndex);
		}
		attackedHitStatus.regionID = regionID;
		attackedHitStatus.weakState = (Enemy.WEAK_STATE)weakState;
		attackedHitStatus.damage = damage;
		attackedHitStatus.validDamage = validDamage;
		attackedHitStatus.downAddBase = downAddBase;
		attackedHitStatus.downAddWeak = downAddWeak;
		attackedHitStatus.isArrowBleed = isArrowBleed;
		attackedHitStatus.arrowBleedDamage = arrowBleedDamage;
		attackedHitStatus.arrowBurstDamage = arrowBurstDamage;
		attackedHitStatus.badStatusAdd.Copy(badStatusAdd);
		attackedHitStatus.isSpAttackHit = isSpAttackHit;
		attackedHitStatus.damageDetails = damageDetails;
		attackedHitStatus.isShadowSealing = isShadowSealing;
		status = new AttackedHitStatusOwner(attackedHitStatus);
	}
}
