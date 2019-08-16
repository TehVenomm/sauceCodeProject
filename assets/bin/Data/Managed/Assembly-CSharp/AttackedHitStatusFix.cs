using UnityEngine;

public class AttackedHitStatusFix
{
	public AttackedHitStatus origin
	{
		get;
		protected set;
	}

	public AttackHitInfo attackInfo => origin.attackInfo;

	public int fromObjectID => origin.fromObjectID;

	public StageObject fromObject => origin.fromObject;

	public StageObject.OBJECT_TYPE fromType => origin.fromType;

	public Vector3 hitPos => origin.hitPos;

	public int fromClientID => origin.fromClientID;

	public SkillInfo.SkillParam skillParam => origin.skillParam;

	public int regionID => origin.regionID;

	public bool isDamageRegionOnly => origin.isDamageRegionOnly;

	public Enemy.WEAK_STATE weakState => origin.weakState;

	public bool IsSpAttackHit => origin.isSpAttackHit;

	public int damage => origin.damage;

	public AtkAttribute damageDetails => origin.damageDetails;

	public float downAddBase => origin.downAddBase;

	public float downAddWeak => origin.downAddWeak;

	public bool isForceDown => origin.isForceDown;

	public float concussionAdd => origin.concussionAdd;

	public bool isArrowBleed => origin.isArrowBleed;

	public int arrowBleedDamage => origin.arrowBleedDamage;

	public int arrowBurstDamage => origin.arrowBurstDamage;

	public bool isShadowSealing => origin.isShadowSealing;

	public bool isArrowBomb => origin.isArrowBomb;

	public Vector3 hostPos => origin.hostPos;

	public float hostDir => origin.hostDir;

	public int afterHP => origin.afterHP;

	public int afterRegionHP => origin.afterRegionHP;

	public int afterHealHp => origin.afterHealHp;

	public bool breakRegion => origin.breakRegion;

	public int reactionType => origin.reactionType;

	public Vector3 blowForce => origin.blowForce;

	public float downTotal => origin.downTotal;

	public float concussionTotal => origin.concussionTotal;

	public BadStatus badStatusTotal => origin.badStatusTotal;

	public float damageHpRate => origin.damageHpRate;

	public bool arrowBleedSkipFirst => origin.arrowBleedSkipFirst;

	public int afterBarrierHp => origin.barrierHp;

	public int afterShieldHp => origin.shieldHp;

	public int afterGrabHp => origin.grabHp;

	public int shieldDamage => origin.shieldDamage;

	public EnemyAegisController.SyncParam aegisParam => origin.aegisParam;

	public int deadReviveCount => origin.deadReviveCount;

	public AttackedHitStatusFix()
	{
		origin = new AttackedHitStatus();
	}

	public AttackedHitStatusFix(AttackedHitStatus status)
	{
		origin = status;
	}
}
