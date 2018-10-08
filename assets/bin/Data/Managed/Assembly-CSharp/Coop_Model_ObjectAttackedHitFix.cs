using UnityEngine;

public class Coop_Model_ObjectAttackedHitFix : Coop_Model_ObjectBase
{
	public string attackInfoName;

	public float attackInfoRate;

	public int fromObjectID;

	public int fromType;

	public Vector3 hitPos = Vector3.zero;

	public int fromClientID;

	public int skillIndex = -1;

	public int regionID = -1;

	public int weakState;

	public int damage;

	public float downAddBase;

	public float downAddWeak;

	public bool isForceDown;

	public bool isArrowBleed;

	public int arrowBleedDamage;

	public int arrowBurstDamage;

	public Vector3 hostPos = Vector3.zero;

	public float hostDir;

	public int afterHP;

	public int afterRegionHP;

	public int afterHealHp;

	public bool breakRegion;

	public int reactionType;

	public Vector3 blowForce = Vector3.zero;

	public float downTotal;

	public BadStatus badStatusTotal = new BadStatus();

	public float damageHpRate;

	public bool arrowBleedSkipFirst;

	public bool isSpAttackHit;

	public int afterRegionBarrierHp;

	public AtkAttribute damageDetails = new AtkAttribute();

	public int afterShieldHp;

	public int afterGrabHp;

	public bool isShadowSealing;

	public EnemyAegisController.SyncParam aegisParam = new EnemyAegisController.SyncParam();

	public Coop_Model_ObjectAttackedHitFix()
	{
		base.packetType = PACKET_TYPE.OBJECT_ATTACKED_HIT_FIX;
	}

	public override bool IsPromiseOverAgainCheck()
	{
		return true;
	}

	public override Vector3 GetObjectPosition()
	{
		return hostPos;
	}

	public override bool IsHaveObjectPosition()
	{
		if (reactionType != 0)
		{
			return true;
		}
		return false;
	}

	public override bool IsForceHandleBefore(StageObject owner)
	{
		if (reactionType != 0)
		{
			return true;
		}
		return base.IsForceHandleBefore(owner);
	}

	public void SetAttackedHitStatus(AttackedHitStatusFix status)
	{
		attackInfoName = status.attackInfo.name;
		attackInfoRate = status.attackInfo.rateInfoRate;
		fromObjectID = status.fromObjectID;
		fromType = (int)status.fromType;
		hitPos = status.hitPos;
		fromClientID = status.fromClientID;
		if (status.skillParam != null)
		{
			skillIndex = status.skillParam.skillIndex;
		}
		regionID = status.regionID;
		weakState = (int)status.weakState;
		damage = status.damage;
		downAddBase = status.downAddBase;
		downAddWeak = status.downAddWeak;
		isArrowBleed = status.isArrowBleed;
		arrowBleedDamage = status.arrowBleedDamage;
		arrowBurstDamage = status.arrowBurstDamage;
		hostPos = status.hostPos;
		hostDir = status.hostDir;
		afterHP = status.afterHP;
		afterRegionHP = status.afterRegionHP;
		afterHealHp = status.afterHealHp;
		breakRegion = status.breakRegion;
		reactionType = status.reactionType;
		blowForce = status.blowForce;
		downTotal = status.downTotal;
		badStatusTotal.Copy(status.badStatusTotal);
		damageHpRate = status.damageHpRate;
		arrowBleedSkipFirst = status.arrowBleedSkipFirst;
		isSpAttackHit = status.IsSpAttackHit;
		afterRegionBarrierHp = status.afterBarrierHp;
		damageDetails = status.damageDetails;
		afterShieldHp = status.afterShieldHp;
		afterGrabHp = status.afterGrabHp;
		isShadowSealing = status.isShadowSealing;
		aegisParam.Copy(status.aegisParam);
	}

	public void CopyAttackedHitStatus(out AttackedHitStatusFix status)
	{
		AttackedHitStatus attackedHitStatus = new AttackedHitStatus();
		attackedHitStatus.fromObjectID = fromObjectID;
		attackedHitStatus.fromObject = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(fromObjectID);
		if ((Object)attackedHitStatus.fromObject != (Object)null && !attackedHitStatus.fromObject.isLoading)
		{
			attackedHitStatus.attackInfo = (attackedHitStatus.fromObject.FindAttackInfoExternal(attackInfoName, true, attackInfoRate) as AttackHitInfo);
		}
		if (attackedHitStatus.attackInfo == null)
		{
			attackedHitStatus.attackInfo = new AttackHitInfo();
		}
		attackedHitStatus.fromType = (StageObject.OBJECT_TYPE)fromType;
		attackedHitStatus.hitPos = hitPos;
		attackedHitStatus.fromClientID = fromClientID;
		if ((Object)attackedHitStatus.fromObject != (Object)null)
		{
			attackedHitStatus.skillParam = attackedHitStatus.fromObject.GetSkillParam(skillIndex);
		}
		attackedHitStatus.regionID = regionID;
		attackedHitStatus.weakState = (Enemy.WEAK_STATE)weakState;
		attackedHitStatus.damage = damage;
		attackedHitStatus.downAddBase = downAddBase;
		attackedHitStatus.downAddWeak = downAddWeak;
		attackedHitStatus.isForceDown = isForceDown;
		attackedHitStatus.isArrowBleed = isArrowBleed;
		attackedHitStatus.arrowBleedDamage = arrowBleedDamage;
		attackedHitStatus.arrowBurstDamage = arrowBurstDamage;
		attackedHitStatus.hostPos = hostPos;
		attackedHitStatus.hostDir = hostDir;
		attackedHitStatus.afterHP = afterHP;
		attackedHitStatus.afterRegionHP = afterRegionHP;
		attackedHitStatus.afterHealHp = afterHealHp;
		attackedHitStatus.breakRegion = breakRegion;
		attackedHitStatus.reactionType = reactionType;
		attackedHitStatus.blowForce = blowForce;
		attackedHitStatus.downTotal = downTotal;
		attackedHitStatus.badStatusTotal.Copy(badStatusTotal);
		attackedHitStatus.damageHpRate = damageHpRate;
		attackedHitStatus.arrowBleedSkipFirst = arrowBleedSkipFirst;
		attackedHitStatus.isSpAttackHit = isSpAttackHit;
		attackedHitStatus.barrierHp = afterRegionBarrierHp;
		attackedHitStatus.damageDetails = damageDetails;
		attackedHitStatus.shieldHp = afterShieldHp;
		attackedHitStatus.grabHp = afterGrabHp;
		attackedHitStatus.isShadowSealing = isShadowSealing;
		attackedHitStatus.aegisParam.Copy(aegisParam);
		status = new AttackedHitStatusFix(attackedHitStatus);
	}
}
