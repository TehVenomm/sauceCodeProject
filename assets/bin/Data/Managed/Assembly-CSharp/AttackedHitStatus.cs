using UnityEngine;

public class AttackedHitStatus
{
	public AttackHitColliderProcessor.HitParam hitParam;

	public AttackHitInfo attackInfo;

	public int fromObjectID;

	public StageObject fromObject;

	public StageObject.OBJECT_TYPE fromType;

	public Vector3 fromPos = Vector3.get_zero();

	public Vector3 hitPos = Vector3.get_zero();

	public float distanceXZ;

	public float hitTime;

	public int fromClientID;

	public DamageDistanceTable.DamageDistanceData damageDistanceData;

	public AtkAttribute atk = new AtkAttribute();

	public SkillInfo.SkillParam skillParam;

	public bool validDamage;

	public BadStatus badStatusAdd = new BadStatus();

	public int regionID = -1;

	public bool isDamageRegionOnly;

	public Enemy.WEAK_STATE weakState;

	public int damage;

	public AtkAttribute damageDetails = new AtkAttribute();

	public float downAddBase;

	public float downAddWeak;

	public bool isForceDown;

	public float concussionAdd;

	public bool isArrowBleed;

	public int arrowBleedDamage;

	public int arrowBurstDamage;

	public bool isShadowSealing;

	public bool isArrowBomb;

	public Vector3 hostPos = Vector3.get_zero();

	public float hostDir;

	public int afterHP;

	public int afterRegionHP;

	public int afterHealHp;

	public bool breakRegion;

	public int reactionType;

	public Vector3 blowForce = Vector3.get_zero();

	public float downTotal;

	public float concussionTotal;

	public BadStatus badStatusTotal = new BadStatus();

	public float damageHpRate;

	public bool arrowBleedSkipFirst;

	public bool isSpAttackHit;

	public int barrierHp;

	public int shieldHp;

	public int grabHp;

	public int shieldDamage;

	public EnemyAegisController.SyncParam aegisParam = new EnemyAegisController.SyncParam();

	public int deadReviveCount;

	public Player.ATTACK_MODE attackMode;

	public Vector3 exHitPos = Vector3.get_zero();
}
