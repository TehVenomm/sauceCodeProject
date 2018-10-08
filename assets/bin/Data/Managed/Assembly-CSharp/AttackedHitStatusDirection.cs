using UnityEngine;

public class AttackedHitStatusDirection
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

	public Vector3 exHitPos => origin.exHitPos;

	public AtkAttribute atk
	{
		get
		{
			return origin.atk;
		}
		set
		{
			origin.atk = value;
		}
	}

	public SkillInfo.SkillParam skillParam
	{
		get
		{
			return origin.skillParam;
		}
		set
		{
			origin.skillParam = value;
		}
	}

	public bool validDamage
	{
		get
		{
			return origin.validDamage;
		}
		set
		{
			origin.validDamage = value;
		}
	}

	public BadStatus badStatusAdd
	{
		get
		{
			return origin.badStatusAdd;
		}
		set
		{
			origin.badStatusAdd = value;
		}
	}

	public int regionID
	{
		get
		{
			return origin.regionID;
		}
		set
		{
			origin.regionID = value;
		}
	}

	public bool isDamageRegionOnly
	{
		get
		{
			return origin.isDamageRegionOnly;
		}
		set
		{
			origin.isDamageRegionOnly = value;
		}
	}

	public Enemy.WEAK_STATE weakState
	{
		get
		{
			return origin.weakState;
		}
		set
		{
			origin.weakState = value;
		}
	}

	public AttackedHitStatusDirection()
	{
		origin = new AttackedHitStatus();
	}

	public AttackedHitStatusDirection(AttackedHitStatus status)
	{
		origin = status;
	}
}
