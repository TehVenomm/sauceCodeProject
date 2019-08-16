using System.Collections.Generic;
using UnityEngine;

public class FieldCarriableTurretGimmickObject : FieldCarriableGimmickObject
{
	public static readonly string kShotEffectName = "ef_btl_trap_04_01_01";

	public static readonly string kPutEffectName = "ef_btl_trap_01_02";

	public static readonly int kPutSEId = 10000058;

	private static readonly int kShiftIndex = 1000;

	private static readonly string kCannonHeadName = "head01";

	private static readonly string kCannonShotPosName = "shot01";

	private static readonly string kDefaultAttackInfoName = "cannonball_field";

	private static readonly string[] kDefaultAttackInfoNames = new string[1]
	{
		kDefaultAttackInfoName
	};

	private static readonly int kDefaultShotSEId = 10000094;

	private static readonly float kDefaultMaxCoolTime = 2f;

	protected Transform cannonHead;

	protected Transform cannonShotPos;

	protected Enemy targetObject;

	protected float searchRange = 10f;

	protected float[] maxCoolTimes;

	protected float coolTime;

	protected string[] attackInfoNames;

	protected Player ownerPlayer;

	protected int shotSEId = kDefaultShotSEId;

	protected float rotationSpeed = 1.5f;

	private float targetingTime;

	protected bool isTargeting;

	protected override void Awake()
	{
		base.Awake();
		maxCoolTimes = new float[1]
		{
			kDefaultMaxCoolTime
		};
		attackInfoNames = new string[1]
		{
			kDefaultAttackInfoName
		};
	}

	protected override void ParseParam(string value2)
	{
		base.ParseParam(value2);
		if (value2.IsNullOrWhiteSpace())
		{
			return;
		}
		List<string> list = new List<string>();
		List<float> list2 = new List<float>();
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 == null || array2.Length != 2)
			{
				continue;
			}
			if (array2[0].StartsWith("ct"))
			{
				list2.Add(float.Parse(array2[1]));
				continue;
			}
			if (array2[0].StartsWith("ai"))
			{
				list.Add(array2[1]);
				continue;
			}
			switch (array2[0])
			{
			case "sr":
				searchRange = float.Parse(array2[1]);
				break;
			case "se":
				shotSEId = int.Parse(array2[1]);
				break;
			case "rt":
				rotationSpeed = float.Parse(array2[1]);
				break;
			}
		}
		attackInfoNames = list.ToArray();
		maxCoolTimes = list2.ToArray();
		list.Clear();
		list2.Clear();
		list = null;
		list2 = null;
	}

	protected override void CreateModel()
	{
		base.CreateModel();
		if (modelTrans != null)
		{
			cannonHead = Utility.Find(modelTrans, kCannonHeadName);
			cannonShotPos = Utility.Find(modelTrans, kCannonShotPosName);
		}
	}

	protected override void OnStartCarry(Player owner)
	{
		base.OnStartCarry(owner);
		if (owner != null && ownerPlayer == null)
		{
			ownerPlayer = owner;
		}
		targetingTime = 0f;
	}

	protected override void OnEndCarry()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		base.OnEndCarry();
		EffectManager.OneShot(kPutEffectName, GetTransform().get_position(), GetTransform().get_rotation());
		SoundManager.PlayOneShotSE(kPutSEId, GetTransform().get_position());
	}

	private void LateUpdate()
	{
		if (!base.isCarrying && hasDeploied)
		{
			UpdateTarget();
			UpdateHeadRotation();
			if (coolTime <= 0f && Shot())
			{
				coolTime = GetMaxCoolTime();
			}
			else
			{
				coolTime -= Time.get_deltaTime();
			}
		}
	}

	public float GetMaxCoolTime()
	{
		if (maxCoolTimes.Length > currentLv)
		{
			return maxCoolTimes[currentLv];
		}
		return kDefaultMaxCoolTime;
	}

	public string GetAttackInfoName()
	{
		if (attackInfoNames.Length > currentLv)
		{
			return attackInfoNames[currentLv];
		}
		return kDefaultAttackInfoName;
	}

	protected virtual void UpdateTarget()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		Enemy enemy = targetObject;
		if (enemy != null)
		{
			float num = Vector3.Magnitude(enemy._transform.get_position() - GetTransform().get_position());
			if (num > searchRange || !targetObject.HasValidTargetPoint())
			{
				targetObject = null;
			}
			else
			{
				targetingTime += Time.get_deltaTime();
			}
			return;
		}
		List<StageObject> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
		for (int i = 0; i < enemyList.Count; i++)
		{
			Enemy enemy2 = enemyList[i] as Enemy;
			if (!enemy2.HasValidTargetPoint())
			{
				continue;
			}
			float num2 = Vector3.Magnitude(enemy2._transform.get_position() - GetTransform().get_position());
			if (num2 > searchRange)
			{
				continue;
			}
			if (enemy != null)
			{
				float num3 = Vector3.Magnitude(enemy._transform.get_position() - GetTransform().get_position());
				if (num2 > num3)
				{
					continue;
				}
			}
			enemy = enemy2;
		}
		targetObject = enemy;
		targetingTime = 0f;
		isTargeting = false;
	}

	protected virtual void UpdateHeadRotation()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		if (!(targetObject == null) && !(cannonHead == null))
		{
			Vector3 position = cannonHead.get_position();
			position.y = 0f;
			Vector3 position2 = targetObject._transform.get_position();
			position2.y = 0f;
			Quaternion rotation = cannonHead.get_rotation();
			Quaternion val = Quaternion.LookRotation(position2 - position, Vector3.get_up());
			cannonHead.set_rotation(Quaternion.Lerp(rotation, val, targetingTime * rotationSpeed));
			if (targetingTime * rotationSpeed >= 1f)
			{
				isTargeting = true;
			}
		}
	}

	protected virtual bool Shot()
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		if (cannonHead == null || cannonShotPos == null || targetObject == null || ownerPlayer == null || base.isCarrying || !isTargeting)
		{
			return false;
		}
		AttackInfo attackInfo = ownerPlayer.FindAttackInfo(GetAttackInfoName());
		if (attackInfo == null)
		{
			return false;
		}
		if (shotSEId > 0)
		{
			SoundManager.PlayOneShotSE(shotSEId, cannonHead.get_position());
		}
		AnimEventShot.Create(ownerPlayer, attackInfo, cannonShotPos.get_position(), cannonHead.get_rotation());
		return true;
	}

	public static string[] GetAttackInfoNames(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return kDefaultAttackInfoNames;
		}
		List<string> list = new List<string>();
		list.Add(kDefaultAttackInfoName);
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2 && array2[0].StartsWith("ai"))
			{
				list.Add(array2[1]);
			}
		}
		return list.ToArray();
	}

	public static int GetShotSEId(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return kDefaultShotSEId;
		}
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2 && array2[0] == "se")
			{
				return int.Parse(array2[1]);
			}
		}
		return kDefaultShotSEId;
	}
}
