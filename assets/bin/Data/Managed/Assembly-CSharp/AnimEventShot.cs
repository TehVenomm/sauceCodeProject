using UnityEngine;

public class AnimEventShot : BulletObject
{
	public enum TARGET_TYPE
	{
		PLAYER_ALL,
		RANDOM_PICKUP,
		RANDOM_POSITION
	}

	private float pierceArrowSec;

	private float pierceArrowInterval = 0.033f;

	private TargetPoint _targetPoint;

	protected GameObject attachObject;

	public TargetPoint targetPoint => _targetPoint;

	public void SetArrowInfo(bool isAimBoss, bool isBossPierce)
	{
		isShotArrow = true;
		isAimBossMode = isAimBoss;
		isBossPierceArrow = isBossPierce;
		pierceArrowSec = 0f;
		pierceArrowInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.pierceInterval;
	}

	public static AnimEventShot Create(StageObject stage_object, AnimEventData.EventData data, AttackInfo atk_info, Vector3 offset)
	{
		Transform transform = stage_object.FindNode(data.stringArgs[1]);
		if ((Object)transform == (Object)null)
		{
			transform = stage_object._transform;
		}
		if ((Object)transform.gameObject != (Object)null && !transform.gameObject.activeInHierarchy)
		{
			return null;
		}
		Vector3 v = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]) + offset;
		v = transform.localToWorldMatrix.MultiplyPoint3x4(v);
		Quaternion rhs = Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
		rhs = ((data.intArgs[0] != 0) ? (stage_object.gameObject.transform.rotation * rhs) : (transform.rotation * rhs));
		return Create(stage_object, atk_info, v, rhs, null, true, null, null, null, Player.ATTACK_MODE.NONE, null, null);
	}

	public static AnimEventShot CreateByExternalBulletData(BulletData exBulletData, StageObject stageObj, AttackInfo atkInfo, Vector3 pos, Quaternion rot, AtkAttribute exAtk = null, Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE, SkillInfo.SkillParam exSkillParam = null)
	{
		if ((Object)exBulletData == (Object)null)
		{
			Log.Error("exBulletData is null !!");
			return null;
		}
		return Create(stageObj, atkInfo, pos, rot, null, true, null, exBulletData, exAtk, attackMode, null, exSkillParam);
	}

	public static AnimEventShot CreateArrow(StageObject stage_object, AttackInfo atk_info, Vector3 pos, Quaternion rot, GameObject attach_object, bool isScaling, string change_effect, DamageDistanceTable.DamageDistanceData damageDistanceData)
	{
		return Create(stage_object, atk_info, pos, rot, attach_object, isScaling, change_effect, null, null, Player.ATTACK_MODE.NONE, damageDistanceData, null);
	}

	public static AnimEventShot Create(StageObject stage_object, AttackInfo atk_info, Vector3 pos, Quaternion rot, GameObject attach_object = null, bool isScaling = true, string change_effect = null, BulletData exBulletData = null, AtkAttribute exAtk = null, Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE, DamageDistanceTable.DamageDistanceData damageDistanceData = null, SkillInfo.SkillParam exSkillParam = null)
	{
		BulletData bulletData = atk_info.bulletData;
		if ((Object)exBulletData != (Object)null)
		{
			bulletData = exBulletData;
		}
		if (atk_info.isBulletSkillReference)
		{
			Player player = stage_object as Player;
			if ((Object)player != (Object)null)
			{
				SkillInfo.SkillParam actSkillParam = player.skillInfo.actSkillParam;
				if (actSkillParam != null)
				{
					bulletData = actSkillParam.bullet;
				}
			}
		}
		if ((Object)bulletData == (Object)null)
		{
			Log.Error("Failed to shoot bullet!! atk_info:" + ((atk_info == null) ? string.Empty : atk_info.name));
			return null;
		}
		Transform transform = Utility.CreateGameObject(bulletData.name, MonoBehaviourSingleton<StageObjectManager>.I._transform, -1);
		AnimEventShot animEventShot = transform.gameObject.AddComponent<AnimEventShot>();
		if (isScaling)
		{
			Transform transform2 = stage_object.gameObject.transform;
			animEventShot.SetBaseScale(transform2.lossyScale);
		}
		else
		{
			animEventShot.SetBaseScale(Vector3.one);
		}
		animEventShot.SetAttachObject(attach_object);
		if (bulletData.type == BulletData.BULLET_TYPE.BREAKABLE)
		{
			animEventShot.SetTargetPoint();
		}
		animEventShot.Shot(stage_object, atk_info, bulletData, pos, rot, change_effect, true, exAtk, attackMode, damageDistanceData, exSkillParam);
		return animEventShot;
	}

	protected override void Awake()
	{
		base.Awake();
		if ((Object)base._collider == (Object)null)
		{
			CapsuleCollider capsuleCollider = base.gameObject.AddComponent<CapsuleCollider>();
			capsuleCollider.center = new Vector3(0f, 0f, 0f);
			capsuleCollider.direction = 2;
			capsuleCollider.isTrigger = true;
			base._collider = capsuleCollider;
			base.capsuleCollider = capsuleCollider;
			isColliderCreate = true;
		}
	}

	protected override void Start()
	{
		base.Start();
	}

	public override void OnDestroy()
	{
		if (!AppMain.isApplicationQuit && !isDestroyed)
		{
			if ((Object)attachObject != (Object)null)
			{
				Object.Destroy(attachObject);
				attachObject = null;
			}
			base.OnDestroy();
		}
	}

	protected override bool IsLoopEnd()
	{
		return true;
	}

	public void SetAttachObject(GameObject attach_object)
	{
		attachObject = attach_object;
		if (!((Object)attach_object == (Object)null))
		{
			attach_object.transform.parent = base._transform;
			attach_object.transform.localPosition = Vector3.zero;
			attach_object.transform.localRotation = Quaternion.identity;
		}
	}

	public void SetTargetPoint()
	{
		_targetPoint = base.gameObject.AddComponent<TargetPoint>();
		_targetPoint.isAimEnable = false;
		_targetPoint.isTargetEnable = false;
	}

	protected override void Update()
	{
		base.Update();
		if (!isDestroyed && isBossPierceArrow)
		{
			pierceArrowSec += Time.deltaTime;
			if (pierceArrowSec >= pierceArrowInterval)
			{
				pierceArrowSec -= pierceArrowInterval;
				if (!object.ReferenceEquals(attackHitChecker, null))
				{
					attackHitChecker.ClearAll();
				}
			}
		}
	}
}
