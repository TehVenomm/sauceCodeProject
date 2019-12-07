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

	public void SetArrowInfo(bool isAim, bool isBossPierce, bool isArrowRain = false)
	{
		isShotArrow = true;
		isAimMode = isAim;
		isBossPierceArrow = isBossPierce;
		pierceArrowSec = 0f;
		if (isArrowRain)
		{
			pierceArrowInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainPierceDamageInterval;
		}
		else
		{
			pierceArrowInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.pierceInterval;
		}
	}

	public static AnimEventShot Create(StageObject stage_object, AnimEventData.EventData data, AttackInfo atk_info, Vector3 offset)
	{
		Transform transform = stage_object.FindNode(data.stringArgs[1]);
		if (transform == null)
		{
			transform = stage_object._transform;
		}
		if (transform.gameObject != null && !transform.gameObject.activeInHierarchy)
		{
			return null;
		}
		Vector3 point = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]) + offset;
		point = transform.localToWorldMatrix.MultiplyPoint3x4(point);
		Quaternion rhs = Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
		rhs = ((data.intArgs[0] != 0) ? (stage_object.gameObject.transform.rotation * rhs) : (transform.rotation * rhs));
		return Create(stage_object, atk_info, point, rhs);
	}

	public static AnimEventShot CreateByExternalBulletData(BulletData exBulletData, StageObject stageObj, AttackInfo atkInfo, Vector3 pos, Quaternion rot, AtkAttribute exAtk = null, Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE, SkillInfo.SkillParam exSkillParam = null)
	{
		if (exBulletData == null)
		{
			Log.Error("exBulletData is null !!");
			return null;
		}
		return Create(stageObj, atkInfo, pos, rot, null, isScaling: true, null, exBulletData, exAtk, attackMode, null, exSkillParam);
	}

	public static AnimEventShot CreateArrow(StageObject stage_object, AttackInfo atk_info, Vector3 pos, Quaternion rot, GameObject attach_object, bool isScaling, string change_effect, DamageDistanceTable.DamageDistanceData damageDistanceData)
	{
		return Create(stage_object, atk_info, pos, rot, attach_object, isScaling, change_effect, null, null, Player.ATTACK_MODE.NONE, damageDistanceData);
	}

	public static AnimEventShot Create(StageObject stage_object, AttackInfo atk_info, Vector3 pos, Quaternion rot, GameObject attach_object = null, bool isScaling = true, string change_effect = null, BulletData exBulletData = null, AtkAttribute exAtk = null, Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE, DamageDistanceTable.DamageDistanceData damageDistanceData = null, SkillInfo.SkillParam exSkillParam = null)
	{
		BulletData bulletData = atk_info.bulletData;
		if (exBulletData != null)
		{
			bulletData = exBulletData;
		}
		if (atk_info.isBulletSkillReference)
		{
			Player player = stage_object as Player;
			if (player != null)
			{
				SkillInfo.SkillParam actSkillParam = player.skillInfo.actSkillParam;
				if (actSkillParam != null)
				{
					bulletData = actSkillParam.bullet;
				}
			}
		}
		if (bulletData == null)
		{
			Log.Error("Failed to shoot bullet!! atk_info:" + ((atk_info != null) ? atk_info.name : ""));
			return null;
		}
		if (MonoBehaviourSingleton<StageObjectManager>.I == null)
		{
			return null;
		}
		AnimEventShot animEventShot = Utility.CreateGameObject(bulletData.name, MonoBehaviourSingleton<StageObjectManager>.I._transform).gameObject.AddComponent<AnimEventShot>();
		if (isScaling)
		{
			Transform transform = stage_object.gameObject.transform;
			animEventShot.SetBaseScale(transform.lossyScale);
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
		animEventShot.Shot(stage_object, atk_info, bulletData, pos, rot, change_effect, reference_attack: true, exAtk, attackMode, damageDistanceData, exSkillParam);
		return animEventShot;
	}

	protected override void Awake()
	{
		base.Awake();
		if (base._collider == null)
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
			if (attachObject != null)
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
		if (!(attach_object == null))
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
		if (isDestroyed || !isBossPierceArrow)
		{
			return;
		}
		pierceArrowSec += Time.deltaTime;
		if (pierceArrowSec >= pierceArrowInterval)
		{
			pierceArrowSec -= pierceArrowInterval;
			if (attackHitChecker != null)
			{
				attackHitChecker.ClearHitInfo(GetAttackInfo().name);
			}
		}
	}
}
