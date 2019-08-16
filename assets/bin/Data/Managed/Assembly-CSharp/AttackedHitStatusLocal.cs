using UnityEngine;

public class AttackedHitStatusLocal
{
	public AttackedHitStatus origin
	{
		get;
		protected set;
	}

	public AttackHitColliderProcessor.HitParam hitParam => origin.hitParam;

	public AttackHitInfo attackInfo => origin.attackInfo;

	public int fromObjectID => origin.fromObjectID;

	public StageObject fromObject => origin.fromObject;

	public StageObject.OBJECT_TYPE fromType => origin.fromType;

	public Vector3 fromPos => origin.fromPos;

	public Vector3 hitPos => origin.hitPos;

	public float distanceXZ => origin.distanceXZ;

	public float hitTime => origin.hitTime;

	public int fromClientID => origin.fromClientID;

	public DamageDistanceTable.DamageDistanceData damageDistanceData => origin.damageDistanceData;

	public AtkAttribute atk => origin.atk;

	public SkillInfo.SkillParam skillParam => origin.skillParam;

	public bool validDamage => origin.validDamage;

	public BadStatus badStatusAdd => origin.badStatusAdd;

	public int regionID => origin.regionID;

	public bool isDamageRegionOnly => origin.isDamageRegionOnly;

	public Enemy.WEAK_STATE weakState => origin.weakState;

	public int damage
	{
		get
		{
			return origin.damage;
		}
		set
		{
			origin.damage = value;
		}
	}

	public AtkAttribute damageDetails
	{
		get
		{
			return origin.damageDetails;
		}
		set
		{
			origin.damageDetails = value;
		}
	}

	public float downAddBase
	{
		get
		{
			return origin.downAddBase;
		}
		set
		{
			origin.downAddBase = value;
		}
	}

	public float downAddWeak
	{
		get
		{
			return origin.downAddWeak;
		}
		set
		{
			origin.downAddWeak = value;
		}
	}

	public bool isForceDown
	{
		get
		{
			return origin.isForceDown;
		}
		set
		{
			origin.isForceDown = value;
		}
	}

	public float concussionAdd
	{
		get
		{
			return origin.concussionAdd;
		}
		set
		{
			origin.concussionAdd = value;
		}
	}

	public bool isArrowBleed
	{
		get
		{
			return origin.isArrowBleed;
		}
		set
		{
			origin.isArrowBleed = value;
		}
	}

	public int arrowBleedDamage
	{
		get
		{
			return origin.arrowBleedDamage;
		}
		set
		{
			origin.arrowBleedDamage = value;
		}
	}

	public int arrowBurstDamage
	{
		get
		{
			return origin.arrowBurstDamage;
		}
		set
		{
			origin.arrowBurstDamage = value;
		}
	}

	public bool isShadowSealing
	{
		get
		{
			return origin.isShadowSealing;
		}
		set
		{
			origin.isShadowSealing = value;
		}
	}

	public bool isArrowBomb
	{
		get
		{
			return origin.isArrowBomb;
		}
		set
		{
			origin.isArrowBomb = value;
		}
	}

	public Player.ATTACK_MODE attackMode
	{
		get
		{
			return origin.attackMode;
		}
		set
		{
			origin.attackMode = value;
		}
	}

	public AttackedHitStatusLocal()
	{
		origin = new AttackedHitStatus();
	}

	public AttackedHitStatusLocal(AttackedHitStatus status)
	{
		origin = status;
	}
}
