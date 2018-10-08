using UnityEngine;

public class AttackedHitStatusOwner
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

	public Vector3 fromPos => origin.fromPos;

	public Vector3 hitPos => origin.hitPos;

	public int fromClientID => origin.fromClientID;

	public SkillInfo.SkillParam skillParam => origin.skillParam;

	public bool validDamage => origin.validDamage;

	public BadStatus badStatusAdd => origin.badStatusAdd;

	public int regionID => origin.regionID;

	public Enemy.WEAK_STATE weakState => origin.weakState;

	public bool IsSpAttackHit => origin.isSpAttackHit;

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

	public int arrowBleedDamage => origin.arrowBleedDamage;

	public int arrowBurstDamage => origin.arrowBurstDamage;

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

	public Vector3 hostPos
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return origin.hostPos;
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			origin.hostPos = value;
		}
	}

	public float hostDir
	{
		get
		{
			return origin.hostDir;
		}
		set
		{
			origin.hostDir = value;
		}
	}

	public int afterHP
	{
		get
		{
			return origin.afterHP;
		}
		set
		{
			origin.afterHP = value;
		}
	}

	public int afterRegionHP
	{
		get
		{
			return origin.afterRegionHP;
		}
		set
		{
			origin.afterRegionHP = value;
		}
	}

	public int afterHealHp
	{
		get
		{
			return origin.afterHealHp;
		}
		set
		{
			origin.afterHealHp = value;
		}
	}

	public bool breakRegion
	{
		get
		{
			return origin.breakRegion;
		}
		set
		{
			origin.breakRegion = value;
		}
	}

	public int reactionType
	{
		get
		{
			return origin.reactionType;
		}
		set
		{
			origin.reactionType = value;
		}
	}

	public Vector3 blowForce
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return origin.blowForce;
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			origin.blowForce = value;
		}
	}

	public float downTotal
	{
		get
		{
			return origin.downTotal;
		}
		set
		{
			origin.downTotal = value;
		}
	}

	public BadStatus badStatusTotal
	{
		get
		{
			return origin.badStatusTotal;
		}
		set
		{
			origin.badStatusTotal = value;
		}
	}

	public float damageHpRate
	{
		get
		{
			return origin.damageHpRate;
		}
		set
		{
			origin.damageHpRate = value;
		}
	}

	public bool arrowBleedSkipFirst
	{
		get
		{
			return origin.arrowBleedSkipFirst;
		}
		set
		{
			origin.arrowBleedSkipFirst = value;
		}
	}

	public int afterBarrierHp
	{
		get
		{
			return origin.barrierHp;
		}
		set
		{
			origin.barrierHp = value;
		}
	}

	public int afterShieldHp
	{
		get
		{
			return origin.shieldHp;
		}
		set
		{
			origin.shieldHp = value;
		}
	}

	public int afterGrabHp
	{
		get
		{
			return origin.grabHp;
		}
		set
		{
			origin.grabHp = value;
		}
	}

	public int shieldDamage
	{
		get
		{
			return origin.shieldDamage;
		}
		set
		{
			origin.shieldDamage = value;
		}
	}

	public EnemyAegisController.SyncParam aegisParam
	{
		get
		{
			return origin.aegisParam;
		}
		set
		{
			origin.aegisParam = value;
		}
	}

	public AttackedHitStatusOwner()
	{
		origin = new AttackedHitStatus();
	}

	public AttackedHitStatusOwner(AttackedHitStatus status)
	{
		origin = status;
	}
}
