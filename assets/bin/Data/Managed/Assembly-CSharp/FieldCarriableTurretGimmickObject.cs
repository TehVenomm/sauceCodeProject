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
		base.OnEndCarry();
		EffectManager.OneShot(kPutEffectName, GetTransform().position, GetTransform().rotation);
		SoundManager.PlayOneShotSE(kPutSEId, GetTransform().position);
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
				coolTime -= Time.deltaTime;
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
		Enemy enemy = targetObject;
		if (enemy != null)
		{
			if (Vector3.Magnitude(enemy._transform.position - GetTransform().position) > searchRange || !targetObject.HasValidTargetPoint())
			{
				targetObject = null;
			}
			else
			{
				targetingTime += Time.deltaTime;
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
			float num = Vector3.Magnitude(enemy2._transform.position - GetTransform().position);
			if (num > searchRange)
			{
				continue;
			}
			if (enemy != null)
			{
				float num2 = Vector3.Magnitude(enemy._transform.position - GetTransform().position);
				if (num > num2)
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
		if (!(targetObject == null) && !(cannonHead == null))
		{
			Vector3 position = cannonHead.position;
			position.y = 0f;
			Vector3 position2 = targetObject._transform.position;
			position2.y = 0f;
			Quaternion rotation = cannonHead.rotation;
			Quaternion b = Quaternion.LookRotation(position2 - position, Vector3.up);
			cannonHead.rotation = Quaternion.Lerp(rotation, b, targetingTime * rotationSpeed);
			if (targetingTime * rotationSpeed >= 1f)
			{
				isTargeting = true;
			}
		}
	}

	protected virtual bool Shot()
	{
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
			SoundManager.PlayOneShotSE(shotSEId, cannonHead.position);
		}
		AnimEventShot.Create(ownerPlayer, attackInfo, cannonShotPos.position, cannonHead.rotation);
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
