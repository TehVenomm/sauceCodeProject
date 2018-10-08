using UnityEngine;

public class Coop_Model_ObjectAttackedHitOwner : Coop_Model_ObjectBase
{
	public string attackInfoName;

	public float attackInfoRate;

	public int fromObjectID;

	public int fromType;

	public Vector3 fromPos = Vector3.zero;

	public Vector3 hitPos = Vector3.zero;

	public int fromClientID;

	public int skillIndex = -1;

	public int regionID = -1;

	public bool isDamageRegionOnly;

	public int weakState;

	public int damage;

	public bool validDamage;

	public float downAddBase;

	public float downAddWeak;

	public bool isForceDown;

	public bool isArrowBleed;

	public int arrowBleedDamage;

	public int arrowBurstDamage;

	public BadStatus badStatusAdd = new BadStatus();

	public bool isSpAttackHit;

	public AtkAttribute damageDetails = new AtkAttribute();

	public bool isShadowSealing;

	public Coop_Model_ObjectAttackedHitOwner()
	{
		base.packetType = PACKET_TYPE.OBJECT_ATTACKED_HIT_OWNER;
	}

	public void SetAttackedHitStatus(AttackedHitStatusOwner status)
	{
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
		isDamageRegionOnly = status.isDamageRegionOnly;
		weakState = (int)status.weakState;
		damage = status.damage;
		validDamage = status.validDamage;
		downAddBase = status.downAddBase;
		downAddWeak = status.downAddWeak;
		isForceDown = status.isForceDown;
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
		AttackedHitStatus attackedHitStatus = new AttackedHitStatus();
		attackedHitStatus.fromObjectID = fromObjectID;
		attackedHitStatus.fromObject = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(fromObjectID);
		if ((Object)attackedHitStatus.fromObject != (Object)null)
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
		if ((Object)attackedHitStatus.fromObject != (Object)null)
		{
			attackedHitStatus.skillParam = attackedHitStatus.fromObject.GetSkillParam(skillIndex);
		}
		attackedHitStatus.regionID = regionID;
		attackedHitStatus.isDamageRegionOnly = isDamageRegionOnly;
		attackedHitStatus.weakState = (Enemy.WEAK_STATE)weakState;
		attackedHitStatus.damage = damage;
		attackedHitStatus.validDamage = validDamage;
		attackedHitStatus.downAddBase = downAddBase;
		attackedHitStatus.downAddWeak = downAddWeak;
		attackedHitStatus.isForceDown = isForceDown;
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
