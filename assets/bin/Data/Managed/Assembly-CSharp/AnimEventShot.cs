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
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		Transform val = stage_object.FindNode(data.stringArgs[1]);
		if (val == null)
		{
			val = stage_object._transform;
		}
		if (val.get_gameObject() != null && !val.get_gameObject().get_activeInHierarchy())
		{
			return null;
		}
		Vector3 val2 = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]) + offset;
		Matrix4x4 localToWorldMatrix = val.get_localToWorldMatrix();
		val2 = localToWorldMatrix.MultiplyPoint3x4(val2);
		Quaternion val3 = Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
		val3 = ((data.intArgs[0] != 0) ? (stage_object.get_gameObject().get_transform().get_rotation() * val3) : (val.get_rotation() * val3));
		return Create(stage_object, atk_info, val2, val3, null, true, null, null, null, Player.ATTACK_MODE.NONE, null, null);
	}

	public static AnimEventShot CreateByExternalBulletData(BulletData exBulletData, StageObject stageObj, AttackInfo atkInfo, Vector3 pos, Quaternion rot, AtkAttribute exAtk = null, Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE, SkillInfo.SkillParam exSkillParam = null)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (exBulletData == null)
		{
			Log.Error("exBulletData is null !!");
			return null;
		}
		return Create(stageObj, atkInfo, pos, rot, null, true, null, exBulletData, exAtk, attackMode, null, exSkillParam);
	}

	public static AnimEventShot CreateArrow(StageObject stage_object, AttackInfo atk_info, Vector3 pos, Quaternion rot, GameObject attach_object, bool isScaling, string change_effect, DamageDistanceTable.DamageDistanceData damageDistanceData)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		return Create(stage_object, atk_info, pos, rot, attach_object, isScaling, change_effect, null, null, Player.ATTACK_MODE.NONE, damageDistanceData, null);
	}

	public static AnimEventShot Create(StageObject stage_object, AttackInfo atk_info, Vector3 pos, Quaternion rot, GameObject attach_object = null, bool isScaling = true, string change_effect = null, BulletData exBulletData = null, AtkAttribute exAtk = null, Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE, DamageDistanceTable.DamageDistanceData damageDistanceData = null, SkillInfo.SkillParam exSkillParam = null)
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
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
			Log.Error("Failed to shoot bullet!! atk_info:" + ((atk_info == null) ? string.Empty : atk_info.name));
			return null;
		}
		Transform val = Utility.CreateGameObject(bulletData.get_name(), MonoBehaviourSingleton<StageObjectManager>.I._transform, -1);
		AnimEventShot animEventShot = val.get_gameObject().AddComponent<AnimEventShot>();
		if (isScaling)
		{
			Transform val2 = stage_object.get_gameObject().get_transform();
			animEventShot.SetBaseScale(val2.get_lossyScale());
		}
		else
		{
			animEventShot.SetBaseScale(Vector3.get_one());
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		if (base._collider == null)
		{
			CapsuleCollider val = this.get_gameObject().AddComponent<CapsuleCollider>();
			val.set_center(new Vector3(0f, 0f, 0f));
			val.set_direction(2);
			val.set_isTrigger(true);
			base._collider = val;
			capsuleCollider = val;
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
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		attachObject = attach_object;
		if (!(attach_object == null))
		{
			attach_object.get_transform().set_parent(base._transform);
			attach_object.get_transform().set_localPosition(Vector3.get_zero());
			attach_object.get_transform().set_localRotation(Quaternion.get_identity());
		}
	}

	public void SetTargetPoint()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		_targetPoint = this.get_gameObject().AddComponent<TargetPoint>();
		_targetPoint.isAimEnable = false;
		_targetPoint.isTargetEnable = false;
	}

	protected override void Update()
	{
		base.Update();
		if (!isDestroyed && isBossPierceArrow)
		{
			pierceArrowSec += Time.get_deltaTime();
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
